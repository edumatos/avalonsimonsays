using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace AvalonSimonSays.iPad
{
    public class ApplicationCanvas : AvalonSimonSays.Code.SimonCanvas
    {
        public const int DefaultWidth = AvalonSimonSays.Code.SimonCanvas.DefaultWidth;
        public const int DefaultHeight = AvalonSimonSays.Code.SimonCanvas.DefaultHeight;



        public ApplicationCanvas()
        {
            this.ActiveIdentity = this.LocalIdentity;
            this.StartGame();
        }

    }
}
