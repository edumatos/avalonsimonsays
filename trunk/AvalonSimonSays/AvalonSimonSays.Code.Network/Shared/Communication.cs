using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Nonoba;

namespace AvalonSimonSays.Code.Network.Shared
{
	public static partial class Communication
	{
		public partial interface IMessages
		{
			void Server_Hello(int user, string name, int others);

			void Server_UserJoined(int user, string name);
			void Server_UserLeft(int user, string name);


			void UserHello(int user, string name, int frame);
			void UserSynced(int user, int frame);


			void SyncFrame(int frame, int framerate);
			void UserSyncFrame(int user, int frame, int framerate);

			void SyncFrameEcho(int frame, int framerate);
			void UserSyncFrameEcho(int user, int frame, int framerate);



			void SetPaused(int frame);
			void UserSetPaused(int user, int frame);

			void ClearPaused();
			void UserClearPaused(int user);



			void MouseMove(double x, double y);
			void UserMouseMove(int user, double x, double y);

			void UserEnqueueSimon(int user, int option);
			void UserEnqueueUser(int user, int option);

			void ClickOption(int frame, int option);
			void UserClickOption(int user, int frame, int option);

			void SimonOption(int frame, int option);
			void UserSimonOption(int user, int frame, int option);
		}


		partial class RemoteEvents : IEventsDispatch
		{
			public void EmptyHandler<T>(T Arguments)
			{
			}

			bool IEventsDispatch.DispatchInt32(int e, IDispatchHelper h)
			{
				return Dispatch((Messages)e, h);
			}

			partial class DispatchHelper : IDispatchHelper
			{
				public Converter<object, int> GetLength { get; set; }

				public DispatchHelper()
				{
					new DefaultImplementationForIDispatchHelper(this);
				}
			}
		}

	}
}
