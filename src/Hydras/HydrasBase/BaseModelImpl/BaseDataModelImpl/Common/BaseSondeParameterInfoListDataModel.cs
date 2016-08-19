using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl.Common
{
    public class BaseSondeParameterInfoListDataModel : List<BaseSondeParameterInfoDataModel>, IDataModel
    {
        public BaseSondeParameterInfoListDataModel addedList;
        public BaseSondeParameterInfoListDataModel removedList;

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
            if (addedList == null)
            {
                addedList = new BaseSondeParameterInfoListDataModel();
            }
            else
            {
                addedList.Clear();
            }

            if (removedList == null)
            {
                removedList = new BaseSondeParameterInfoListDataModel();
            }
            else
            {
                removedList.Clear();
            }
            List<BaseSondeParameterInfoDataModel> newList = temp as List<BaseSondeParameterInfoDataModel>;
            for (int i = 0; i < this.Count; i++)
            {
                //if (newList.Contains(this[i]) == false)
                //{
                    removedList.Add(this[i]);
                //}
            }

            for (int i = 0; i < newList.Count; i++)
            {
                //if (this.Contains(newList[i]) == false)
                //{
                    addedList.Add(newList[i]);
                //}
            }

            //for (int i = 0; i < removedList.Count; i++)
            //{
            //    addedList.Remove(removedList[i]);
            //}
        }

        public void updateElement(BaseSondeParameterInfoDataModel model)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].sn == model.sn)
                {
                    this[i].update(model);
                    return;
                }
            }
            this.Add(model);
        }

        public string[] compare(IModel model)
        {
            return null;
        }
    }
}

