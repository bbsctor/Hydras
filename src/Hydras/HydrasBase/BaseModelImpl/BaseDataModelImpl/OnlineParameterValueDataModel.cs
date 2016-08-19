using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class OnlineParameterValueDataModel : BasicModel, IDataModel
    {
        public List<byte[]> values;

        public OnlineParameterValueDataModel()
        {
            values = new List<byte[]>();
        }

        public override void update(IModel model)
        {
            values = ((OnlineParameterValueDataModel)model).values;
        }
    }
}

