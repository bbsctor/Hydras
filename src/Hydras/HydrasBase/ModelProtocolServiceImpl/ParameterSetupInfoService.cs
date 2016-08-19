using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.Util;
using HydrasBase.CommunicationDataWrapper;
using HydrasBase.ModelProtocolServiceImpl.FrameParser;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl
{
    public class ParameterSetupInfoService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            if (action.Contains("getBaseInfo"))
            {
                byte para ;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para);
                return genGetBaseInfo(para);
            }
            else if (action.Contains("getDetailInfo"))
            {
                byte para1, para2;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                return genGetDetailInfo(para1, para2);
            }
            else if (action.Contains("setParameter"))
            {
                byte para1;
                byte[] para2;
                byte[] para3;
                byte para4;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                para2 = StringByteConverter.stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para2"));
                para3 = StringByteConverter.stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para3"));
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para4"), out para4);
                return genSetParameter(para1, para2, para3, para4);
            }
            return null;
        }

        public IRequest[] genSetParameter(byte para1, byte[] para2, byte[] para3, byte para4)
        {
            Request req = new Request();
            req.requestFrame = new Request_4AFrame(para1, para2, para3, para4);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genGetBaseInfo(byte para)
        {
            Request req = new Request();
            req.requestFrame = new Request_49BaseFrame(para);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genGetDetailInfo(byte para1, byte para2)
        {
            Request req = new Request();
            req.requestFrame = new Request_49DetailFrame(para1, para2);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genSetInfo(byte para1, byte[] para2, byte[] para3, byte para4)
        {
            Request req = new Request();
            req.requestFrame = new Request_4AFrame(para1, para2, para3, para4);
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {
            Response resp = (Response)response;
            ParameterSetupMetaModel mModel = null;
            if (resp.responseFrame is Response_49BaseFrame)
            {
                mModel = new ParameterSetupMetaModel();
                Response_49BaseFrame frame = (Response_49BaseFrame)resp.responseFrame;
                ParameterSetupBaseInfoDataModel model = ParameterSetupBaseInfoParser.parse(frame);
                mModel.type = ParameterSetupMetaModel.TYPE.BASE_INFO;
                mModel.Data = model;
            }
            else if (resp.responseFrame is Response_49DetailFrame)
            {
                mModel = new ParameterSetupMetaModel();
                Response_49DetailFrame frame = (Response_49DetailFrame)resp.responseFrame;
                ParameterSetupDetailInfoDataModel model = ParameterSetupDetailInfoParser.parse(frame);
                mModel.type = ParameterSetupMetaModel.TYPE.DETAIL_INFO;
                mModel.Data = model;
            }
            
            return mModel;
        }
    }
}