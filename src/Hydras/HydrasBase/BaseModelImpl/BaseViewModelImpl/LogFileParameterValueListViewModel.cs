using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class LogFileParameterValueListViewModel : List<LogFileParameterValueViewModel>, IViewModel
    {

        public bool Modified
        {
            get { return false; }
            set { }
        }

        public List<string> ModifiedFieldList
        {
            get { return null; }
            set { }
        }

        public void update(IModel model)
        {

        }

        public string[] update(IModel model, string[] fields)
        {
            return null;
        }

        public string[] compare(IModel model)
        {
            return null;
        }

    }
}
