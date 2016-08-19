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
    public class OnlineParameterValueService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            if (action.Equals("getOnlineParameterValue"))
            {
                return genGetOnlineParameterValue();
            }
            return null;
        }

        public IRequest[] genGetOnlineParameterValue()
        {
            Request req = new Request();
            req.requestFrame = new Request_4DFrame();
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {
            Response resp = (Response)response;
            Response_4DFrame frame = (Response_4DFrame)resp.responseFrame;
            ParameterMetaModel mModel = new ParameterMetaModel();
            OnlineParameterValueDataModel model = OnlineParameterValueParser.parse(frame);
            mModel.type = ParameterMetaModel.TYPE.PARAMETER_VALUE;
            mModel.Data = model;
            return mModel;
        }
    }
}

