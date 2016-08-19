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
    public class ParameterInfoTaskGroup:BasicTaskGroup
    {
        public enum TYPE { PARAMETER_INFO};
        public SerialPortModel port;

        public ParameterInfoTaskGroup(Control control, BasicTask.UpdateView updateView)
            :base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.PARAMETER_INFO, control, updateView, new BasicTask.Process(parameterInfoProcess)));

            base.Interval = 1000;
        }

        private void parameterInfoProcess()
        {
            new UIParameterInfoService(port).getParameterInfo();
        }

        public void getParameterInfo()
        {
            base.execute(TYPE.PARAMETER_INFO, 1);
        }
    }
}
