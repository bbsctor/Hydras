using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;

namespace ConfigFrame.BaseService
{
    public interface IModelProtocolService
    {
        IMetaModel handleResponse(IResponse resps);
        IRequest[] genRequestByActionName(string name, IDataModel model);
        IRequest[] genIdleRequest();
    }
}
