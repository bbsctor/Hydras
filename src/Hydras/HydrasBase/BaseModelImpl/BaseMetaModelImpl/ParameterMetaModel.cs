using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseMetaModelImpl
{
    public class ParameterMetaModel : MetaModel
    {
        public enum TYPE { PARAMETER_INFO, PARAMETER_NUM, PARAMETER_VALUE };
        public TYPE type;
    }
}

