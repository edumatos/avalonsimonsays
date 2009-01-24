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

		PlayerIdentity InternalActiveIdentity;

		public PlayerIdentity ActiveIdentity
		{
			get
			{
				return InternalActiveIdentity;
			}
			set
			{
				InternalActiveIdentity = value;

				if (value != null)
					if (value == LocalIdentity)
					{
						if (this.CoPlayers.Count > 0)
							this.Message("your turn!");
					}
					else
						this.Message(value.Name + " is now playing");
			}
		}

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

		public PlayerIdentity PrimateIdentity
		{
			get
			{
				var id = this.AllPlayers.Min(k => k.Number);

				return this.AllPlayers.Single(k => k.Number == id);
			}
		}

		public bool LocalIdentityIsPrimate
		{
			get
			{
				return this.AllPlayers.Min(k => k.Number) == this.LocalIdentity.Number;
			}
		}

		int InternalMyHighestScore;
		public int MyHighestScore
		{
			get
			{
				return InternalMyHighestScore;
			}
			set
			{
				InternalMyHighestScore = value;

				if (MyHighestScoreChanged != null)
					MyHighestScoreChanged();
			}
		}

		public event Action MyHighestScoreChanged;

		public readonly TextBox Score;

		public event Action StatisticsAddFail;

		public SimonCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;
			Background = Brushes.Black;

			this.CoPlayers.ForEachItemDeleted(
				DeletedPlayer =>
				{
					if (DeletedPlayer == ActiveIdentity)
						ActiveIdentity = PrimateIdentity;
				}
			);

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
							if (ActiveIdentity != LocalIdentity)
							{
								if (ActiveIdentity != null)
									this.Message(ActiveIdentity.Name + " is now playing, wait!");

								return;
							}

							if (this.Sync_ClickOption != null)
								this.Sync_ClickOption(LocalIdentity.Number, Options.IndexOf(NewOption));
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


			this.CoPlayers.ForEachNewOrExistingItem(
				Player => Message(Player.Name + " has joined!")
			);

			this.CoPlayers.ForEachItemDeleted(
				Player => Message(Player.Name + " has left!")
			);

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
				(user, option) =>
				{
					if (Simon.Count == 0)
					{
						Message("desync!");
						return;
					}

					HappySimon.Opacity = 0;

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

							this.LocalIdentity.HandleFutureFrame(
								DefaultFramerate / 2,
								GoForward
							);

							if (this.LocalIdentity == this.ActiveIdentity)
								if (this.Sync_RemoteOnly_SetActive != null)
									this.Sync_RemoteOnly_SetActive();
						}
					}
					else
					{
						if (user == LocalIdentity.Number)
							if (StatisticsAddFail != null)
								StatisticsAddFail();

						ShowFailure(n);

						if (this.LocalIdentity == this.ActiveIdentity)
							if (this.Sync_RemoteOnly_SetActive != null)
								this.Sync_RemoteOnly_SetActive();
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
									this.LocalIdentity.HandleFutureFrame(
										DefaultFramerate / 2,
										next
									);
								}
							);
						}
					)(
						delegate
						{
							HappySimon.Opacity = 1;
							OptionsEnabled = true;

							this.LocalIdentity.HandleFutureFrame(
								DefaultFramerate / 2,
								() => HappySimon.Opacity = 0
							);
						}
					);

				};

			this.StartThinking();

			this.OptionsEnabled = false;

		}

		private void ShowFailure(Option n)
		{
			var FailureText =
@"Awww....
So close!
Yikes!
Miss!
Wasted!
Head shot!
Overkill!
Too bad!
You fail!
¤#%&!
Dang!
Verdamt!
You silly!
Why did you do that?
No, the other left!
Now what?
Will you ever learn?
Atleast try!
Sissy!
Come on!
Consentrate!
Think!
Watch it!
Try harder!
Utter failure!
Excellent... Not!
Why would you do that?
Try not to loose!
Suck less, please!
A real winner!
Looser!
Will ya?
What?!?
";

			if (LocalIdentity == ActiveIdentity)
				Message(FailureText.Split(Environment.NewLine).Random());

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

			this.LocalIdentity.HandleFutureFrame(
				2 * DefaultFramerate,
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
				this.LocalIdentity.HandleFutureFrame(
					1000 / DefaultFramerate,
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


			this.Message(Promotion.Info.Description);

		}

		public void Dispose()
		{
		}
	}
}
