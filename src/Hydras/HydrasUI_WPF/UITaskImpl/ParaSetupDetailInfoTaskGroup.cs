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
    public class ParaSetupDetailInfoTaskGroup:BasicTaskGroup
    {
        public enum TYPE {PARASETUP_DETAILINFO};
        public SerialPortModel port;
        public ParaSetupDetailInfoTaskGroup(Control control, BasicTask.UpdateView updateView)
            :base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.PARASETUP_DETAILINFO, control, updateView, new BasicTask.Process(paraSetupDetailInfoProcess)));
        }

        private void paraSetupDetailInfoProcess()
        {
            new UIParaSetupInfoService(port).getDetailInfo();
        }

        public void getDetailInfo()
        {
            base.execute(TYPE.PARASETUP_DETAILINFO, 1);
        }
    }
}

