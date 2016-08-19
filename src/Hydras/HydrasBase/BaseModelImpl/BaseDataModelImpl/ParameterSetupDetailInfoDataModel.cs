using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class ParameterSetupDetailInfoDataModel : BasicModel, IDataModel
    {
        public byte paraCode1;
        public byte fixed1;
        public byte paraCode2;
        public byte[] settingName1;
        public byte[] settingName2;
        public byte[] settingContent;
        public byte[] calFormat1;
        public byte settingValue;
        public byte[] calFormatAndScope;
        public byte[] prompt;
    }
}