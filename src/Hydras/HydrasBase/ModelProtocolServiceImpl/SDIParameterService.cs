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
    public class SDIParameterService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            switch (action)
            {
                case "getOrder":
                    return genGetOrder();
            }
            return null;
        }

        public IRequest[] genGetOrder()
        {
            Request req = new Request();
            req.requestFrame = new Request_67QueryingFrame();
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {
            Response resp = (Response)response;
            SDIParameterDataModel model = SDIParameterParser.parse((Response_67QueryingFrame)resp.responseFrame);
            SDIParameterMetaModel mModel = new SDIParameterMetaModel();
            mModel.type = SDIParameterMetaModel.TYPE.PARA_ORDER;
            mModel.Data = model;
            return mModel;
        }
    }
}
