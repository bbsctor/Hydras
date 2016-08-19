using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseService;
using ConfigFrame.Util;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.ModelProtocolServiceImpl
{
    public class BasicService : IModelProtocolService
    {

        public virtual IRequest[] genIdleRequest()
        {
            return null;
        }

        public virtual IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            return null;
        }

        public virtual IMetaModel handleResponse(IResponse response)
        {
            return null;
        }
    }
}
