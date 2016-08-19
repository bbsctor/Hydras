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
    public class SynDateTimeTaskGroup:BasicTaskGroup
    {
        public enum TYPE { SYN_DATETIME};
        public SerialPortModel port;

        public SynDateTimeTaskGroup(Control control, BasicTask.UpdateView updateView)
            :base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.SYN_DATETIME, control, updateView,
                new BasicTask.Process(synDateTimeProcess)));

            base.Interval = 10000;
        }

        private void synDateTimeProcess()
        {
            new UISystemAndSettingService(port).getDateTime();
        }

        public void synchronizedDateTime()
        {
            base.execute(TYPE.SYN_DATETIME, -1);
        }
    }
}