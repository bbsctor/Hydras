using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseMetaModelImpl
{
    public class SystemAndSettingMetaModel : MetaModel
    {
        public enum TYPE { SETTING_PARAMETER, PARAMETER_NUM};
        public TYPE type;
    }
}
