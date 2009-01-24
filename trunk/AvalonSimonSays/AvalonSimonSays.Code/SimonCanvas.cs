using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AvalonSimonSays.Promotion;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Input;
using System.Linq;
using System.Windows.Shapes;

namespace AvalonSimonSays.Code
{
	[Script]
	public partial class SimonCanvas : Canvas, IDisposable
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 480;

		public readonly Canvas Content;
		public readonly Canvas InfoLayer;
		public readonly Canvas Overlay;

		public readonly BindingList<Option> Options = new BindingList<Option>();

		public readonly PlayerIdentity LocalIdentity = new PlayerIdentity { Name = "Local Player" };

		bool InternalOptionsEnabled = true;
		public bool OptionsEnabled
		{
			get
			{
				return InternalOptionsEnabled;
			}
			set
			{
				InternalOptionsEnabled = value;
				Options.ForEach(k => k.Overlay.Show(value));
			}
		}


		public AnimatedOpacity<Image> HappySimon;

		public readonly BindingList<PlayerIdentity> CoPlayers = new BindingList<PlayerIdentity>();

		public IEnumerable<PlayerIdentity> AllPlayers
		{
			get
			{
				return CoPlayers.ConcatSingle(this.LocalIdentity);
			}
		}

		public bool LocalIdentityIsPrimate
		{
			get
			{
				return this.AllPlayers.Min(k => k.Number) == this.LocalIdentity.Number;
			}
		}

		public int MyHighestScore;

		public readonly TextBox Score;

		public SimonCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;
			Background = Brushes.Black;

			this.ClipToBounds = true;

