using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class SondeInfoListDataModel : List<SondeInfoDataModel>, IDataModel
    {

        public bool Modified
        {
            get { return false; }
            set {  }
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

        public void updateElement(SondeInfoDataModel model)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].portModel.Equals(model.portModel))
                {
                    this[i].update(model);
                    return;
                }
            }
            this.Add(model);
        }

        public SondeInfoDataModel getElementByPortModel(SerialPortModel portModel)
        {
            SondeInfoDataModel result = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].portModel.BaudRate.Equals(portModel.BaudRate) == true &&
                    this[i].portModel.Port.Equals(portModel.Port) == true)
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
