﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using Nonoba.GameLibrary;
using ScriptCoreLib.Shared.Nonoba.Generic;
using AvalonSimonSays.Code.Network.Shared;

namespace AvalonSimonSays.Code.Network.Server
{
	[Script]
	public class NonobaGameUser<TVirtualPlayer> : NonobaGameUser
	{
		public TVirtualPlayer Virtual { get; set; }

		public override Dictionary<string, string> GetDebugValues()
		{
			return new Dictionary<string, string> { };
		}
	}

	//[GameSetup.Boolean(
	//    "mojo",
	//    "Enable mojo",
	//    "Enable mojo in game",
	//    false)]

	[GameSetup.Boolean(
	    "turn",
	    "Disable turn mode",
	    "Players can play without waiting their turn",
	    false)]

	[Script]
	public class NonobaGame : NonobaGame<NonobaGameUser<VirtualPlayer>>
	{
		// this c# server code is going through jsc to
		// assemble a single assembly.

		// this must be a direct inheritance to get it working 

		public VirtualGame Virtual;


		public override void GameStarted()
		{
			Virtual = new VirtualGame
			{
				AtDelay = (h, i) => this.ScheduleCallback(() => h(), i).Stop,
				AtInterval = (h, i) => this.AddTimer(() => h(), i).Stop,
				Settings = new ServerGameSettings
				{
					GetBoolean = (e, v) => this.Setup.GetBoolean(e),
					GetInteger = (e, v) => this.Setup.GetInteger(e),
					GetOption = (e, v) => this.Setup.GetOption(e),
					GetString = (e, v) => this.Setup.GetString(e)
				}
			};

			var StateMap = new Dictionary<VirtualGame.GameStateEnum, NonobaGameState>
            {
                { VirtualGame.GameStateEnum.ClosedGameInProgress, NonobaGameState.ClosedGameInProgress },
                { VirtualGame.GameStateEnum.OpenGameInProgress, NonobaGameState.OpenGameInProgress },
                { VirtualGame.GameStateEnum.WaitingForPlayers, NonobaGameState.WaitingForPlayers },
            };

			Virtual.GameStateChanged +=
				delegate
				{
					this.SetState(StateMap[Virtual.GameState]);
				};


			// You can explicitly setup how many users are allowed in your game.
			MaxUsers = 4;

			Virtual.GameStarted();
		}

		public override void GameClosed()
		{
			Virtual.GameClosed();
			Virtual = null;
		}


		public override void GotMessage(NonobaGameUser<VirtualPlayer> user, Message message)
		{
			var e = /*(SharedClass1.Messages)*/int.Parse(message.Type);

			var p = user.Virtual;

			Converter<uint, byte[]> GetMemoryStream =
				index => message.GetByteArray(index);

			if (p.FromPlayerDispatch.DispatchInt32(e,
					new Communication.RemoteEvents.DispatchHelper
					{
						GetLength = i => (int)message.Count,
						GetInt32 = message.GetInt,
						GetDouble = message.GetDouble,
						GetString = message.GetString,
						GetMemoryStream = GetMemoryStream
					}
				))
				return;

			Console.WriteLine("Not on dispatch: " + message.Type);
		}

		public override void UserJoined(NonobaGameUser<VirtualPlayer> user)
		{
			var FromPlayer =
				  new Communication.RemoteEvents
				  {
					  BroadcastRouter = new Communication.RemoteEvents.WithUserArgumentsRouter_Broadcast
					  {
						  user = user.UserId,
					  },
					  SinglecastRouter = new Communication.RemoteEvents.WithUserArgumentsRouter_Singlecast
					  {
						  user = user.UserId,
					  }
				  };

			user.Virtual = new VirtualPlayer
			{
				ToOthers =
					new Communication.RemoteMessages
					{
						Send = q => this.SendOthers(user.UserId, q.i, q.args)
					},
				ToPlayer =
					new Communication.RemoteMessages
					{
						Send = e => this.Send(user, e.i, e.args)
					},
				FromPlayer = FromPlayer,
				FromPlayerDispatch = FromPlayer,
				UserId = user.UserId,
				Username = user.Username,

				AddScore = user.SubmitRankingDelta,
				SetScore = user.SetRankingScore,

				AddHighscore = user.SubmitHighscore,

				AwardAchievement = user.AwardAchievement,

				GetData = user.GetData,
				SetData = user.SetData
			};


			
			FromPlayer.BroadcastRouter.Target = user.Virtual.ToOthers;

			FromPlayer.SinglecastRouter.Target =
				target_user =>
				{
					var r = default(Communication.IMessages);

					foreach (var v in this.Users)
					{
						if (v.UserId == target_user)
						{
							r = v.Virtual.ToPlayer;

							break;
						}

					}
					return r;
				};

			Virtual.Users.Add(user.Virtual);
			Virtual.UserJoined(user.Virtual);
			user.Virtual.UserJoined();

		}

		public override void UserLeft(NonobaGameUser<VirtualPlayer> user)
		{
			this.Virtual.Users.Remove(user.Virtual);
			this.Virtual.UserLeft(user.Virtual);
			user.Virtual.UserLeft();
			user.Virtual = null;

		}

		#region Send
		private void Send(NonobaGameUser<VirtualPlayer> v, Communication.Messages type, params object[] e)
		{
			var MessageType = ((int)type).ToString();


			v.Send(MessageType, e);
		}

		private void Send(int id, Communication.Messages type, params object[] e)
		{
			foreach (var v in Users)
			{
				if (v.UserId == id)
					Send(v, type, e);
			}
		}

		private void SendOthers(int id, Communication.Messages type, params object[] e)
		{
			foreach (var v in Users)
			{
				if (v.UserId != id)
					Send(v, type, e);
			}
		}


		#endregion


	}
}
