using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ConfigFrame.BaseModel;
using ConfigFrame.Util;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class LogFileBaseInfoListDataModel : List<LogFileBaseInfoDataModel>, IDataModel
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

        public void updateElement(LogFileBaseInfoDataModel model)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (model.logNum == this[i].logNum)
                {
                    //this[i].update(model);
                    this[i] = model;
                    return;
                }
            }
            this.Add(model);
        }

        public LogFileBaseInfoDataModel getElementByBaseNum(byte baseNum)
        {
            LogFileBaseInfoDataModel result = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].searchNum == baseNum)
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

