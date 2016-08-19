using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationService.SerialPortSupport;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace HydrasUI_WPF.Util
{
    public class HydrasExecuteInfoSubscriber:ExecuteInfoSubscriber
    {
        public delegate void update(StatusBarItem item, string content);
        public override void HandleExecuteInfoEvent(object sender, ExecuteInfoEventArgs e)
        {
            SerialPortModel port = (SerialPortModel)((BasicStaticController)sender).CommService.ConnectParameterModel;
            SondeWindow window = SondeWindowContext.getSondeWindow(port);
            if (window != null)
            {
                window.processStatusBarItem.Dispatcher.Invoke(new update(updateStatus), 
                    new Object[] { window.processStatusBarItem, e.Message });
            }
            else
            {
                MainWindowContext.mainWindow.statusTextBarItem.Dispatcher.Invoke(new update(updateStatus),
                    new Object[] { MainWindowContext.mainWindow.statusTextBarItem, e.Message });
                //Console.WriteLine(e.Message);
            }
        }

        public void updateStatus(StatusBarItem item, string content)
        {
            item.Content = content;
        }
    }
}
