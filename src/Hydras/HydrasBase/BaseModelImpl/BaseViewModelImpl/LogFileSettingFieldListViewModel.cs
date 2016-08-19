using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class LogFileSettingFieldListViewModel : List<LogFileSettingFieldViewModel>, IViewModel
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

        public LogFileSettingFieldViewModel getModelByFileNum(byte sn)
        {
            LogFileSettingFieldViewModel vModel = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Sn == sn)
                {
                    vModel = this[i];
                    break;
                }
            }
            return vModel;
        }
    }
}
