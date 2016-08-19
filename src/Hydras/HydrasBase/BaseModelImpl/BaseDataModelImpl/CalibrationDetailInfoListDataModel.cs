using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class CalibrationDetailInfoListDataModel : List<CalibrationDetailInfoDataModel>, IDataModel
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

        public void updateElement(CalibrationDetailInfoDataModel model)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].paraCode1 == model.paraCode1 && this[i].paraCode2 == model.paraCode2)
                {
                    this[i].update(model);
                    return;
                }
            }
            this.Add(model);
        }

        public CalibrationDetailInfoDataModel getElementByNum(byte baseNum)
        {
            CalibrationDetailInfoDataModel result = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].paraCode2 == baseNum)
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
