using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class CalibrationDetailInfoParser
    {
        public static CalibrationDetailInfoDataModel parse(Response_4BDetailFrame frame)
        {
            CalibrationDetailInfoDataModel model = new CalibrationDetailInfoDataModel();
            model.paraCode1 = frame.paraCode1.Data[0];
            model.paraCode2 = frame.paraCode2.Data[0];
            model.fixed1 = frame.fixed1.Data[0];
            model.prompt = frame.prompt.Data;
            model.countFormat1 = frame.countFormat1.Data;
            model.countFormat2 = frame.countFormat2.Data;
            model.paraName = frame.paraName.Data;
            model.paraNameForShort = frame.paraNameForShort.Data;
            model.paraUnit = frame.paraUnit.Data;
            model.paraValue = frame.paraValue.Data;
            model.prompt = frame.prompt.Data;
            model.scope = frame.scope.Data;

            return model;
        }
    }
}
