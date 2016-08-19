using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class ParameterSetupDetailInfoParser
    {
        public static ParameterSetupDetailInfoDataModel parse(Response_49DetailFrame frame)
        {
            ParameterSetupDetailInfoDataModel model = new ParameterSetupDetailInfoDataModel();
            model.paraCode1 = frame.paraCode1.Data[0];
            model.paraCode2 = frame.paraCode2.Data[0];
            model.calFormat1 = frame.calFormat1.Data;
            model.calFormatAndScope = frame.calFormatAndScope.Data;
            model.fixed1 = frame.fixed1.Data[0];
            model.prompt = frame.prompt.Data;
            model.settingName1 = frame.settingName1.Data;
            model.settingName2 = frame.settingName2.Data;
            model.settingContent = frame.settingContent.Data;
            model.settingValue = frame.settingValue.Data[0];

            return model;
        }
    }
}
