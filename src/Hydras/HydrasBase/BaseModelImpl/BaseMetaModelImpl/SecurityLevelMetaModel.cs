using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseMetaModelImpl
{
    public class SecurityLevelMetaModel : MetaModel
    {
        public enum TYPE { SECURITY_LEVEL };
        public TYPE type;
    }
}