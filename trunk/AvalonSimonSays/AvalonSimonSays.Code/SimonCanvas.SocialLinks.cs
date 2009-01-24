using System;
using AvalonSimonSays.Promotion;
using ScriptCoreLib.Shared.Avalon.Extensions;
using AvalonSimonSays.Assets.Shared;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

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

			// redefine the ctor to fit our context
			Func<string, string, string, GameMenu.Option> Option =
				(Text, Image, href) =>
					new GameMenu.Option
					{
						Text = "Play " + Text + "!",
						Source = (KnownAssets.Path.SocialLinks + "/" + Image + ".png").ToSource(),
						Hyperlink = new Uri(href),
						MarginAfter = Math.PI / 3
					};

			var ShadowSize = 40;

			new TextBox
			{
				Text = "More games here! ;)",
				FontFamily = new FontFamily("Helvetica"),
				FontSize = 10,
				Background = Brushes.Transparent,
				Foreground = Brushes.Blue,
				Width = DefaultWidth,
				Height = 20,
				BorderThickness = new Thickness(0),
			}.AttachTo(this.InfoLayer).MoveTo(3, 3);

			var Menu = new GameMenu(DefaultWidth, DefaultHeight, ShadowSize)
			{
				Option("FreeCell", "Preview_FreeCell",  "http://nonoba.com/zproxy/avalon-freecell"),
				Option("Spider Solitaire", "Preview_Spider",  "http://nonoba.com/zproxy/avalon-spider-solitaire"),
				Option("Treasure Hunt", "Preview_TreasureHunt",  "http://nonoba.com/zproxy/treasure-hunt"),
				Option("FlashMinesweeper:MP", "Preview_Minesweeper", "http://nonoba.com/zproxy/flashminesweepermp"),
				Option("Multiplayer Mahjong", "Preview_Mahjong", "http://nonoba.com/zproxy/mahjong-multiplayer"),
				Option("Multiplayer SpaceInvaders", "Preview_SpaceInvaders", "http://nonoba.com/zproxy/flashspaceinvaders"),
			};

			Menu.AttachContainerTo(this);
			//Menu.Show();



		}

	}
}
