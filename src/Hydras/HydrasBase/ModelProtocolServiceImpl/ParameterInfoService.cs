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
    public class ParameterInfoService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            if (action.Contains("getParameterInfo"))
            {
                //byte para = byte.Parse(action.Substring(17));
                byte para;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para);
                return genGetParameterInfo(para);
            }
            return null;
        }

        public IRequest[] genGetParameterInfo(byte para)
        {
            Request req = new Request();
            req.requestFrame = new Request_48Frame(para);
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {
            Response resp = (Response)response;
            Response_48Frame frame = (Response_48Frame)resp.responseFrame;
            ParameterMetaModel mModel = new ParameterMetaModel();
            ParameterInfoDataModel model = ParameterInfoParser.parse(frame);
            if (frame.Type == Response_48Frame.TYPE.PERAMETER_DETAIL)
            {
                mModel.type = ParameterMetaModel.TYPE.PARAMETER_INFO;
            }
            else
            {
                mModel.type = ParameterMetaModel.TYPE.PARAMETER_NUM;
            }
            mModel.Data = model;
            return mModel;
        }
    }
}

