using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class CalibrationBaseInfoParser
    {
        public static CalibrationBaseInfoDataModel parse(Response_4BAllFrame frame)
        {
            CalibrationBaseInfoDataModel model = null;
            if (frame.Type == Response_4BAllFrame.TYPE.NORMAL)
            {
                model = new CalibrationBaseInfoDataModel();
                model.para1 = frame.paraCode1.Data[0];
                model.para2 = frame.paraCode2.Data[0];
            }
            return model;
        }
    }
}
