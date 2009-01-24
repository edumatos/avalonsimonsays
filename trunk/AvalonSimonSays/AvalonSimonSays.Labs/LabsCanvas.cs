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

namespace AvalonSimonSays.Labs.Shared
{
	using TargetCanvas = AvalonSimonSays.Code.SimonCanvas;

	[Script]
	public class LabsCanvas : Canvas
	{
		public const int DefaultWidth = TargetCanvas.DefaultWidth;
		public const int DefaultHeight = TargetCanvas.DefaultHeight;

		public LabsCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			new TargetCanvas().AttachTo(this).StartGame();

		}
	}
}
