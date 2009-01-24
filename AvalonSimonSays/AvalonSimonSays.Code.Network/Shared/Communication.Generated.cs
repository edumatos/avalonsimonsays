using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ScriptCoreLib.Shared.Nonoba;
using ScriptCoreLib;

namespace AvalonSimonSays.Code.Network.Shared
{
    #region Communication
    [Script]
    [CompilerGenerated]
    public partial class Communication
    {
        #region Messages
        [Script]
        [CompilerGenerated]
        public enum Messages
        {
            None = 100,
            Server_Hello,
            Server_UserJoined,
            Server_UserLeft,
            UserHello,
            UserSynced,
            SyncFrame,
            UserSyncFrame,
            SyncFrameEcho,
            UserSyncFrameEcho,
            SetPaused,
            UserSetPaused,
            ClearPaused,
            UserClearPaused,
            MouseMove,
            UserMouseMove,
            UserEnqueueSimon,
            UserEnqueueUser,
            ClickOption,
            UserClickOption,
            SimonOption,
            UserSimonOption,
            SetActive,
            UserSetActive,
            Server_SetScore,
            Server_AddFail,
        }
        #endregion

        #region IMessages
        [Script]
        [CompilerGenerated]
        public partial interface IMessages
        {
        }
        #endregion
        #region IEvents
        [Script]
        [CompilerGenerated]
        public partial interface IEvents
        {
            event Action<RemoteEvents.Server_HelloArguments> Server_Hello;
            event Action<RemoteEvents.Server_UserJoinedArguments> Server_UserJoined;
            event Action<RemoteEvents.Server_UserLeftArguments> Server_UserLeft;
            event Action<RemoteEvents.UserHelloArguments> UserHello;
            event Action<RemoteEvents.UserSyncedArguments> UserSynced;
            event Action<RemoteEvents.SyncFrameArguments> SyncFrame;
            event Action<RemoteEvents.UserSyncFrameArguments> UserSyncFrame;
            event Action<RemoteEvents.SyncFrameEchoArguments> SyncFrameEcho;
            event Action<RemoteEvents.UserSyncFrameEchoArguments> UserSyncFrameEcho;
            event Action<RemoteEvents.SetPausedArguments> SetPaused;
            event Action<RemoteEvents.UserSetPausedArguments> UserSetPaused;
            event Action<RemoteEvents.ClearPausedArguments> ClearPaused;
            event Action<RemoteEvents.UserClearPausedArguments> UserClearPaused;
            event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            event Action<RemoteEvents.UserEnqueueSimonArguments> UserEnqueueSimon;
            event Action<RemoteEvents.UserEnqueueUserArguments> UserEnqueueUser;
            event Action<RemoteEvents.ClickOptionArguments> ClickOption;
            event Action<RemoteEvents.UserClickOptionArguments> UserClickOption;
            event Action<RemoteEvents.SimonOptionArguments> SimonOption;
            event Action<RemoteEvents.UserSimonOptionArguments> UserSimonOption;
            event Action<RemoteEvents.SetActiveArguments> SetActive;
            event Action<RemoteEvents.UserSetActiveArguments> UserSetActive;
            event Action<RemoteEvents.Server_SetScoreArguments> Server_SetScore;
            event Action<RemoteEvents.Server_AddFailArguments> Server_AddFail;
        }
        #endregion

