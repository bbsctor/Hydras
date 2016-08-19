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
    public class OnlineParameterValueTaskGroup:BasicTaskGroup
    {
        public enum TYPE { PARAMETER_VALUE};
        public SerialPortModel port;
        public OnlineParameterValueTaskGroup(Control control, BasicTask.UpdateView updateView)
            :base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.PARAMETER_VALUE, control, updateView,
                new BasicTask.Process(onlineParameterValueProcess)));

            base.Interval = 3000;
        }

        private void onlineParameterValueProcess()
        {
            new UIOnlineParameterValueService(port).getOnlineParameterValue();
        }

        public void getOnlineParameterValue()
        {
            base.execute(TYPE.PARAMETER_VALUE, -1);
        }
    }
}

