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
    public class LogFileStatusTaskGroup:BasicTaskGroup
    {
        public enum TYPE { STATUS};
        public byte logNum;
        public SerialPortModel port;

        public LogFileStatusTaskGroup(Control control, BasicTask.UpdateView updateView)
            :base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.STATUS, control, updateView, new BasicTask.Process(statusProcess)));

            base.Interval = 8000;
        }

        private void statusProcess()
        {
            new UILogFileBaseInfoService(port).getLogFileBaseInfo(logNum);
            new UIDeviceStorageService(port).getInfo();
            
        }

        public void onlineStatus(byte logNum)
        {
            this.logNum = logNum;
            base.execute(TYPE.STATUS, -1);
        }
    }
}

