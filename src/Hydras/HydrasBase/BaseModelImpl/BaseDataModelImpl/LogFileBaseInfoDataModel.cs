using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class LogFileBaseInfoDataModel : BasicModel, IDataModel
    {
        public byte searchNum;
        public byte logNum;
        public byte autoLogState;
        public byte[] logFileName;
        public byte[] startTime;
        public byte[] size_bytes;
        public byte[] size_scans;
        public byte parasNum;
        public byte settingFieldsNum;

        public override void update(IModel model)
        {
            update(model, new string[]{"mode", "autoLogState","logFileName", "startTime", "size_bytes", "size_scans",
                "parasNum", "settingFieldsNum"});
        }
    }
}
