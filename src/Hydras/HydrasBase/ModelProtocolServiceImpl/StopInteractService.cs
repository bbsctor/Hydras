using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationDataWrapper;
using HydrasBase.CommunicationDataWrapper;
using HydrasBase.ModelProtocolServiceImpl.FrameParser;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl
{
    public class StopInteractService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            switch (action)
            {
                case "stopInteract":
                    return genStopInteract();
            }
            return null;
        }

        public IRequest[] genStopInteract()
        {
            Request req = new Request();
            req.requestFrame = new Request_42Frame();
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {
            return MetaModel.NULL;
        }
    }
}
