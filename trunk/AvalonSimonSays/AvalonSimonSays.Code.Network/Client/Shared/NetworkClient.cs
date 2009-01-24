﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.ComponentModel;
using System.Diagnostics;
using AvalonSimonSays.Code.Network.Shared;

namespace AvalonSimonSays.Code.Network.Client.Shared
{



	[Script]
	public class NetworkClient : VirtualClient, ISupportsContainer
	{
		// this code is shared between
		// javascript, actionscript, c#

		// read about network programming:
		// http://trac.bookofhook.com/bookofhook/trac.cgi/wiki/GameDesign
		// http://www.jakeworld.org/JakeWorld/main.php?main=Articles/Networking%20for%20Games%20101.php
		// http://trac.bookofhook.com/bookofhook/trac.cgi/wiki/Quake3Networking
		// http://trac.bookofhook.com/bookofhook/trac.cgi/wiki/IntroductionToMultiplayerGameProgramming



		public const int DefaultWidth = SimonCanvas.DefaultWidth;
		public const int DefaultHeight = SimonCanvas.DefaultHeight;


		public Canvas Container { get; set; }

		public SimonCanvas Content;

		/// <summary>
		/// state:String - Defines the state of the request using the values of the NonobaAPI's Public Constants
		/// success:Boolean - If true, the user bought the item.
		/// </summary>
		public Action<string, Action<string, bool>> ShowShop;

		public NetworkClient()
		{
			this.Container = new Canvas
			{
				Background = Brushes.Black,
				Width = DefaultWidth,
				Height = DefaultHeight
			};



		}

		public void Connect()
		{
			//Content.Console.WriteLine("Connect");

			IsConnected = true;
		}

		public bool IsConnected;

		public void Disconnect()
		{
			//Content.Console.WriteLine("Disconnect");

			IsConnected = false;
		}

		public BindingList<PlayerIdentity> CoPlayers;

		public IEnumerable<PlayerIdentity> AllPlayers
		{
			get
			{
				return CoPlayers.ConcatSingle(this.Content.LocalIdentity);
			}
		}

