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
	partial class SimonCanvas
	{
		[Script]
		public delegate void DelegateMouseMove(double x, double y);
		public DelegateMouseMove Sync_RemoteOnly_MouseMove;
	}
}