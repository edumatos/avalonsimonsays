﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Nonoba.Generic;

namespace AvalonSimonSays.Code.Network.Shared
{
	[Script]
	public partial class VirtualGame : ServerGameBase<Communication.IEvents, Communication.IMessages, VirtualPlayer>
	{

		public void WriteLine(string Text)
		{
			Console.WriteLine("Server > Avalon Ugh: " + Text);
		}


		public override void UserJoined(VirtualPlayer player)
		{
			WriteLine("UserJoined " + player.Username);

			var five = new AvailibleAchievement(player.AwardAchievement, "five");
			var eight = new AvailibleAchievement(player.AwardAchievement, "eight");
			var thirdteen = new AvailibleAchievement(player.AwardAchievement, "thirdteen");
			var twenty = new AvailibleAchievement(player.AwardAchievement, "twenty");
			var granny = new AvailibleAchievement(player.AwardAchievement, "granny");

			//var score = 0;

			player.FromPlayer.Server_SetScore +=
				e =>
				{
					player.SetScore("score", e.score);


					if (e.score >= 5)
						five.Give();

					if (e.score >= 8)
						eight.Give();

					if (e.score >= 13)
						thirdteen.Give();

					if (e.score >= 20)
						twenty.Give();
				};

			int FailCount = 0;
			player.FromPlayer.Server_AddFail +=
				e =>
				{
					FailCount++;

					player.AddScore("fail", 1);

					if (FailCount >= 20)
						granny.Give();
				};


			//player.FromPlayer.AddScore += e =>
			//{
			//    score += e.score;

			//    player.AddScore("score", e.score);

			//    if (score > 100)
			//        a100.Give();

			//    if (score > 1000)
			//        a1000.Give();

			//    if (score > 50000)
			//        a50000.Give();
			//};

			//player.FromPlayer.AwardAchievementLayoutCompleted +=
			//    e =>
			//    {
			//        aLC.GiveMultiple();
			//    };

		

			////var x = AnyOtherUser(player);

			////player.FromPlayer.LockGame += e => this.GameState = MyGame.GameStateEnum.ClosedGameInProgress;
			////player.FromPlayer.UnlockGame += e => this.GameState = MyGame.GameStateEnum.OpenGameInProgress;

			////var total_score = 0;
			////var total_kills = 0;
			////var total_level = 0;

			////// registered nonoba rankings
			////player.FromPlayer.ReportScore +=
			////    e =>
			////    {
			////        if (e.level > 0)
			////            exitfound.Give();

			////        if (e.kills > 0)
			////            firstblood.Give();

			////        if (e.teleports > 0)
			////            portalfound.Give();

			////        total_score += e.score;
			////        total_kills += e.kills;
			////        total_level += e.level;

			////        if (total_score > 2000)
			////            getrich.Give();

			////        if (total_kills > 50)
			////            massacre.Give();

			////        if (total_level > 15)
			////            levelup.Give();

			////        player.AddScore("score", e.score);
			////        player.AddScore("kills", e.kills);
			////        player.AddScore("level", e.level);
			////        player.AddScore("teleports", e.teleports);
			////        player.SetScore("fps", e.fps);
			////    };



			////player.FromPlayer.AwardAchievementFirst += e => player.AwardAchievement("first");
			////player.FromPlayer.AwardAchievementFiver += e => player.AwardAchievement("fiver");
			////player.FromPlayer.AwardAchievementUFOKill += e => player.AwardAchievement("ufokill");
			////player.FromPlayer.AwardAchievementMaxGun += e => player.AwardAchievement("maxgun");

			////var user_with_map = -1;

			////if (x != null)
			////{
			////    user_with_map = x.UserId;
			////}

			//var navbar = 1;
			//var layoutinput = 1;
			//var vote = 1;
			//var hints = 0;

			//if (this.Settings.GetBoolean(SettingsInfo.navbar, false))
			//    navbar = 0;

			//if (this.Settings.GetBoolean(SettingsInfo.layoutinput, false))
			//    layoutinput = 0;

			var turn = 0;

			if (this.Settings.GetBoolean("turn", false))
				turn = 1;

			//if (this.Settings.GetBoolean(SettingsInfo.hints, true))
			//    hints = 1;

			// let new player know how it is named, also send magic bytes to verify
			player.ToPlayer.Server_Hello(
				player.UserId, 
				player.Username, 
				this.Users.Count - 1,
				turn
				//navbar,
				//vote,
				//layoutinput,
				//hints,
				//new Handshake().Bytes
			);

			// let other players know that there is a new player in the map
			player.ToOthers.Server_UserJoined(
			   player.UserId, 
			   player.Username
			);

			//var PreventStatic = 0;

			//player.FromPlayer.ServerPlayerHello +=
			//    e =>
			//    {
			//        var StaticPrevented = PreventStatic;

			//        new Handshake().Verify(e.handshake);
			//    };

			//player.FromPlayer.UserMapResponse +=
			//    e =>
			//    {
			//        var StaticPrevented = PreventStatic;

			//        Console.WriteLine("map: " + e.bytes.Length);

			//    };

		}



		public override void UserLeft(VirtualPlayer player)
		{
			WriteLine("UserLeft " + player.Username);

			player.ToOthers.Server_UserLeft(player.UserId, player.Username);
		}

		public override void GameStarted()
		{
			WriteLine("GameStarted");
		}

	}
}
