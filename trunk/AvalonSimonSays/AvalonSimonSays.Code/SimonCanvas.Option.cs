using System;
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
	partial class SimonCanvas
	{
		[Script]
		public class Option
		{
			public readonly Image Image;
			public readonly Rectangle Overlay;

			public readonly Action<Action> Play;

			public event Action Click;

			public Option(Brush Color, string Image, string Sound, int Left, int Top, int Width, int Height)
			{
				this.Image = new Image
				{
					Stretch = Stretch.Fill,
					Width = DefaultWidth,
					Height = DefaultHeight,
					Source = Image.ToSource(),
					Visibility = Visibility.Hidden
				};

				this.Overlay = new Rectangle
				{
					Fill = Color,
					Opacity = 0,
					Cursor = Cursors.Hand
				}.MoveTo(Left, Top).SizeTo(Width, Height);

				Overlay.MouseLeftButtonDown +=
					delegate
					{
						if (Click != null)
							Click();
					};

				this.Play =
					done =>
					{
						this.Image.Show();

						Sound.PlaySound();

						300.AtDelay(
							delegate
							{
								this.Image.Hide();

								if (done != null)
									done();
							}
						);
					};
			}
		}

	}
}
