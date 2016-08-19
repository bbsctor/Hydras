using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseMetaModelImpl
{
    public class LogFileMetaModel : MetaModel
    {
        public enum TYPE { BASE_INFO, SETTING_FIELD, SELECTED_PARAMETER, DOWNLOAD_VALUES };
        public TYPE type;
    }
}