			this.Content = new Canvas { Width = DefaultWidth, Height = DefaultHeight }.AttachTo(this);
			this.InfoLayer = new Canvas { Width = DefaultWidth, Height = DefaultHeight }.AttachTo(this);
			this.Overlay = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
			}.AttachTo(this);

			new Rectangle
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
				Fill = Brushes.White,
				Opacity = 0
			}.AttachTo(this.Overlay);

			this.Arrows.AttachTo(this.InfoLayer);

			new Image
			{
				Stretch = Stretch.Fill,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Source = (Assets.Shared.KnownAssets.Path.Assets + "/01.png").ToSource()
			}.AttachTo(this.Content);


			this.Options.ForEachNewOrExistingItem(
				NewOption =>
				{
					NewOption.Image.AttachTo(this.Content);
					NewOption.Overlay.AttachTo(this.Overlay);

					NewOption.Click +=
						delegate
						{
							if (this.Sync_ClickOption != null)
								this.Sync_ClickOption(Options.IndexOf(NewOption));
						};
				}
			);

			this.Options.AddRange(
				new Option(
					Brushes.Blue,
					Key.B,
					Assets.Shared.KnownAssets.Path.Assets + "/02.png",
					Assets.Shared.KnownAssets.Path.Sounds + "/4.mp3",
					91, 172, 193, 162
				),

				new Option(
					Brushes.Red,
					Key.R,
					Assets.Shared.KnownAssets.Path.Assets + "/03.png",
					Assets.Shared.KnownAssets.Path.Sounds + "/5.mp3",
					98, 40, 193, 120
				),

				new Option(
					Brushes.Green,
					Key.G,
					Assets.Shared.KnownAssets.Path.Assets + "/04.png",
					Assets.Shared.KnownAssets.Path.Sounds + "/3.mp3",
					309, 44, 190, 114
				),

				new Option(
					Brushes.Yellow,
					Key.Y,
					Assets.Shared.KnownAssets.Path.Assets + "/05.png",
					Assets.Shared.KnownAssets.Path.Sounds + "/1.mp3",
					309, 171, 190, 174
				)
			);


			// we are going for the keyboard input
			// we want to enable the tilde console feature
			this.Overlay.FocusVisualStyle = null;
			this.Overlay.Focusable = true;
			this.Overlay.Focus();


			// at this time we should add a local player
			this.Overlay.MouseLeftButtonDown +=
				(sender, key_args) =>
				{
					this.Overlay.Focus();
				};


			this.Overlay.KeyUp +=
				(sender, key_args) =>
				{
					if (OptionsEnabled)
						Options.FirstOrDefault(k => k.Key == key_args.Key).Apply(k => k.RaiseClick());
				};

			var HappySimonImage = new Image
			{
				Stretch = Stretch.Fill,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Source = (Assets.Shared.KnownAssets.Path.Assets + "/06.png").ToSource(),
				Visibility = Visibility.Hidden
			}.AttachTo(this.Content);




			this.HappySimon = HappySimonImage.ToAnimatedOpacity();


			AttachSocialLinks();



			this.Overlay.MouseMove +=
				(sender, args) =>
				{
					var p = args.GetPosition(this.Overlay);

					if (this.Sync_RemoteOnly_MouseMove != null)
						this.Sync_RemoteOnly_MouseMove(p.X, p.Y);
				};



			this.Score = new TextBox
			{
				AcceptsReturn = true,
				BorderThickness = new Thickness(0),
				TextAlignment = TextAlignment.Right,
				Width = DefaultWidth - 48,
				Height = 60,
				Text = "score: 0\nmy highest: 0",
				Background = Brushes.Transparent,
				Foreground = Brushes.White,
				FontFamily = new FontFamily("Helvetica"),
				FontSize = 18
			}.MoveTo(24, DefaultHeight - 60).AttachTo(this.InfoLayer);


			this.Sync_ClickOption =
				option =>
				{
					var Option = Options.AtModulus(option);

					var n = Simon.Dequeue();

					if (n == Option)
					{

						User.Enqueue(n);
						n.Play(null);

					
						if (Simon.Count == 0)
						{
							MyHighestScore = User.Count.Max(MyHighestScore);
							Score.Text = "score: " + User.Count + "\nmy highest: " + MyHighestScore;

							OptionsEnabled = false;

							HappySimon.Opacity = 1;

							while (User.Count > 0)
							{
								Simon.Enqueue(User.Dequeue());
							}

							GoForward();
						}
					}
					else
					{
						ShowFailure(n);
					}
				};

			this.Sync_SimonOption =
				option =>
				{
					OptionsEnabled = false;

					Simon.Enqueue(Options.AtModulus(option));

					HappySimon.Opacity = 0;

					Simon.ForEach(
						(value, next) =>
						{
							value.Play(
								delegate
								{
									200.AtDelay(next);
								}
							);
						}
					)(
						delegate
						{
							OptionsEnabled = true;
						}
					);

				};

			this.StartThinking();

			this.OptionsEnabled = false;
		}

		private void ShowFailure(Option n)
		{

			OptionsEnabled = false;

			n.Image.Show();

			(Assets.Shared.KnownAssets.Path.Sounds + "/6.mp3").PlaySound();

			var Shake = 400;

			(1000 / DefaultFramerate).AtIntervalWithTimer(
				t =>
				{
					this.Content.MoveTo(
						(Shake.Random() - Shake / 2) * 0.01,
						(Shake.Random() - Shake / 2) * 0.01
					);

					Shake -= 8;

					t.IsEnabled = Shake > 0;
				}
			);

			User.Clear();
			Simon.Clear();

			60.AtDelay(n.Image.Hide);
			120.AtDelay(n.Image.Show);
			200.AtDelay(n.Image.Hide);
			300.AtDelay(n.Image.Show);

			2400.AtDelay(
				delegate
				{
					Score.Text = "score: 0\nmy highest: " + MyHighestScore;
					n.Image.Hide();
					GoForward();
				}
			);
		}


		public readonly Queue<Option> Simon = new Queue<Option>();
		public readonly Queue<Option> User = new Queue<Option>();

		public void GoForward()
		{
			if (LocalIdentityIsPrimate)
			{
				1000.AtDelay(
					delegate
					{
						this.Sync_SimonOption(Options.RandomIndex());
					}
				);
			}
		}

		public void StartGame()
		{
			(Assets.Shared.KnownAssets.Path.Sounds + "/2.mp3").PlaySound();

			GoForward();
		}

		public void Dispose()
		{
		}
	}
}
