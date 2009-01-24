using System;
using AvalonSimonSays.Promotion;
using ScriptCoreLib.Shared.Avalon.Extensions;

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
