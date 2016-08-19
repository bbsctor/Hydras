using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class SystemAndSettingDataModel : BasicModel, IDataModel
    {
        public byte paraCode1;
        //setting paraNum
        public byte paraNum;

        //setting parameter
        public byte[] paraName;
        //public byte[] paraNameAndSettingType;
        public byte[] paraNameForShort;
        public byte[] settingType;
        public byte[] paraFormat;
        public byte[] settingContent;
        public byte[] paraFormatAndScope;
        public byte[] prompt;

        public override void update(IModel model)
        {
            update(model, new string[]{"settingContent"});
        }
    }
}
