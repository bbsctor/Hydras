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
    public class CalibrationInfoService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            if (action.Contains("getBaseInfo"))
            {
                byte para;
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
                byte[] para4;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                para2 = StringByteConverter.stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para2"));
                para3 = StringByteConverter.stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para3"));
                para4 = StringByteConverter.parseByteArrayString(ActionStrAssistant.getParameterStrValue(action, "para4"));
                return genSetParameter(para1, para2, para3, para4);
            }
            else if (action.Contains("reset"))
            {
                byte para1;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                return genReset(para1);
            }

            return null;
        }

        public IRequest[] genReset(byte para1)
        {
            Request req = new Request();
            req.requestFrame = new Request_68Frame(para1);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genSetParameter(byte para1, byte[] para2, byte[] para3, byte[] para4)
        {
            Request req = new Request();
            req.requestFrame = new Request_4CFrame(para1, para2, para3, para4);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genGetBaseInfo(byte para)
        {
            Request req = new Request();
            req.requestFrame = new Request_4BAllFrame(para);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genGetDetailInfo(byte para1, byte para2)
        {
            Request req = new Request();
            req.requestFrame = new Request_4BDetailFrame(para1, para2);
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {

            Response resp = (Response)response;
            CalibrationMetaModel mModel = null;
            if (resp.responseFrame is Response_4BAllFrame)
            {
                mModel = new CalibrationMetaModel();
                Response_4BAllFrame frame = (Response_4BAllFrame)resp.responseFrame;
                CalibrationBaseInfoDataModel model = CalibrationBaseInfoParser.parse(frame);
                mModel.type = CalibrationMetaModel.TYPE.BASE_INFO;
                mModel.Data = model;
            }
            else if (resp.responseFrame is Response_4BDetailFrame)
            {
                mModel = new CalibrationMetaModel();
                Response_4BDetailFrame frame = (Response_4BDetailFrame)resp.responseFrame;
                CalibrationDetailInfoDataModel model = CalibrationDetailInfoParser.parse(frame);
                mModel.type = CalibrationMetaModel.TYPE.DETAIL_INFO;
                mModel.Data = model;
            }

            return mModel;
        }
    }
}
