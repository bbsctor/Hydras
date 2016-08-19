using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl.Common
{
    public class BaseSondeParameterInfoDataModel : BasicModel, IDataModel
    {
        public byte sn;
        //setting paraNum
        public byte paraNum;

        //setting parameter
        public byte paraCode;
        public byte constByte1;
        public byte[] paraName;
        public byte[] paraNameForShort;
        public byte[] calUnit;
        public byte[] formatAndScope;
        public byte[] calFormat1;
        public byte[] calFormat2;
        //public byte[] unknown1;

        public override void update(IModel model)
        {

            update(model, new string[]{"paraCode1", "paraNum", "paraCode2", "fixed1", "paraName",
                "paraNameForShort", "calUnit", "formatAndScope", "calFormat1", "calFormat2", "unknown1"});
        }

        public override bool Equals(object obj)
        {
            BaseSondeParameterInfoDataModel model = obj as BaseSondeParameterInfoDataModel;
            if (model != null && model.paraCode == this.paraCode)
            {
                return true;
            }
            return false;
        }
    }
}

