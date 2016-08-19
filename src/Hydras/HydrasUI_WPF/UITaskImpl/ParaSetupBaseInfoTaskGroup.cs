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
    public class ParaSetupBaseInfoTaskGroup:BasicTaskGroup
    {
        public enum TYPE { PARASETUP_BASEINFO};
        public SerialPortModel port;

        public ParaSetupBaseInfoTaskGroup(Control control, BasicTask.UpdateView updateView)
            :base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.PARASETUP_BASEINFO, control, updateView, new BasicTask.Process(paraSetupBaseInfoProcess)));
        }

        private void paraSetupBaseInfoProcess()
        {
            new UIParaSetupInfoService(port).getBaseInfo();
        }

        public void getBaseInfo()
        {
            base.execute(TYPE.PARASETUP_BASEINFO, 1);
        }
    }
}
