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
    public class CalibrationDetailInfoTaskGroup:BasicTaskGroup
    {
        public enum TYPE {CALIBRATION_DETAILINFO};
        public SerialPortModel port;
        public CalibrationDetailInfoTaskGroup(Control control, BasicTask.UpdateView updateView)
            :base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.CALIBRATION_DETAILINFO, control, updateView, new BasicTask.Process(calibrationDetailInfoProcess)));
        }

        private void calibrationDetailInfoProcess()
        {
            new UICalibrationInfoService(port).getDetailInfo();
        }

        public void getDetailInfo()
        {
            base.execute(TYPE.CALIBRATION_DETAILINFO, 1);
        }
    }
}

