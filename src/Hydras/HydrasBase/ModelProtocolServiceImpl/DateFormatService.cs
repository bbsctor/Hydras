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
    public class DateFormatService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            if (action.Contains("getDateFormat"))
            {
                //byte para1, para2;
                //byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                //byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                return genGetSetDateFormat(0xFF, 0x00);
            }
            else if (action.Contains("setDateFormat"))
            {
                byte para1, para2;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                return genGetSetDateFormat(para1, para2);
            }
            return null;
        }

        public IRequest[] genGetSetDateFormat(byte para1, byte para2)
        {
            Request req = new Request();
            req.requestFrame = new Request_69Frame(para1, para2);
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {

            Response resp = (Response)response;
            DateFormatMetaModel mModel = new DateFormatMetaModel();
            if (resp.responseFrame is Response_69Frame)
            {
                Response_69Frame frame = (Response_69Frame)resp.responseFrame;
                DateFormatDataModel model = DateFormatParser.parse(frame);
                mModel.type = DateFormatMetaModel.TYPE.FORMAT_INFO;
                mModel.Data = model;
            }
            
            return mModel;
        }
    }
}
