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
		public void Message(string Text)
		{
			var Height = 40;

			var Car = new Canvas
			{
				Width = DefaultWidth,
				Height = Height
			};

			Action<double, double> AddFill =
				(edge, opacity) =>
				{
					var _x = DefaultWidth * edge;

					new Rectangle
					{
						Width = DefaultWidth - _x * 2,
						Height = Height,
						Fill = Brushes.Black,
						Opacity = opacity
					}.AttachTo(Car).MoveTo(_x, 0);
				};

			for (int i = 1; i < 16; i++)
			{
				AddFill(0.3 + 0.01 * i, 0.005 * i);
			}

			var CarText = new TextBox
			{
				BorderThickness = new Thickness(0),
				Background = Brushes.Transparent,
				Foreground = Brushes.White,
				Width = DefaultWidth,
				Height = Height - 8,
				TextAlignment = TextAlignment.Center,
				FontFamily = new FontFamily("Helvetica"),
				FontSize = 18,
				Text = Text
			}.AttachTo(Car).MoveTo(0, 8);


			double x = DefaultWidth;
			double y = (DefaultHeight - Height) / 2;

			Car.MoveTo(x, y);
			Car.AttachTo(this.InfoLayer);

			(1000 / DefaultFramerate).AtIntervalWithTimer(
				t =>
				{
					x -= Math.Abs(x * 0.1).Max(0.5);

					Car.MoveTo(x, y);


					if (x < -DefaultWidth)
					{
						t.Stop();

						Car.Orphanize();
					}
				}
			);
		}
	}
}
