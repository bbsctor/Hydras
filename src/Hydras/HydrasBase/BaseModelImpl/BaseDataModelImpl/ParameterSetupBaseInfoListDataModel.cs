using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class ParameterSetupBaseInfoListDataModel : List<ParameterSetupBaseInfoDataModel>, IDataModel
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

        public void updateElement(ParameterSetupBaseInfoDataModel model)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].para1 == model.para1)
                {
                    this[i].update(model);
                    return;
                }
            }
            this.Add(model);
        }

        public ParameterSetupBaseInfoDataModel getElementByBaseNum(byte baseNum)
        {
            ParameterSetupBaseInfoDataModel result = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].para1 == baseNum)
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
