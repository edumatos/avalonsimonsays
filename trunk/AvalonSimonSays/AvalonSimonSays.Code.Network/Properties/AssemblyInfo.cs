using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AvalonSimonSays.Code.Network")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("AvalonSimonSays.Code.Network")]
[assembly: AssemblyCopyright("Copyright ©  2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("91e381a8-e17a-4482-9f4e-ad74a8de2280")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]


[assembly:
	Script,
	ScriptTypeFilter(ScriptType.ActionScript, typeof(global::AvalonSimonSays.Code.Network.Shared.Communication)),
	ScriptTypeFilter(ScriptType.JavaScript, typeof(global::AvalonSimonSays.Code.Network.Shared.Communication)),
	ScriptTypeFilter(ScriptType.CSharp2, typeof(global::AvalonSimonSays.Code.Network.Shared.Communication)),

	ScriptTypeFilter(ScriptType.CSharp2, typeof(global::AvalonSimonSays.Code.Network.Server.NonobaGame)),

	ScriptTypeFilter(ScriptType.ActionScript, typeof(global::AvalonSimonSays.Code.Network.Client.ActionScript.NonobaClient)),
	ScriptTypeFilter(ScriptType.ActionScript, typeof(global::AvalonSimonSays.Code.Network.Client.Shared.NetworkClient)),
	ScriptTypeFilter(ScriptType.JavaScript, typeof(global::AvalonSimonSays.Code.Network.Client.Shared.NetworkClient)),

]

