using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class LogFileBaseInfoListViewModel : List<LogFileBaseInfoViewModel>, IViewModel
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

        public LogFileBaseInfoViewModel getModelByFileName(string fileName)
        {
            LogFileBaseInfoViewModel vModel = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].LogFileName.Equals(fileName))
                {
                    vModel = this[i];
                    break;
                }
            }
            return vModel;
        }

        public LogFileBaseInfoViewModel getModelByLogNum(byte logNum)
        {
            LogFileBaseInfoViewModel vModel = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].LogNum == logNum)
                {
                    vModel = this[i];
                    break;
                }
            }
            return vModel;
        }
    }
}
