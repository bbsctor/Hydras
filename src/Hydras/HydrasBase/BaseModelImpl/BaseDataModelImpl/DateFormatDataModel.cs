using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class DateFormatDataModel : BasicModel, IDataModel
    {
        public byte format;
        public byte useDelimiter;

        public override void update(IModel model)
        {
            update(model, new string[]{"format", "useDelimiter"});
        }
    }
}

