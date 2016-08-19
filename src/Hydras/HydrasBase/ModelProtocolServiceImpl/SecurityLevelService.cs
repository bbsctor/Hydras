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
    public class SecurityLevelService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            if (action.Equals("getSecurityLevel"))
            {
                return genGetSecurityLevel();
            }
            else if (action.Equals("setPassword"))
            {
                return genSetPassword();
            }
            else if (action.Contains("setSingleLevelPassword"))
            {
                byte para1;
                byte[] para2;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                para2 = StringByteConverter.stringToByteArray( ActionStrAssistant.getParameterStrValue(action, "para2"));
                return genSetSingleLevelPassword(para1, para2);
            }
            return null;
        }

        public IRequest[] genSetPassword()
        {
            Request req = new Request();
            req.requestFrame = new Request_41Frame();
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genSetSingleLevelPassword(byte para1, byte[] para2)
        {
            Request req = new Request();
            req.requestFrame = new Request_44Frame(para1, para2);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genGetSecurityLevel()
        {
            Request req = new Request();
            req.requestFrame = new Request_43Frame();
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {
            Response resp = (Response)response;
            SecurityLevelMetaModel mModel = null;

            if (resp.responseFrame is Response_43Frame)
            {
                SecurityLevelDataModel model = SecurityLevelParser.parse((Response_43Frame)resp.responseFrame);
                mModel = new SecurityLevelMetaModel();
                mModel.type = SecurityLevelMetaModel.TYPE.SECURITY_LEVEL;
                mModel.Data = model;
            }
            return mModel;
        }
    }
}
