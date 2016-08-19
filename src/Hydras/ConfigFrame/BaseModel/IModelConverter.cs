using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.BaseModel
{
    public interface IModelConverter
    {
        IDataModel genDataModel(IViewModel vModel);
        IViewModel genViewModel(IDataModel dModel);
    }
}
