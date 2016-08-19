using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class SondeDataOptionDataModel : BasicModel, IDataModel
    {
        public string[] portArray;
        public string[] baudRateArray;

        public override void update(IModel model)
        {
            this.baudRateArray = ((SondeDataOptionDataModel)model).baudRateArray;
            this.portArray = ((SondeDataOptionDataModel)model).portArray;
        }
    }
}
