using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class LogFileParameterValueViewModel : BasicModel, IViewModel
    {
        public List<string> values;
        public LogFileParameterValueViewModel()
        {
            values = new List<string>();
        }
    }
}