        #region RemoteMessages
        [Script]
        [CompilerGenerated]
        public sealed partial class RemoteMessages : IMessages
        {
            public Action<SendArguments> Send;
            public Func<IEnumerable<IMessages>> VirtualTargets;
            #region SendArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SendArguments
            {
                public Messages i;
                public object[] args;
            }
            #endregion
            public void Server_Hello(int user, string name, int others, int turn)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.Server_Hello, args = new object[] { user, name, others, turn } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.Server_Hello(user, name, others, turn);
                    }
                }
            }
            public void Server_UserJoined(int user, string name)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.Server_UserJoined, args = new object[] { user, name } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.Server_UserJoined(user, name);
                    }
                }
            }
            public void Server_UserLeft(int user, string name)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.Server_UserLeft, args = new object[] { user, name } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.Server_UserLeft(user, name);
                    }
                }
            }
            public void UserHello(int user, string name, int frame)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserHello, args = new object[] { user, name, frame } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserHello(user, name, frame);
                    }
                }
            }
            public void UserSynced(int user, int frame)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserSynced, args = new object[] { user, frame } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSynced(user, frame);
                    }
                }
            }
            public void SyncFrame(int frame, int framerate)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.SyncFrame, args = new object[] { frame, framerate } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SyncFrame(frame, framerate);
                    }
                }
            }
            public void UserSyncFrame(int user, int frame, int framerate)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserSyncFrame, args = new object[] { user, frame, framerate } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSyncFrame(user, frame, framerate);
                    }
                }
            }
            public void SyncFrameEcho(int frame, int framerate)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.SyncFrameEcho, args = new object[] { frame, framerate } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SyncFrameEcho(frame, framerate);
                    }
                }
            }
            public void UserSyncFrameEcho(int user, int frame, int framerate)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserSyncFrameEcho, args = new object[] { user, frame, framerate } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSyncFrameEcho(user, frame, framerate);
                    }
                }
            }
            public void SetPaused(int frame)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.SetPaused, args = new object[] { frame } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SetPaused(frame);
                    }
                }
            }
            public void UserSetPaused(int user, int frame)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserSetPaused, args = new object[] { user, frame } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSetPaused(user, frame);
                    }
                }
            }
            public void ClearPaused()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ClearPaused, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ClearPaused();
                    }
                }
            }
            public void UserClearPaused(int user)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserClearPaused, args = new object[] { user } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserClearPaused(user);
                    }
                }
            }
            public void MouseMove(double x, double y)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.MouseMove, args = new object[] { x, y } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.MouseMove(x, y);
                    }
                }
            }
            public void UserMouseMove(int user, double x, double y)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserMouseMove, args = new object[] { user, x, y } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserMouseMove(user, x, y);
                    }
                }
            }
            public void UserEnqueueSimon(int user, int option)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserEnqueueSimon, args = new object[] { user, option } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserEnqueueSimon(user, option);
                    }
                }
            }
            public void UserEnqueueUser(int user, int option)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserEnqueueUser, args = new object[] { user, option } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserEnqueueUser(user, option);
                    }
                }
            }
            public void ClickOption(int frame, int option)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ClickOption, args = new object[] { frame, option } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ClickOption(frame, option);
                    }
                }
            }
            public void UserClickOption(int user, int frame, int option)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserClickOption, args = new object[] { user, frame, option } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserClickOption(user, frame, option);
                    }
                }
            }
            public void SimonOption(int frame, int option)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.SimonOption, args = new object[] { frame, option } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SimonOption(frame, option);
                    }
                }
            }
            public void UserSimonOption(int user, int frame, int option)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserSimonOption, args = new object[] { user, frame, option } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSimonOption(user, frame, option);
                    }
                }
            }
            public void SetActive(int frame, int active)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.SetActive, args = new object[] { frame, active } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SetActive(frame, active);
                    }
                }
            }
            public void UserSetActive(int user, int frame, int active)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserSetActive, args = new object[] { user, frame, active } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSetActive(user, frame, active);
                    }
                }
            }
            public void Server_SetScore(int score)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.Server_SetScore, args = new object[] { score } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.Server_SetScore(score);
                    }
                }
            }
            public void Server_AddFail()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.Server_AddFail, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.Server_AddFail();
                    }
                }
            }
        }
        #endregion

        #region RemoteEvents
        [Script]
        [CompilerGenerated]
        public sealed partial class RemoteEvents : IEvents
        {
            private readonly Dictionary<Messages, Action<IDispatchHelper>> DispatchTable;
            private readonly Dictionary<Messages, Converter<object, Delegate>> DispatchTableDelegates;
            [AccessedThroughProperty("BroadcastRouter")]
            private WithUserArgumentsRouter_Broadcast _BroadcastRouter;
            [AccessedThroughProperty("SinglecastRouter")]
            private WithUserArgumentsRouter_Singlecast _SinglecastRouter;
            #region DispatchHelper
            [Script]
            [CompilerGenerated]
            public partial class DispatchHelper
            {
                public Converter<uint, int> GetInt32 { get; set; }
                public Converter<uint, double> GetDouble { get; set; }
                public Converter<uint, string> GetString { get; set; }
                public Converter<uint, int[]> GetInt32Array { get; set; }
                public Converter<uint, double[]> GetDoubleArray { get; set; }
                public Converter<uint, string[]> GetStringArray { get; set; }
                public Converter<uint, byte[]> GetMemoryStream { get; set; }
            }
            #endregion
            public bool Dispatch(Messages e, IDispatchHelper h)
            {
                if (!DispatchTableDelegates.ContainsKey(e)) return false;
                if (DispatchTableDelegates[e](null) == null) return false;
                if (!DispatchTable.ContainsKey(e)) return false;
                DispatchTable[e](h);
                return true;
            }
            #region WithUserArguments
            [Script]
            [CompilerGenerated]
            public abstract partial class WithUserArguments
            {
                public int user;
            }
            #endregion
            #region WithUserArgumentsRouter_Broadcast
            [Script]
            [CompilerGenerated]
            public sealed partial class WithUserArgumentsRouter_Broadcast : WithUserArguments
            {
                public IMessages Target;

                #region Automatic Event Routing
                public void CombineDelegates(IEvents value)
                {
                    value.SyncFrame += this.UserSyncFrame;
                    value.SyncFrameEcho += this.UserSyncFrameEcho;
                    value.SetPaused += this.UserSetPaused;
                    value.ClearPaused += this.UserClearPaused;
                    value.MouseMove += this.UserMouseMove;
                    value.ClickOption += this.UserClickOption;
                    value.SimonOption += this.UserSimonOption;
                    value.SetActive += this.UserSetActive;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.SyncFrame -= this.UserSyncFrame;
                    value.SyncFrameEcho -= this.UserSyncFrameEcho;
                    value.SetPaused -= this.UserSetPaused;
                    value.ClearPaused -= this.UserClearPaused;
                    value.MouseMove -= this.UserMouseMove;
                    value.ClickOption -= this.UserClickOption;
                    value.SimonOption -= this.UserSimonOption;
                    value.SetActive -= this.UserSetActive;
                }
                #endregion

                #region Routing
                public void UserSyncFrame(SyncFrameArguments e)
                {
                    Target.UserSyncFrame(this.user, e.frame, e.framerate);
                }
                public void UserSyncFrameEcho(SyncFrameEchoArguments e)
                {
                    Target.UserSyncFrameEcho(this.user, e.frame, e.framerate);
                }
                public void UserSetPaused(SetPausedArguments e)
                {
                    Target.UserSetPaused(this.user, e.frame);
                }
                public void UserClearPaused(ClearPausedArguments e)
                {
                    Target.UserClearPaused(this.user);
                }
                public void UserMouseMove(MouseMoveArguments e)
                {
                    Target.UserMouseMove(this.user, e.x, e.y);
                }
                public void UserClickOption(ClickOptionArguments e)
                {
                    Target.UserClickOption(this.user, e.frame, e.option);
                }
                public void UserSimonOption(SimonOptionArguments e)
                {
                    Target.UserSimonOption(this.user, e.frame, e.option);
                }
                public void UserSetActive(SetActiveArguments e)
                {
                    Target.UserSetActive(this.user, e.frame, e.active);
                }
                #endregion
            }
            #endregion
            #region WithUserArgumentsRouter_SinglecastView
            [Script]
            [CompilerGenerated]
            public sealed partial class WithUserArgumentsRouter_SinglecastView : WithUserArguments
            {
                public IMessages Target;
                #region Routing
                public void UserHello(string name, int frame)
                {
                    this.Target.UserHello(this.user, name, frame);
                }
                public void UserHello(UserHelloArguments e)
                {
                    this.Target.UserHello(this.user, e.name, e.frame);
                }
                public void UserSynced(int frame)
                {
                    this.Target.UserSynced(this.user, frame);
                }
                public void UserSynced(UserSyncedArguments e)
                {
                    this.Target.UserSynced(this.user, e.frame);
                }
                public void UserSyncFrame(int frame, int framerate)
                {
                    this.Target.UserSyncFrame(this.user, frame, framerate);
                }
                public void UserSyncFrame(UserSyncFrameArguments e)
                {
                    this.Target.UserSyncFrame(this.user, e.frame, e.framerate);
                }
                public void UserSyncFrameEcho(int frame, int framerate)
                {
                    this.Target.UserSyncFrameEcho(this.user, frame, framerate);
                }
                public void UserSyncFrameEcho(UserSyncFrameEchoArguments e)
                {
                    this.Target.UserSyncFrameEcho(this.user, e.frame, e.framerate);
                }
                public void UserSetPaused(int frame)
                {
                    this.Target.UserSetPaused(this.user, frame);
                }
                public void UserSetPaused(UserSetPausedArguments e)
                {
                    this.Target.UserSetPaused(this.user, e.frame);
                }
                public void UserClearPaused()
                {
                    this.Target.UserClearPaused(this.user);
                }
                public void UserClearPaused(UserClearPausedArguments e)
                {
                    this.Target.UserClearPaused(this.user);
                }
                public void UserMouseMove(double x, double y)
                {
                    this.Target.UserMouseMove(this.user, x, y);
                }
                public void UserMouseMove(UserMouseMoveArguments e)
                {
                    this.Target.UserMouseMove(this.user, e.x, e.y);
                }
                public void UserEnqueueSimon(int option)
                {
                    this.Target.UserEnqueueSimon(this.user, option);
                }
                public void UserEnqueueSimon(UserEnqueueSimonArguments e)
                {
                    this.Target.UserEnqueueSimon(this.user, e.option);
                }
                public void UserEnqueueUser(int option)
                {
                    this.Target.UserEnqueueUser(this.user, option);
                }
                public void UserEnqueueUser(UserEnqueueUserArguments e)
                {
                    this.Target.UserEnqueueUser(this.user, e.option);
                }
                public void UserClickOption(int frame, int option)
                {
                    this.Target.UserClickOption(this.user, frame, option);
                }
                public void UserClickOption(UserClickOptionArguments e)
                {
                    this.Target.UserClickOption(this.user, e.frame, e.option);
                }
                public void UserSimonOption(int frame, int option)
                {
                    this.Target.UserSimonOption(this.user, frame, option);
                }
                public void UserSimonOption(UserSimonOptionArguments e)
                {
                    this.Target.UserSimonOption(this.user, e.frame, e.option);
                }
                public void UserSetActive(int frame, int active)
                {
                    this.Target.UserSetActive(this.user, frame, active);
                }
                public void UserSetActive(UserSetActiveArguments e)
                {
                    this.Target.UserSetActive(this.user, e.frame, e.active);
                }
                #endregion
            }
            #endregion
            #region WithUserArgumentsRouter_Singlecast
            [Script]
            [CompilerGenerated]
            public sealed partial class WithUserArgumentsRouter_Singlecast : WithUserArguments
            {
                public System.Converter<int, IMessages> Target;

                #region Automatic Event Routing
                public void CombineDelegates(IEvents value)
                {
                    value.UserHello += this.UserHello;
                    value.UserSynced += this.UserSynced;
                    value.UserSyncFrame += this.UserSyncFrame;
                    value.UserSyncFrameEcho += this.UserSyncFrameEcho;
                    value.UserSetPaused += this.UserSetPaused;
                    value.UserClearPaused += this.UserClearPaused;
                    value.UserMouseMove += this.UserMouseMove;
                    value.UserEnqueueSimon += this.UserEnqueueSimon;
                    value.UserEnqueueUser += this.UserEnqueueUser;
                    value.UserClickOption += this.UserClickOption;
                    value.UserSimonOption += this.UserSimonOption;
                    value.UserSetActive += this.UserSetActive;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.UserHello -= this.UserHello;
                    value.UserSynced -= this.UserSynced;
                    value.UserSyncFrame -= this.UserSyncFrame;
                    value.UserSyncFrameEcho -= this.UserSyncFrameEcho;
                    value.UserSetPaused -= this.UserSetPaused;
                    value.UserClearPaused -= this.UserClearPaused;
                    value.UserMouseMove -= this.UserMouseMove;
                    value.UserEnqueueSimon -= this.UserEnqueueSimon;
                    value.UserEnqueueUser -= this.UserEnqueueUser;
                    value.UserClickOption -= this.UserClickOption;
                    value.UserSimonOption -= this.UserSimonOption;
                    value.UserSetActive -= this.UserSetActive;
                }
                #endregion

                #region Routing
                public void UserHello(UserHelloArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserHello(this.user, e.name, e.frame);
                }
                public void UserSynced(UserSyncedArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSynced(this.user, e.frame);
                }
                public void UserSyncFrame(UserSyncFrameArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSyncFrame(this.user, e.frame, e.framerate);
                }
                public void UserSyncFrameEcho(UserSyncFrameEchoArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSyncFrameEcho(this.user, e.frame, e.framerate);
                }
                public void UserSetPaused(UserSetPausedArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSetPaused(this.user, e.frame);
                }
                public void UserClearPaused(UserClearPausedArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserClearPaused(this.user);
                }
                public void UserMouseMove(UserMouseMoveArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserMouseMove(this.user, e.x, e.y);
                }
                public void UserEnqueueSimon(UserEnqueueSimonArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserEnqueueSimon(this.user, e.option);
                }
                public void UserEnqueueUser(UserEnqueueUserArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserEnqueueUser(this.user, e.option);
                }
                public void UserClickOption(UserClickOptionArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserClickOption(this.user, e.frame, e.option);
                }
                public void UserSimonOption(UserSimonOptionArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSimonOption(this.user, e.frame, e.option);
                }
                public void UserSetActive(UserSetActiveArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSetActive(this.user, e.frame, e.active);
                }
                #endregion
            }
            #endregion
            #region Server_HelloArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class Server_HelloArguments
            {
                public int user;
                public string name;
                public int others;
                public int turn;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(", others = ").Append(this.others).Append(", turn = ").Append(this.turn).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<Server_HelloArguments> Server_Hello;
            #region Server_UserJoinedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class Server_UserJoinedArguments
            {
                public int user;
                public string name;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<Server_UserJoinedArguments> Server_UserJoined;
            #region Server_UserLeftArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class Server_UserLeftArguments
            {
                public int user;
                public string name;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<Server_UserLeftArguments> Server_UserLeft;
            #region UserHelloArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserHelloArguments : WithUserArguments
            {
                public string name;
                public int frame;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(", frame = ").Append(this.frame).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserHelloArguments> UserHello;
            #region UserSyncedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSyncedArguments : WithUserArguments
            {
                public int frame;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", frame = ").Append(this.frame).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSyncedArguments> UserSynced;
            #region SyncFrameArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SyncFrameArguments
            {
                public int frame;
                public int framerate;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ frame = ").Append(this.frame).Append(", framerate = ").Append(this.framerate).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SyncFrameArguments> SyncFrame;
            #region UserSyncFrameArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSyncFrameArguments : WithUserArguments
            {
                public int frame;
                public int framerate;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", frame = ").Append(this.frame).Append(", framerate = ").Append(this.framerate).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSyncFrameArguments> UserSyncFrame;
            #region SyncFrameEchoArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SyncFrameEchoArguments
            {
                public int frame;
                public int framerate;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ frame = ").Append(this.frame).Append(", framerate = ").Append(this.framerate).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SyncFrameEchoArguments> SyncFrameEcho;
            #region UserSyncFrameEchoArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSyncFrameEchoArguments : WithUserArguments
            {
                public int frame;
                public int framerate;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", frame = ").Append(this.frame).Append(", framerate = ").Append(this.framerate).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSyncFrameEchoArguments> UserSyncFrameEcho;
            #region SetPausedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SetPausedArguments
            {
                public int frame;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ frame = ").Append(this.frame).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SetPausedArguments> SetPaused;
            #region UserSetPausedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSetPausedArguments : WithUserArguments
            {
                public int frame;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", frame = ").Append(this.frame).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSetPausedArguments> UserSetPaused;
            #region ClearPausedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class ClearPausedArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<ClearPausedArguments> ClearPaused;
            #region UserClearPausedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserClearPausedArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserClearPausedArguments> UserClearPaused;
            #region MouseMoveArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class MouseMoveArguments
            {
                public double x;
                public double y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ x = ").Append(this.x).Append(", y = ").Append(this.y).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<MouseMoveArguments> MouseMove;
            #region UserMouseMoveArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserMouseMoveArguments : WithUserArguments
            {
                public double x;
                public double y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserMouseMoveArguments> UserMouseMove;
            #region UserEnqueueSimonArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserEnqueueSimonArguments : WithUserArguments
            {
                public int option;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", option = ").Append(this.option).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserEnqueueSimonArguments> UserEnqueueSimon;
            #region UserEnqueueUserArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserEnqueueUserArguments : WithUserArguments
            {
                public int option;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", option = ").Append(this.option).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserEnqueueUserArguments> UserEnqueueUser;
            #region ClickOptionArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class ClickOptionArguments
            {
                public int frame;
                public int option;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ frame = ").Append(this.frame).Append(", option = ").Append(this.option).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ClickOptionArguments> ClickOption;
            #region UserClickOptionArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserClickOptionArguments : WithUserArguments
            {
                public int frame;
                public int option;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", frame = ").Append(this.frame).Append(", option = ").Append(this.option).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserClickOptionArguments> UserClickOption;
            #region SimonOptionArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SimonOptionArguments
            {
                public int frame;
                public int option;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ frame = ").Append(this.frame).Append(", option = ").Append(this.option).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SimonOptionArguments> SimonOption;
            #region UserSimonOptionArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSimonOptionArguments : WithUserArguments
            {
                public int frame;
                public int option;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", frame = ").Append(this.frame).Append(", option = ").Append(this.option).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSimonOptionArguments> UserSimonOption;
            #region SetActiveArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SetActiveArguments
            {
                public int frame;
                public int active;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ frame = ").Append(this.frame).Append(", active = ").Append(this.active).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SetActiveArguments> SetActive;
            #region UserSetActiveArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSetActiveArguments : WithUserArguments
            {
                public int frame;
                public int active;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", frame = ").Append(this.frame).Append(", active = ").Append(this.active).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSetActiveArguments> UserSetActive;
            #region Server_SetScoreArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class Server_SetScoreArguments
            {
                public int score;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ score = ").Append(this.score).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<Server_SetScoreArguments> Server_SetScore;
            #region Server_AddFailArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class Server_AddFailArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<Server_AddFailArguments> Server_AddFail;
            public RemoteEvents()
            {
                DispatchTable = new Dictionary<Messages, Action<IDispatchHelper>>
                        {
                            { Messages.Server_Hello, e => { Server_Hello(new Server_HelloArguments { user = e.GetInt32(0), name = e.GetString(1), others = e.GetInt32(2), turn = e.GetInt32(3) }); } },
                            { Messages.Server_UserJoined, e => { Server_UserJoined(new Server_UserJoinedArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.Server_UserLeft, e => { Server_UserLeft(new Server_UserLeftArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.UserHello, e => { UserHello(new UserHelloArguments { user = e.GetInt32(0), name = e.GetString(1), frame = e.GetInt32(2) }); } },
                            { Messages.UserSynced, e => { UserSynced(new UserSyncedArguments { user = e.GetInt32(0), frame = e.GetInt32(1) }); } },
                            { Messages.SyncFrame, e => { SyncFrame(new SyncFrameArguments { frame = e.GetInt32(0), framerate = e.GetInt32(1) }); } },
                            { Messages.UserSyncFrame, e => { UserSyncFrame(new UserSyncFrameArguments { user = e.GetInt32(0), frame = e.GetInt32(1), framerate = e.GetInt32(2) }); } },
                            { Messages.SyncFrameEcho, e => { SyncFrameEcho(new SyncFrameEchoArguments { frame = e.GetInt32(0), framerate = e.GetInt32(1) }); } },
                            { Messages.UserSyncFrameEcho, e => { UserSyncFrameEcho(new UserSyncFrameEchoArguments { user = e.GetInt32(0), frame = e.GetInt32(1), framerate = e.GetInt32(2) }); } },
                            { Messages.SetPaused, e => { SetPaused(new SetPausedArguments { frame = e.GetInt32(0) }); } },
                            { Messages.UserSetPaused, e => { UserSetPaused(new UserSetPausedArguments { user = e.GetInt32(0), frame = e.GetInt32(1) }); } },
                            { Messages.ClearPaused, e => { ClearPaused(new ClearPausedArguments {  }); } },
                            { Messages.UserClearPaused, e => { UserClearPaused(new UserClearPausedArguments { user = e.GetInt32(0) }); } },
                            { Messages.MouseMove, e => { MouseMove(new MouseMoveArguments { x = e.GetDouble(0), y = e.GetDouble(1) }); } },
                            { Messages.UserMouseMove, e => { UserMouseMove(new UserMouseMoveArguments { user = e.GetInt32(0), x = e.GetDouble(1), y = e.GetDouble(2) }); } },
                            { Messages.UserEnqueueSimon, e => { UserEnqueueSimon(new UserEnqueueSimonArguments { user = e.GetInt32(0), option = e.GetInt32(1) }); } },
                            { Messages.UserEnqueueUser, e => { UserEnqueueUser(new UserEnqueueUserArguments { user = e.GetInt32(0), option = e.GetInt32(1) }); } },
                            { Messages.ClickOption, e => { ClickOption(new ClickOptionArguments { frame = e.GetInt32(0), option = e.GetInt32(1) }); } },
                            { Messages.UserClickOption, e => { UserClickOption(new UserClickOptionArguments { user = e.GetInt32(0), frame = e.GetInt32(1), option = e.GetInt32(2) }); } },
                            { Messages.SimonOption, e => { SimonOption(new SimonOptionArguments { frame = e.GetInt32(0), option = e.GetInt32(1) }); } },
                            { Messages.UserSimonOption, e => { UserSimonOption(new UserSimonOptionArguments { user = e.GetInt32(0), frame = e.GetInt32(1), option = e.GetInt32(2) }); } },
                            { Messages.SetActive, e => { SetActive(new SetActiveArguments { frame = e.GetInt32(0), active = e.GetInt32(1) }); } },
                            { Messages.UserSetActive, e => { UserSetActive(new UserSetActiveArguments { user = e.GetInt32(0), frame = e.GetInt32(1), active = e.GetInt32(2) }); } },
                            { Messages.Server_SetScore, e => { Server_SetScore(new Server_SetScoreArguments { score = e.GetInt32(0) }); } },
                            { Messages.Server_AddFail, e => { Server_AddFail(new Server_AddFailArguments {  }); } },
                        }
                ;
                DispatchTableDelegates = new Dictionary<Messages, Converter<object, Delegate>>
                        {
                            { Messages.Server_Hello, e => Server_Hello },
                            { Messages.Server_UserJoined, e => Server_UserJoined },
                            { Messages.Server_UserLeft, e => Server_UserLeft },
                            { Messages.UserHello, e => UserHello },
                            { Messages.UserSynced, e => UserSynced },
                            { Messages.SyncFrame, e => SyncFrame },
                            { Messages.UserSyncFrame, e => UserSyncFrame },
                            { Messages.SyncFrameEcho, e => SyncFrameEcho },
                            { Messages.UserSyncFrameEcho, e => UserSyncFrameEcho },
                            { Messages.SetPaused, e => SetPaused },
                            { Messages.UserSetPaused, e => UserSetPaused },
                            { Messages.ClearPaused, e => ClearPaused },
                            { Messages.UserClearPaused, e => UserClearPaused },
                            { Messages.MouseMove, e => MouseMove },
                            { Messages.UserMouseMove, e => UserMouseMove },
                            { Messages.UserEnqueueSimon, e => UserEnqueueSimon },
                            { Messages.UserEnqueueUser, e => UserEnqueueUser },
                            { Messages.ClickOption, e => ClickOption },
                            { Messages.UserClickOption, e => UserClickOption },
                            { Messages.SimonOption, e => SimonOption },
                            { Messages.UserSimonOption, e => UserSimonOption },
                            { Messages.SetActive, e => SetActive },
                            { Messages.UserSetActive, e => UserSetActive },
                            { Messages.Server_SetScore, e => Server_SetScore },
                            { Messages.Server_AddFail, e => Server_AddFail },
                        }
                ;
            }
            public WithUserArgumentsRouter_Broadcast BroadcastRouter
            {
                [DebuggerNonUserCode]
                get
                {
                    return this._BroadcastRouter;
                }
                [DebuggerNonUserCode]
                [MethodImpl(MethodImplOptions.Synchronized)]
                set
                {
                    if(_BroadcastRouter != null)
                    {
                        _BroadcastRouter.RemoveDelegates(this);
                    }
                    _BroadcastRouter = value;
                    if(_BroadcastRouter != null)
                    {
                        _BroadcastRouter.CombineDelegates(this);
                    }
                }
            }
            public WithUserArgumentsRouter_Singlecast SinglecastRouter
            {
                [DebuggerNonUserCode]
                get
                {
                    return this._SinglecastRouter;
                }
                [DebuggerNonUserCode]
                [MethodImpl(MethodImplOptions.Synchronized)]
                set
                {
                    if(_SinglecastRouter != null)
                    {
                        _SinglecastRouter.RemoveDelegates(this);
                    }
                    _SinglecastRouter = value;
                    if(_SinglecastRouter != null)
                    {
                        _SinglecastRouter.CombineDelegates(this);
                    }
                }
            }
        }
        #endregion
        #region Bridge
        [Script]
        [CompilerGenerated]
        public partial class Bridge : IEvents, IMessages
        {
            public Action<Action> VirtualLatency;
            public Bridge()
            {
                this.VirtualLatency = VirtualLatencyDefaultImplemenetation;
            }
            public void VirtualLatencyDefaultImplemenetation(Action e)
            {
                e();
            }
            public event Action<RemoteEvents.Server_HelloArguments> Server_Hello;
            void IMessages.Server_Hello(int user, string name, int others, int turn)
            {
                if(Server_Hello == null) return;
                var v = new RemoteEvents.Server_HelloArguments { user = user, name = name, others = others, turn = turn };
                this.VirtualLatency(() => this.Server_Hello(v));
            }

            public event Action<RemoteEvents.Server_UserJoinedArguments> Server_UserJoined;
            void IMessages.Server_UserJoined(int user, string name)
            {
                if(Server_UserJoined == null) return;
                var v = new RemoteEvents.Server_UserJoinedArguments { user = user, name = name };
                this.VirtualLatency(() => this.Server_UserJoined(v));
            }

            public event Action<RemoteEvents.Server_UserLeftArguments> Server_UserLeft;
            void IMessages.Server_UserLeft(int user, string name)
            {
                if(Server_UserLeft == null) return;
                var v = new RemoteEvents.Server_UserLeftArguments { user = user, name = name };
                this.VirtualLatency(() => this.Server_UserLeft(v));
            }

            public event Action<RemoteEvents.UserHelloArguments> UserHello;
            void IMessages.UserHello(int user, string name, int frame)
            {
                if(UserHello == null) return;
                var v = new RemoteEvents.UserHelloArguments { user = user, name = name, frame = frame };
                this.VirtualLatency(() => this.UserHello(v));
            }

            public event Action<RemoteEvents.UserSyncedArguments> UserSynced;
            void IMessages.UserSynced(int user, int frame)
            {
                if(UserSynced == null) return;
                var v = new RemoteEvents.UserSyncedArguments { user = user, frame = frame };
                this.VirtualLatency(() => this.UserSynced(v));
            }

            public event Action<RemoteEvents.SyncFrameArguments> SyncFrame;
            void IMessages.SyncFrame(int frame, int framerate)
            {
                if(SyncFrame == null) return;
                var v = new RemoteEvents.SyncFrameArguments { frame = frame, framerate = framerate };
                this.VirtualLatency(() => this.SyncFrame(v));
            }

            public event Action<RemoteEvents.UserSyncFrameArguments> UserSyncFrame;
            void IMessages.UserSyncFrame(int user, int frame, int framerate)
            {
                if(UserSyncFrame == null) return;
                var v = new RemoteEvents.UserSyncFrameArguments { user = user, frame = frame, framerate = framerate };
                this.VirtualLatency(() => this.UserSyncFrame(v));
            }

            public event Action<RemoteEvents.SyncFrameEchoArguments> SyncFrameEcho;
            void IMessages.SyncFrameEcho(int frame, int framerate)
            {
                if(SyncFrameEcho == null) return;
                var v = new RemoteEvents.SyncFrameEchoArguments { frame = frame, framerate = framerate };
                this.VirtualLatency(() => this.SyncFrameEcho(v));
            }

            public event Action<RemoteEvents.UserSyncFrameEchoArguments> UserSyncFrameEcho;
            void IMessages.UserSyncFrameEcho(int user, int frame, int framerate)
            {
                if(UserSyncFrameEcho == null) return;
                var v = new RemoteEvents.UserSyncFrameEchoArguments { user = user, frame = frame, framerate = framerate };
                this.VirtualLatency(() => this.UserSyncFrameEcho(v));
            }

            public event Action<RemoteEvents.SetPausedArguments> SetPaused;
            void IMessages.SetPaused(int frame)
            {
                if(SetPaused == null) return;
                var v = new RemoteEvents.SetPausedArguments { frame = frame };
                this.VirtualLatency(() => this.SetPaused(v));
            }

            public event Action<RemoteEvents.UserSetPausedArguments> UserSetPaused;
            void IMessages.UserSetPaused(int user, int frame)
            {
                if(UserSetPaused == null) return;
                var v = new RemoteEvents.UserSetPausedArguments { user = user, frame = frame };
                this.VirtualLatency(() => this.UserSetPaused(v));
            }

            public event Action<RemoteEvents.ClearPausedArguments> ClearPaused;
            void IMessages.ClearPaused()
            {
                if(ClearPaused == null) return;
                var v = new RemoteEvents.ClearPausedArguments {  };
                this.VirtualLatency(() => this.ClearPaused(v));
            }

            public event Action<RemoteEvents.UserClearPausedArguments> UserClearPaused;
            void IMessages.UserClearPaused(int user)
            {
                if(UserClearPaused == null) return;
                var v = new RemoteEvents.UserClearPausedArguments { user = user };
                this.VirtualLatency(() => this.UserClearPaused(v));
            }

            public event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            void IMessages.MouseMove(double x, double y)
            {
                if(MouseMove == null) return;
                var v = new RemoteEvents.MouseMoveArguments { x = x, y = y };
                this.VirtualLatency(() => this.MouseMove(v));
            }

            public event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            void IMessages.UserMouseMove(int user, double x, double y)
            {
                if(UserMouseMove == null) return;
                var v = new RemoteEvents.UserMouseMoveArguments { user = user, x = x, y = y };
                this.VirtualLatency(() => this.UserMouseMove(v));
            }

            public event Action<RemoteEvents.UserEnqueueSimonArguments> UserEnqueueSimon;
            void IMessages.UserEnqueueSimon(int user, int option)
            {
                if(UserEnqueueSimon == null) return;
                var v = new RemoteEvents.UserEnqueueSimonArguments { user = user, option = option };
                this.VirtualLatency(() => this.UserEnqueueSimon(v));
            }

            public event Action<RemoteEvents.UserEnqueueUserArguments> UserEnqueueUser;
            void IMessages.UserEnqueueUser(int user, int option)
            {
                if(UserEnqueueUser == null) return;
                var v = new RemoteEvents.UserEnqueueUserArguments { user = user, option = option };
                this.VirtualLatency(() => this.UserEnqueueUser(v));
            }

            public event Action<RemoteEvents.ClickOptionArguments> ClickOption;
            void IMessages.ClickOption(int frame, int option)
            {
                if(ClickOption == null) return;
                var v = new RemoteEvents.ClickOptionArguments { frame = frame, option = option };
                this.VirtualLatency(() => this.ClickOption(v));
            }

            public event Action<RemoteEvents.UserClickOptionArguments> UserClickOption;
            void IMessages.UserClickOption(int user, int frame, int option)
            {
                if(UserClickOption == null) return;
                var v = new RemoteEvents.UserClickOptionArguments { user = user, frame = frame, option = option };
                this.VirtualLatency(() => this.UserClickOption(v));
            }

            public event Action<RemoteEvents.SimonOptionArguments> SimonOption;
            void IMessages.SimonOption(int frame, int option)
            {
                if(SimonOption == null) return;
                var v = new RemoteEvents.SimonOptionArguments { frame = frame, option = option };
                this.VirtualLatency(() => this.SimonOption(v));
            }

            public event Action<RemoteEvents.UserSimonOptionArguments> UserSimonOption;
            void IMessages.UserSimonOption(int user, int frame, int option)
            {
                if(UserSimonOption == null) return;
                var v = new RemoteEvents.UserSimonOptionArguments { user = user, frame = frame, option = option };
                this.VirtualLatency(() => this.UserSimonOption(v));
            }

            public event Action<RemoteEvents.SetActiveArguments> SetActive;
            void IMessages.SetActive(int frame, int active)
            {
                if(SetActive == null) return;
                var v = new RemoteEvents.SetActiveArguments { frame = frame, active = active };
                this.VirtualLatency(() => this.SetActive(v));
            }

            public event Action<RemoteEvents.UserSetActiveArguments> UserSetActive;
            void IMessages.UserSetActive(int user, int frame, int active)
            {
                if(UserSetActive == null) return;
                var v = new RemoteEvents.UserSetActiveArguments { user = user, frame = frame, active = active };
                this.VirtualLatency(() => this.UserSetActive(v));
            }

            public event Action<RemoteEvents.Server_SetScoreArguments> Server_SetScore;
            void IMessages.Server_SetScore(int score)
            {
                if(Server_SetScore == null) return;
                var v = new RemoteEvents.Server_SetScoreArguments { score = score };
                this.VirtualLatency(() => this.Server_SetScore(v));
            }

            public event Action<RemoteEvents.Server_AddFailArguments> Server_AddFail;
            void IMessages.Server_AddFail()
            {
                if(Server_AddFail == null) return;
                var v = new RemoteEvents.Server_AddFailArguments {  };
                this.VirtualLatency(() => this.Server_AddFail(v));
            }

        }
        #endregion
    }
    #endregion
}
// 24.01.2009 19:25:57
