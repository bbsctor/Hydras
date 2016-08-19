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
    public class OperateSondeTaskGroup
        : BasicTaskGroup
    {
        public enum TYPE { OPERATE };
        public SerialPortModel port;

        public OperateSondeTaskGroup(Control control, BasicTask.UpdateView updateView)
            : base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.OPERATE, control, updateView, new BasicTask.Process(operateProcess)));

            base.Interval = 1000;
        }

        private void operateProcess()
        {
            UIOperateSondeService.operateSonde(port);
        }

        public void operate()
        {
            base.execute(TYPE.OPERATE, 1);
        }
    }
}













