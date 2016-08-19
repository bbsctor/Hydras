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
    public class CalibrationBaseInfoTaskGroup:BasicTaskGroup
    {
        public enum TYPE { CALIBRATION_BASEINFO};
        public SerialPortModel port;
        
        public CalibrationBaseInfoTaskGroup(Control control, BasicTask.UpdateView updateView)
            :base(control, updateView)
        {
            base.Add(new BasicTask(TYPE.CALIBRATION_BASEINFO, control, updateView, new BasicTask.Process(calibrationBaseInfoProcess)));

            base.Interval = 1000;
        }

        private void calibrationBaseInfoProcess()
        {
            new UICalibrationInfoService(port).getBaseInfo();
        }

        public void getBaseInfo()
        {
            base.execute(TYPE.CALIBRATION_BASEINFO, 1);
        }
    }
}
