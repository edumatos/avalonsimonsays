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
using ScriptCoreLib.Shared.Avalon.Cursors;
using ScriptCoreLib.Shared.Avalon.Tween;

namespace AvalonSimonSays.Code
{
	partial class SimonCanvas
	{
		[Script]
		public class Arrow : ArrowCursorControl
		{
			public PlayerIdentity Identity;

			public Action<double, double> AnimatedMoveTo;

			public Arrow()
			{
				this.AnimatedMoveTo = NumericEmitter.OfDouble((x, y) => this.MoveContainerTo(Convert.ToInt32(x), Convert.ToInt32(y)));
			}
		}

		public readonly BindingList<Arrow> Arrows = new BindingList<Arrow>();

	}
}
