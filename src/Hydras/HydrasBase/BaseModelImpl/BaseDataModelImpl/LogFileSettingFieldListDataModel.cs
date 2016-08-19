using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ConfigFrame.BaseModel;
using ConfigFrame.Util;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class LogFileSettingFieldListDataModel : List<LogFileSettingFieldDataModel>, IDataModel
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

        public string[] update(IModel iModel, string[] fieldName)
        {
            return null;
        }

        public void update(IModel temp)
        {

        }

        public void updateElement(LogFileSettingFieldDataModel model)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (model.sn == this[i].sn)
                {
                    this[i].update(model);
                    return;
                }
            }
            this.Add(model);
        }

        public LogFileSettingFieldDataModel getElementBySn(byte itemNum)
        {
            LogFileSettingFieldDataModel result = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].sn == itemNum)
                {
                    result = this[i];
                }
            }
            return result;
        }

        public string[] compare(IModel model)
        {
            return null;
        }
    }
}