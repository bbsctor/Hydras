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
    public class SystemAndSettingService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            if (action.Contains("getSystemAndSetting"))
            {
                byte para;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para);
                return genGetSystemAndSetting(para);
            }
            else if (action.Contains("setID"))
            {
                byte[] para1, para2, para3;
                para1 = StringByteConverter.stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para1"));
                para2 = StringByteConverter.stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para2"));
                para3 = StringByteConverter.stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para3"));
                return genSetSystemAndSetting(para1, para2, para3);
            }
            else if (action.Contains("setOthers"))
            {
                byte[] para1, para2, para3;
                para1 = StringByteConverter.stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para1"));
                para2 = StringByteConverter.stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para2"));
                para3 = StringByteConverter.parseByteArrayString(ActionStrAssistant.getParameterStrValue(action, "para3"));
                return genSetSystemAndSetting(para1, para2, para3);
            }
            return null;
        }

        public IRequest[] genGetSystemAndSetting(byte para)
        {
            Request req = new Request();
            req.requestFrame = new Request_46Frame(para);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genSetSystemAndSetting(byte[] para1, byte[] para2, byte[] para3)
        {
            Request req = new Request();
            req.requestFrame = new Request_47Frame(para1, para2, para3);
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {
            Response resp = (Response)response;
            SystemAndSettingMetaModel mModel = null;
            if (resp.responseFrame is Response_46Frame)
            {
                mModel = new SystemAndSettingMetaModel();
                Response_46Frame frame = (Response_46Frame)resp.responseFrame;

                SystemAndSettingDataModel model = SystemAndSettingParser.parse(frame);
                if (frame.Type == Response_46Frame.TYPE.PERAMETER_DETAIL)
                {
                    mModel.type = SystemAndSettingMetaModel.TYPE.SETTING_PARAMETER;
                }
                else
                {
                    mModel.type = SystemAndSettingMetaModel.TYPE.PARAMETER_NUM;
                }
                mModel.Data = model;
            }
            return mModel;
        }
    }
}
