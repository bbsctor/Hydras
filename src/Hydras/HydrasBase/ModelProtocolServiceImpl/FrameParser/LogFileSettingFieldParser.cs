using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class LogFileSettingFieldParser
    {
        public static LogFileSettingFieldDataModel parse(Response_4FSettingFieldFrame frame)
        {
            LogFileSettingFieldDataModel model = new LogFileSettingFieldDataModel();
            model.countFormat1 = frame.countFormat1.Data;
            model.countFormatAndScope = frame.countFormatAndScope.Data;
            model.prompt = frame.prompt.Data;
            model.settingContent = frame.settingContent.Data;
            model.settingName = frame.settingName.Data;
            model.settingNameForShort = frame.settingNameForShort.Data;
            model.sn = frame.sn.Data[0];
            model.settingValue = frame.settingValue.Data;

            return model;
        }
    }
}
