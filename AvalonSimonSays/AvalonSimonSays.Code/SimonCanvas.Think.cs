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
using System.Windows.Threading;

namespace AvalonSimonSays.Code
{
	partial class SimonCanvas
	{
		public const int DefaultFramerate = 60;

		void StartThinking()
		{
			var ClientWidth = 400;
			var ClientHeight = 24;

			var StatusText = new TextBox
			{
				Width = ClientWidth,
				Height = ClientHeight,
				Foreground = Brushes.Yellow,
				Background = Brushes.Transparent,
				BorderThickness = new Thickness(0),
				FontFamily = new FontFamily("Courier New"),
				FontSize = 10,
				IsReadOnly = true
			};

			


			StatusText.AttachTo(this.InfoLayer).MoveTo(4, 4);

			ThinkTimer = (1000 / DefaultFramerate).AtInterval(
				delegate
				{
					StatusText.Text = new
					{
						Frame = this.LocalIdentity.SyncFrame,
						Limit = this.LocalIdentity.SyncFrameLimit,
						Simon = this.Simon.Count,
						User = this.User.Count
					}.ToString();
					Think();
				}
			);
		}

		DispatcherTimer ThinkTimer;

		void Think()
		{
			if (this.LocalIdentity.SyncFramePaused)
			{
				if (this.LocalIdentity.SyncFramePausedSkip)
				{
					this.LocalIdentity.SyncFramePausedSkip = false;
				}
				else
				{
					return;
				}
			}

			if (this.LocalIdentity.SyncFrameLimit > 0)
			{
				if (this.LocalIdentity.SyncFrameLimit <= this.LocalIdentity.SyncFrame)
				{
					return;
				}
			}

			
			this.LocalIdentity.SyncFrame++;
		}

	}
}
