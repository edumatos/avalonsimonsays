﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using FlashTreasureHunt.ConsoleTest;
using System.Windows.Forms;
using System.Diagnostics;
using AvalonUgh.Labs.MultiplayerTest.Interop;

namespace AvalonSimonSays.Labs.Multiplayer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("running Nonoba DevelopmentServer...");

			// http://board.flashkit.com/board/showthread.php?t=305322

			#region create projector
			const string player = @"C:\util\flex\runtimes\player\win\FlashPlayer.exe";
			const string swf = @"..\..\..\Public\NonobaClientFlash.swf";
			const string exe = swf + ".exe";

			var Files = new
			{
				swf = new FileInfo(swf)
			};

			#region build projector

			if (!File.Exists(player))
				throw new FileNotFoundException("player", player);

			if (!Files.swf.Exists)
				throw new FileNotFoundException("swf", swf);


			if (File.Exists(exe))
				File.Delete(exe);

			using (var r_player = new BinaryReader(File.OpenRead(player)))
			using (var r_swf = new BinaryReader(File.OpenRead(swf)))
			using (var w = new BinaryWriter(File.OpenWrite(exe)))
			{
				w.Write(r_player.ReadBytes((int)r_player.BaseStream.Length));
				w.Write(r_swf.ReadBytes((int)r_swf.BaseStream.Length));

				w.Write((uint)0xFA123456);
				w.Write((uint)r_swf.BaseStream.Length);

			}
			#endregion

			#endregion


			#region launcher
			ThreadPool.QueueUserWorkItem(
				delegate
				{
					Thread.Sleep(1000);

					var x = new Launcher();

					var app = new ApplicationContext(x);

					Action<Action> Invoke =
						h =>
						{
							if (x.IsDisposed)
								return;
							x.Invoke(h);
						};


					x.button1.Enabled = File.Exists(exe);

					x.button1.Click +=
						delegate
						{

							var p = Process.Start(exe);

							x.checkedListBox1.Items.Add(p.Id);

							p.EnableRaisingEvents = true;


							p.Exited +=
								delegate
								{
									Invoke(
										delegate
										{
											x.checkedListBox1.Items.Remove(p.Id);
										}
									);
								};

							x.FormClosing +=
								delegate
								{
									if (p.HasExited)
										return;

									p.Kill();

									p.WaitForExit();

									//p.CloseMainWindow();
								};

						};

					x.FormClosed +=
						delegate
						{
							if (File.Exists(exe))
								File.Delete(exe);

							foreach (Form f in Application.OpenForms)
							{
								if (f == x)
									continue;

								var k = f;

								f.Invoke(
									new Action(
											k.Close
									)
								);
							}
						};

					x.ShowServerWindow.CheckedChanged +=
						delegate
						{
							foreach (Form f in Application.OpenForms)
							{
								if (f == x)
									continue;

								f.Invoke(
									new Action(
										() => f.Visible = x.ShowServerWindow.Checked
									)
								);
							}

						};

					x.OfflineMode.CheckedChanged +=
						delegate
						{
							wininet.SetIEConnectionMode(x.OfflineMode.Checked);
						};

					x.Text = Files.swf.Name + " " + Files.swf.Length;

					//x.ShowServerWindow.Checked = true;
					//x.ShowServerWindow.Checked = false;

					Application.Run(app);


				}
			);
			#endregion

			//Nonoba.DevelopmentServer.Server.StartWithDebugging(100);
			global::Nonoba.DevelopmentServer.Server.StartWithDebugging(100);
		}
	}
}
