using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class SystemAndSettingParser
    {
        public static SystemAndSettingDataModel parse(Response_46Frame frame)
        {
            SystemAndSettingDataModel model = new SystemAndSettingDataModel();
            model.paraCode1 = frame.paraCode1.Data[0];

            if (frame.paraNum.Data != null)
            {
                model.paraNum = frame.paraNum.Data[0];
            }

            model.paraFormat = frame.paraFormat.Data;
            model.paraFormatAndScope = frame.paraFormatAndScope.Data;
            model.paraName = frame.paraName.Data;
            //model.paraNameAndSettingType = frame.paraNameAndSettingType.Data;
            model.paraNameForShort = frame.paraNameForShort.Data;
            model.settingType = frame.settingType.Data;
            model.prompt = frame.prompt.Data;
            model.settingContent = frame.settingContent.Data;

            return model;
        }
    }
}
