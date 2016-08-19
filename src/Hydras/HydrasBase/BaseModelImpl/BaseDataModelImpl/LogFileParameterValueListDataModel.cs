using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ConfigFrame.BaseModel;
using ConfigFrame.Util;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class LogFileParameterValueListDataModel : List<LogFileParameterValueDataModel>, IDataModel
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

        public void updateElement(LogFileParameterValueDataModel model)
        {
            this.Add(model);
        }

        public string[] update(IModel iModel, string[] fieldName)
        {
            return null;
        }

        public void update(IModel temp)
        {

        }


        public string[] compare(IModel model)
        {
            return null;
        }
    }
}


