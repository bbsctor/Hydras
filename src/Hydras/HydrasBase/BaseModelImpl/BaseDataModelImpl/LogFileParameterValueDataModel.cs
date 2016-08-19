using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class LogFileParameterValueDataModel : BasicModel, IDataModel
    {
        public List<byte[]> values;
        public LogFileParameterValueDataModel()
        {
            values = new List<byte[]>();
        }
    }
}
