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

namespace AvalonSimonSays.Code
{
	[Script]
	public class SimonCanvas : Canvas
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 480;

		public SimonCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			this.ClipToBounds = true;


			var a = new Image
			{
				Stretch = Stretch.Fill,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Source = (Assets.Shared.KnownAssets.Path.Assets + "/01.png").ToSource()
			}.AttachTo(this);

			a.MouseLeftButtonUp +=
				delegate
				{
					(Assets.Shared.KnownAssets.Path.Sounds + "/1.mp3").PlaySound();
				};

		}
	}
}
