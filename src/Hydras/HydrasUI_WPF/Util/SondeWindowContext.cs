using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasUI_WPF.Util
{
    public class SondeWindowContext
    {
        public static Dictionary<SerialPortModel, SondeWindow> sondeDict;

        static SondeWindowContext()
        {
            sondeDict = new Dictionary<SerialPortModel, SondeWindow>();
        }

        public static void addSondeWindow(SerialPortModel port, SondeWindow window)
        {
            sondeDict.Add(port, window);
        }

        public static void removeSondeWindow(SerialPortModel port)
        {
            sondeDict.Remove(port);
        }

        public static SondeWindow getSondeWindow(SerialPortModel port)
        {
            SondeWindow window;
            sondeDict.TryGetValue(port, out window);
            return window;
        }
    }
}

