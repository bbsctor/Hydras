using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class OnlineParameterValueViewModel : BasicModel, IViewModel
    {
        private List<string> values;

        public List<string> Values
        {
            get { return values; }
            set { values = value; }
        }
        public OnlineParameterValueViewModel()
        {
            values = new List<string>();
        }
    }
}
