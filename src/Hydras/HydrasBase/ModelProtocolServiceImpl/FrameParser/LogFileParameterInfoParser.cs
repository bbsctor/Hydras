using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class LogFileParameterInfoParser
    {
        public static LogFileParameterInfoDataModel parse(Response_4FSelectedParaFrame frame)
        {
            LogFileParameterInfoDataModel model = new LogFileParameterInfoDataModel();
            model.sn = frame.sn.Data[0];
            model.paraCode = frame.paraCode.Data[0];
            model.calFormat1 = frame.calFormat1.Data;
            model.calFormat2 = frame.calFormat2.Data;
            model.calUnit = frame.calUnit.Data;

            model.formatAndScope = frame.formatAndScope.Data;

            model.paraName = frame.paraName.Data;
            model.paraNameForShort = frame.paraNameForShort.Data;

            return model;
        }
    }
}
