using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ConfigFrame.UITask;
using HydrasUI_WPF.UIServiceImpl;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasUI_WPF.UITaskImpl
{
    public class ScanSondeTaskGroup:BasicTaskGroup
    {
        public enum TYPE { SCAN, RESCAN};
        public List<SerialPortModel> portList;
        
        public ScanSondeTaskGroup(Control control, BasicTask.UpdateView updateView)
            :base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.SCAN, control, updateView, new BasicTask.Process(scanProcess)));

            base.Interval = 1000;
        }

        private void scanProcess()
        {
            UIScanService.scan(portList);
        }

        public void scan()
        {
            base.execute(TYPE.SCAN,1);
        }
    }
}
