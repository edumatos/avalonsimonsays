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
using AvalonSimonSays.Promotion;

namespace AvalonSimonSays.Code
{
	partial class SimonCanvas
	{

		private void AttachSocialLinks()
		{
			var SocialLinks = new GameSocialLinks(this)
				{
					new GameSocialLinks.Button { 
						Source = (Assets.Shared.KnownAssets.Path.Assets + "/plus_google.png").ToSource(),
						Width = 62,
						Height = 17,
						Hyperlink = new Uri(Info.GoogleGadget.AddLink)
					},
					new GameSocialLinks.Button { 
						Source = (Assets.Shared.KnownAssets.Path.Assets + "/su.png").ToSource(),
						Width = 16,
						Height = 16,
						Hyperlink = new Uri( "http://www.stumbleupon.com/submit?url=" + Info.Nonoba.URL)
					}
				};
		}

	}
}
