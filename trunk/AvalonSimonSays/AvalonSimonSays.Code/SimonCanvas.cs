﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System.ComponentModel;
using ScriptCoreLib.Shared.Lambda;

namespace AvalonSimonSays.Code
{
	[Script]
	public partial class SimonCanvas : Canvas
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 480;

		public readonly Canvas Content;
		public readonly Canvas Overlay;

		public readonly BindingList<Option> Options = new BindingList<Option>();

		public readonly PlayerIdentity LocalIdentity;

		public bool OptionsEnabled
		{
			set
			{
				Options.ForEach(k => k.Overlay.Show(value));
			}
		}

		public event Action<Option> Click;

		public SimonCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			this.ClipToBounds = true;

			this.Content = new Canvas { Width = DefaultWidth, Height = DefaultHeight }.AttachTo(this);
			this.Overlay = new Canvas { Width = DefaultWidth, Height = DefaultHeight }.AttachTo(this);

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
							if (this.Click != null)
								this.Click(NewOption);
						};
				}
			);

			this.Options.AddRange(
				new Option(
					Brushes.Blue,
					Assets.Shared.KnownAssets.Path.Assets + "/02.png",
					Assets.Shared.KnownAssets.Path.Sounds + "/4.mp3",
					91, 172, 193, 162
				),

				new Option(
					Brushes.Red,
					Assets.Shared.KnownAssets.Path.Assets + "/03.png",
					Assets.Shared.KnownAssets.Path.Sounds + "/5.mp3",
					98, 40, 193, 120
				),

				new Option(
					Brushes.Green,
					Assets.Shared.KnownAssets.Path.Assets + "/04.png",
					Assets.Shared.KnownAssets.Path.Sounds + "/3.mp3",
					309, 44, 190, 114
				),

				new Option(
					Brushes.Yellow,
					Assets.Shared.KnownAssets.Path.Assets + "/05.png",
					Assets.Shared.KnownAssets.Path.Sounds + "/1.mp3",
					309, 171, 190, 174
				)
			);

			var HappySimonImage = new Image
			{
				Stretch = Stretch.Fill,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Source = (Assets.Shared.KnownAssets.Path.Assets + "/06.png").ToSource(),
				Visibility = Visibility.Hidden
			}.AttachTo(this.Content);

			var HappySimon = HappySimonImage.ToAnimatedOpacity();



			(Assets.Shared.KnownAssets.Path.Sounds + "/2.mp3").PlaySound();


			var Simon = new Queue<Option>();
			var User = new Queue<Option>();

			Action GoForward =
				delegate
				{
					OptionsEnabled = false;

					Simon.Enqueue(Options.Random());

					1000.AtDelay(
						delegate
						{
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
						}
					);
				};

			GoForward();

			this.Click +=
				Option =>
				{

					var n = Simon.Dequeue();

					if (n == Option)
					{
						User.Enqueue(n);
						n.Play(null);

						if (Simon.Count == 0)
						{
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
						OptionsEnabled = false;

						n.Image.Show();

						(Assets.Shared.KnownAssets.Path.Sounds + "/6.mp3").PlaySound();

						User.Clear();
						Simon.Clear();

						1700.AtDelay(
							delegate
							{
								n.Image.Hide();
								GoForward();
							}
						);
					}

					
				};
		}
	}
}