using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace AvalonSimonSays.Code.Network.Shared
{
	[Script]
	public class VirtualClient
	{
		// this code is shared
		// between client and server

		public Communication.IEvents Events { get; set; }
		public Communication.IMessages Messages { get; set; }

	
	}
}
