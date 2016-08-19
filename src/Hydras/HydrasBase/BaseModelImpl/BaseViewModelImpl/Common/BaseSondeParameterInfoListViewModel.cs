using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl.Common
{
    public class BaseSondeParameterInfoListViewModel : List<BaseSondeParameterInfoViewModel>, IViewModel
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

        public BaseSondeParameterInfoViewModel getModelByParaCode(byte logNum)
        {
            BaseSondeParameterInfoViewModel vModel = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ParaCode == logNum)
                {
                    vModel = this[i];
                    break;
                }
            }
            return vModel;
        }
    }
}

