using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseMetaModelImpl
{
    public class ParameterSetupMetaModel : MetaModel
    {
        public enum TYPE { BASE_INFO, DETAIL_INFO};
        public TYPE type;
    }
}
