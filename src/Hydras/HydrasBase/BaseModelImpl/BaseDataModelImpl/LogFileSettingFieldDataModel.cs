using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class LogFileSettingFieldDataModel : BasicModel, IDataModel
    {
        public byte sn;
        public byte[] settingName;
        public byte[] settingNameForShort;
        public byte[] settingContent;
        public byte[] countFormat1;
        public byte[] settingValue;
        public byte[] countFormatAndScope;
        public byte[] prompt;

        public override void update(IModel model)
        {
            update(model, new string[] { "settingValue"});
        }
    }
}