		public void InitializeEvents()
		{
			#region CoPlayers

			// coplayers are remoted locals
			this.CoPlayers = new BindingList<PlayerIdentity>();
			this.CoPlayers.ForEachNewItem(
				c =>
				{

				}
			);

			this.CoPlayers.ForEachItemDeleted(
				c =>
				{
				
				}
			);

			#endregion

			Content = new SimonCanvas(
	
			).AttachTo(this);

			//Content.Console.WriteLine("binding to shop menu item");

			//Content.Lobby.Menu.Shop +=
			//    delegate
			//    {
			//        Content.Console.WriteLine("invoking shop");

			//        if (this.ShowShop != null)
			//            this.ShowShop("saveten",
			//                delegate
			//                {
			//                    // and reaction to the shop activity
			//                    Content.Console.WriteLine("invoking shop done");
			//                }
			//            );
			//    };

			//Content.Console.WriteLine("InitializeEvents");

			var Server_Hello_UserSynced = new BindingList<PlayerIdentity>();

			this.Events.Server_Hello +=
				e =>
				{
					// yay, the server tells me my name. lets atleast remember it.
					this.Content.LocalIdentity.Number = e.user;
					this.Content.LocalIdentity.Name = e.name;

					//Content.Console.WriteLine("Server_Hello " + e);

					// we have joined the server
					// now we need to sync up the frames
					// if we are alone in the server we do not sync yet
					if (e.others == 0)
					{
						// we do not have to sync to others
						this.Content.LocalIdentity.SyncFramePaused = false;
					}
					else
					{
						Server_Hello_UserSynced.ForEachNewItem(
							delegate
							{
								if (Server_Hello_UserSynced.Count == e.others)
								{
									// we know everybody now
									var NextSyncFrame = Server_Hello_UserSynced.Min(k => k.SyncFrame);

									//this.Content.Console.WriteLine("synced and ready to unpause from frame " + NextSyncFrame);
									this.Content.LocalIdentity.SyncFrame = NextSyncFrame;

									// unpause
									this.Content.LocalIdentity.SyncFramePaused = false;
								}
							}
						);
					}
				};


			#region Server_UserJoined
			this.Events.Server_UserJoined +=
				e =>
				{
					//Content.Console.WriteLine("Server_UserJoined " + new { e, this.Content.LocalIdentity.SyncFrame });
					var EgoIsPrimate = this.AllPlayers.Min(k => k.Number) == this.Content.LocalIdentity.Number;

					this.Messages.UserHello(
						e.user,
						this.Content.LocalIdentity.Name,
						this.Content.LocalIdentity.SyncFrame
					);

					var LowestSyncFrame = this.Content.LocalIdentity.SyncFrame;

					if (this.CoPlayers.Any())
						LowestSyncFrame = this.CoPlayers.Min(k => k.SyncFrame) - this.Content.LocalIdentity.SyncFrameWindow;

					this.CoPlayers.Add(
						new PlayerIdentity
						{
							Name = e.name,
							Number = e.user,
							// that new client is paused
							// we need to run out of frames in order to pause correctly
							SyncFrame = LowestSyncFrame
						}
					);



					// the new player needs to be synced
					// lets pause for now to figure out how to do that

					var NextSyncFrameLimit = this.CoPlayers.Min(k => k.SyncFrame) + this.Content.LocalIdentity.SyncFrameWindow;
					// we can only increase the limiter
					this.Content.LocalIdentity.SyncFrameLimit = this.Content.LocalIdentity.SyncFrameLimit.Max(NextSyncFrameLimit);


					if (this.Content.LocalIdentity.SyncFrame == this.Content.LocalIdentity.SyncFrameLimit)
					{
						//Content.Console.WriteLine("will sync to new joined client at frame " + this.Content.LocalIdentity.SyncFrame);
					}
					else
					{
						//Content.Console.WriteLine("new client joined at frame " + this.Content.LocalIdentity.SyncFrame);
						//Content.Console.WriteLine("will sync to that client at future frame " + this.Content.LocalIdentity.SyncFrameLimit);

					}

					this.Content.LocalIdentity.HandleFrame(
						this.Content.LocalIdentity.SyncFrameLimit,
						delegate
						{
							// this event happens at a later timepoint
							// if someone joins after us
							// there is some catching up to do
							// like we need to tell it about our locals

							

							if (EgoIsPrimate)
							{

								
							}

							this.Messages.UserSynced(
								e.user,
								this.Content.LocalIdentity.SyncFrame
							);

							//this.Content.Console.WriteLine("syncing to new client done at frame " +
							//    this.Content.LocalIdentity.SyncFrame + " with limiter " +
							//    this.Content.LocalIdentity.SyncFrameLimit);



						}
					);
				};
			#endregion

			this.Events.UserHello +=
				e =>
				{
					//Content.Console.WriteLine("UserHello " + e);

					this.CoPlayers.Add(
						new PlayerIdentity
						{
							Name = e.name,
							Number = e.user,
							SyncFrame = e.frame
						}
					);

					if (!this.Content.LocalIdentity.SyncFramePaused)
					{
						//this.Content.Console.WriteLine("error: got UserHello while unpaused");
					}

				};

			this.Events.UserSynced +=
				e =>
				{
					var c = this[e.user];

					Server_Hello_UserSynced.Add(c);
				};

			this.Events.Server_UserLeft +=
				e =>
				{
					var c = this[e.user];

					//Content.Console.WriteLine("Server_UserLeft " + e + " - " + c);

					this.CoPlayers.Remove(c);

					// if we are again alone on the server
					// and we are not in sync 
					// we can just proceed as we do not need to sync
					if (this.CoPlayers.Count == 0)
					{
						this.Content.LocalIdentity.SyncFramePaused = false;
						this.Content.LocalIdentity.SyncFrameLimit = 0;
					}
					else
					{
						this.Content.LocalIdentity.SyncFrameLimit = this.CoPlayers.Min(k => k.SyncFrame) + this.Content.LocalIdentity.SyncFrameWindow;
					}
				};



			#region broadcast current frame
			this.Content.LocalIdentity.SyncFrameChanged +=
				delegate
				{
					this.Messages.SyncFrame(
						this.Content.LocalIdentity.SyncFrame,
						0
					);
				};

			this.Events.UserSyncFrameEcho +=
				e =>
				{
					var c = this[e];

					if (c == null)
						return;

					//c.SyncFrameLatency = this.Content.LocalIdentity.SyncFrame - e.frame;
				};

			this.Events.UserSyncFrame +=
				e =>
				{
					var c = this[e];

					if (c == null)
					{
						// we do not know yet about this user
						return;
					}

					c.SyncFrame = e.frame;


					// if we are paused we will not try to recalculate our new limit
					var NextSyncFrameLimit = this.CoPlayers.Min(k => k.SyncFrame) + this.Content.LocalIdentity.SyncFrameWindow;
					// we can only increase the limiter
					this.Content.LocalIdentity.SyncFrameLimit = this.Content.LocalIdentity.SyncFrameLimit.Max(NextSyncFrameLimit);


					// lets send the same data back to calculate lag
					this.Messages.UserSyncFrameEcho(e.user, e.frame, 0);
				};
			#endregion



			//this.Events.UserKeyStateChanged +=
			//    e =>
			//    {
			//        var c = this[e.user];

			//        this.Content.LocalIdentity.HandleFrame(e.frame,
			//            delegate
			//            {
			//                var p = c[e.local];

			//                // if the remote frame is less than here
			//                // then we are in the future
			//                // otherwise they are in the future
			//                var key = p.Input.Keyboard.FromDefaultTranslation((Key)e.key);
			//                var state = Convert.ToBoolean(e.state);



			//                p.Input.Keyboard.KeyState[key] = state;
			//            },
			//            delegate
			//            {
			//                this.Content.Console.WriteLine("UserKeyStateChanged desync " + e);
			//            }
			//        );
			//    };

			//#region UserMouseMove
			//this.Content.Sync_RemoteOnly_MouseMove =
			//    (int port, double x, double y) =>
			//    {
			//        this.Messages.MouseMove(port, x, y);
			//    };

			//this.Events.UserMouseMove +=
			//    e =>
			//    {
			//        var c = this[e];

			//        // e.port could be used to select a specific editor window
			//        // for now we ignore it

			//        var a = this.Content.Editor.Arrows.SingleOrDefault(k => k.Identity == c);

			//        if (a == null)
			//        {
			//            a = new Workspace.EditorPort.Arrow
			//            {
			//                Identity = c
			//            };

			//            this.Content.Editor.Arrows.Add(a);

			//            this.CoPlayers.ForEachItemDeleted(
			//                DeletedIdentity =>
			//                {
			//                    if (DeletedIdentity == c)
			//                        this.Content.Editor.Arrows.Remove(a);
			//                }
			//            );
			//        }

			//        a.AnimatedMoveTo(e.x, e.y);
			//    };
			//#endregion

			//#region UserLoadLevelHint
			//this.Content.Sync_RemoteOnly_LoadLevelHint =
			//    (int port) =>
			//    {
			//        this.Messages.LoadLevelHint(port);
			//    };

			//this.Events.UserLoadLevelHint +=
			//    e =>
			//    {
			//        if (this.Content.CurrentPort.PortIdentity == e.port)
			//            this.Content.CurrentPort.Window.ColorOverlay.Opacity = 1;
			//        else
			//            this.Content.BackgroundLoading.Show();
			//    };
			//#endregion


			//#region Sync_LoadLevel
			//var Sync_LoadLevel = this.Content.Sync_LoadLevel;

			//this.Events.UserLoadLevel +=
			//    e =>
			//    {
			//        var c = this[e.user];

			//        this.Content.LocalIdentity.HandleFrame(e.frame,
			//            delegate
			//            {
			//                Sync_LoadLevel(e.port, e.level, e.custom);
			//            },
			//            delegate
			//            {
			//                this.Content.Console.WriteLine("UserTeleportTo desync " + e);
			//            }
			//        );
			//    };

			//this.Content.Sync_LoadLevel =
			//    (int port, int level, string custom) =>
			//    {
			//        var FutureFrame = this.Content.LocalIdentity.HandleFutureFrame(
			//            delegate
			//            {
			//                // do a local teleport in the future
			//                Sync_LoadLevel(port, level, custom);
			//            }
			//        );

			//        this.Messages.LoadLevel(FutureFrame, port, level, custom);
			//    };
			//#endregion

			//#region Sync_EditorSelector
			//var Sync_EditorSelector = this.Content.Sync_EditorSelector;

			//this.Content.Sync_EditorSelector =
			//    (int port, int type, int size, int x, int y) =>
			//    {
			//        var FutureFrame = this.Content.LocalIdentity.HandleFutureFrame(
			//            delegate
			//            {
			//                // do a local teleport in the future
			//                Sync_EditorSelector(port, type, size, x, y);
			//            }
			//        );

			//        this.Messages.EditorSelector(FutureFrame, port, type, size, x, y);
			//    };

			//this.Events.UserEditorSelector +=
			//    e =>
			//    {
			//        var c = this[e.user];

			//        this.Content.LocalIdentity.HandleFrame(e.frame,
			//            delegate
			//            {
			//                Sync_EditorSelector(e.port, e.type, e.size, e.x, e.y);
			//            },
			//            delegate
			//            {
			//                this.Content.Console.WriteLine("UserEditorSelector desync " + e);
			//            }
			//        );
			//    };
			//#endregion


			//#region networked Sync_TeleportTo
			//// save the local implementation
			//var Sync_TeleportTo = this.Content.Sync_TeleportTo;

			//this.Events.UserTeleportTo +=
			//    e =>
			//    {
			//        var c = this[e.user];

			//        this.Content.LocalIdentity.HandleFrame(e.frame,
			//            delegate
			//            {
			//                Sync_TeleportTo(c.Locals, e.port, e.local, e.x, e.y, e.vx, e.vy);
			//            },
			//            delegate
			//            {
			//                this.Content.Console.WriteLine("UserTeleportTo desync " + e);
			//            }
			//        );
			//    };

			//this.Content.Sync_TeleportTo =
			//    (BindingList<PlayerInfo> a, int port, int local, double x, double y, double vx, double vy) =>
			//    {
			//        var FutureFrame = this.Content.LocalIdentity.HandleFutureFrame(
			//            delegate
			//            {
			//                // do a local teleport in the future
			//                Sync_TeleportTo(a, port, local, x, y, vx, vy);
			//            }
			//        );

			//        this.Messages.TeleportTo(FutureFrame, local, port, x, y, vx, vy);
			//    };

			//#endregion

			//#region Sync_RemoveLocalPlayer
			//var Sync_RemoveLocalPlayer = this.Content.Sync_RemoveLocalPlayer;

			//this.Events.UserRemoveLocalPlayer +=
			//    e =>
			//    {
			//        var c = this[e.user];

			//        this.Content.LocalIdentity.HandleFrame(e.frame,
			//            delegate
			//            {
			//                Sync_RemoveLocalPlayer(c.Locals, e.local);
			//            },
			//            delegate
			//            {
			//                this.Content.Console.WriteLine("UserRemoveLocalPlayer desync " + e);
			//            }
			//        );
			//    };

			//this.Content.Sync_RemoveLocalPlayer =
			//    (BindingList<PlayerInfo> a, int local) =>
			//    {
			//        var FutureFrame = this.Content.LocalIdentity.HandleFutureFrame(
			//            delegate
			//            {
			//                // do a local teleport in the future
			//                Sync_RemoveLocalPlayer(a, local);
			//            }
			//        );

			//        this.Messages.RemoveLocalPlayer(FutureFrame, local);
			//    };
			//#endregion


			//this.Content.LocalIdentity.Locals.ForEachNewOrExistingItem(
			//    Local =>
			//    {
			//        // ... while member apply this rule

			//        var ConnectedKeyboard = Local.Input.Keyboard;
			//        var LatencyKeyboard = new KeyboardInput(new KeyboardInput.Arguments.Arrows());

			//        Local.Input.Keyboard = LatencyKeyboard;

			//        // sending local ingame player keystates
			//        Action<Key, bool> Local_KeyStateChanged =
			//            (key, state) =>
			//            {
			//                //var FutureFrame = this.Content.LocalIdentity.SyncFrame + this.Content.LocalIdentity.SyncFrameWindow;

			//                var FutureFrame = this.Content.LocalIdentity.HandleFutureFrame(0,
			//                    delegate
			//                    {
			//                        LatencyKeyboard.KeyState[
			//                            LatencyKeyboard.FromDefaultTranslation(
			//                                ConnectedKeyboard.ToDefaultTranslation(key)
			//                            )
			//                        ] = state;
			//                    },
			//                    delegate
			//                    {
			//                        // can we be desynced?
			//                    }
			//                );


			//                this.Messages.KeyStateChanged(
			//                    Local.IdentityLocal,
			//                    FutureFrame,
			//                    (int)ConnectedKeyboard.ToDefaultTranslation(key),
			//                    Convert.ToInt32(state)
			//                );
			//            };


			//        this.Content.Console.WriteLine("event add KeyStateChanged " + Local);
			//        ConnectedKeyboard.KeyStateChanged += Local_KeyStateChanged;

			//        // when do we want to stop broadcasting our key changes?
			//        // maybe when we remove that local player

			//        this.Content.LocalIdentity.Locals.ForEachItemDeleted(
			//            (DeletedLocal, Dispose) =>
			//            {
			//                if (DeletedLocal != Local)
			//                    return;

			//                /// we should delete our copies on the net too...

			//                this.Content.Console.WriteLine("event remove KeyStateChanged " + Local);
			//                ConnectedKeyboard.KeyStateChanged -= Local_KeyStateChanged;

			//                DeletedLocal.Input.Keyboard = ConnectedKeyboard;

			//                // we should not listen to that event anymore
			//                Dispose();


			//            }
			//        );
			//    }
			//);

			//this.Content.LocalIdentity.Locals.ForEachItemDeleted(
			//    Local => this.Messages.LocalPlayers_Decrease(0)
			//);

			//#endregion

			//#region EditorSelectorApplied

			//this.Content.View.EditorSelectorDisabled = true;

			//this.Content.View.EditorSelectorApplied +=
			//    (Selector, Position) =>
			//    {
			//        var FutureFrame = this.Content.LocalIdentity.SyncFrame + this.Content.LocalIdentity.SyncFrameWindow;

			//        this.Content.LocalIdentity.HandleFrame(FutureFrame,
			//            delegate
			//            {
			//                Selector.CreateTo(this.Content.View.Level, Position);
			//            }
			//        );

			//        var Index = KnownSelectors.Index.Of(Selector, this.Content.Selectors);

			//        // unknown selector
			//        if (Index.Type == -1)
			//            return;

			//        this.Messages.EditorSelector(FutureFrame, Index.Type, Index.Size, Position.ContentX, Position.ContentY);
			//    };

			//this.Events.UserEditorSelector +=
			//    e =>
			//    {
			//        Content.Console.WriteLine("UserEditorSelector " + e);

			//        this.Content.LocalIdentity.HandleFrame(
			//            e.frame,
			//            delegate
			//            {
			//                var Selector = this.Content.Selectors.Types[e.type].Sizes[e.size];
			//                var Position = new View.SelectorPosition { ContentX = e.x, ContentY = e.y };

			//                Selector.CreateTo(this.Content.View.Level, Position);
			//            },
			//            delegate
			//            {
			//                this.Content.Console.WriteLine("error: desync!");
			//            }
			//        );
			//    };
			//#endregion




			//#region pause
			//var SetPause = this.Content.Sync_SetPause;
			//this.Content.Sync_SetPause =
			//    (IsPaused, ByWhom) =>
			//    {
			//        if (IsPaused)
			//        {
			//            var FutureFrame = this.Content.LocalIdentity.HandleFutureFrame(
			//                delegate
			//                {
			//                    SetPause(true, ByWhom);
			//                }
			//            );

			//            this.Messages.SetPaused(FutureFrame);
			//        }
			//        else
			//        {
			//            SetPause(false, ByWhom);
			//            this.Messages.ClearPaused();
			//        }
			//    };

			//this.Events.UserSetPaused +=
			//    e =>
			//    {
			//        var c = this[e];

			//        this.Content.LocalIdentity.HandleFrame(e.frame,
			//            delegate
			//            {
			//                SetPause(true, c.Name);
			//            }
			//        );
			//    };

			//this.Events.UserClearPaused +=
			//    e =>
			//    {
			//        var c = this[e];

			//        SetPause(false, c.Name);
			//    };
			//#endregion



		}

		public PlayerIdentity this[int user]
		{
			get
			{
				return this.CoPlayers.FirstOrDefault(k => user == k.Number);
			}
		}

		public PlayerIdentity this[Communication.RemoteEvents.WithUserArguments u]
		{
			get
			{
				return this[u.user];
			}
		}


	}
}
