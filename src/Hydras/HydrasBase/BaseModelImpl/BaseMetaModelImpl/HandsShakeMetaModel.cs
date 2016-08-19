using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseMetaModelImpl
{
    public class HandsShakeMetaModel : MetaModel
    {
        public enum TYPE { SONDE_INFO, INIT_INFO };
        public TYPE type;
    }
}
