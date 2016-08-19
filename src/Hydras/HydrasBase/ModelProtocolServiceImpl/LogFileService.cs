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
    public class LogFileService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            if (action.Contains("createLogFile"))
            {
                byte[] fileName = StringByteConverter.
                    stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para1"));
                return genCreateLogFile(fileName);
            }
            else if (action.Contains("deleteLogFile"))
            {
                byte para1;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                byte[] fileName = StringByteConverter.
                    stringToByteArray(ActionStrAssistant.getParameterStrValue(action, "para2"));
                return genDeleteLogFile(para1, fileName);
            }
            else if (action.Contains("readLogFileBaseInfo"))
            {
                byte para1;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                return genReadLogFileBaseInfo(para1);
            }
            else if (action.Contains("readLogFileParameterInfo"))
            {
                byte para1, para2;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                return genReadLogFileParameterInfo(para1, para2);
            }
            else if (action.Contains("readLogFileSettingField"))
            {
                byte para1,para2;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                return genReadLogFileSettingField(para1, para2);
            }
            else if (action.Contains("updateLogFileSettingField"))
            {
                //para1 logNum
                //para2 settingName
                //para3 settingContent
                //para4 settingValue
                byte para1;
                byte[] para2;
                byte[] para3;
                byte[] para4;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                para2 = StringByteConverter.parseByteArrayString(ActionStrAssistant.getParameterStrValue(action, "para2"));
                para3 = StringByteConverter.parseByteArrayString(ActionStrAssistant.getParameterStrValue(action, "para3"));
                para4 = StringByteConverter.parseByteArrayString(ActionStrAssistant.getParameterStrValue(action, "para4"));
                return genUpdateLogFileSettingField(para1, para2, para3, para4);
            }
            else if (action.Contains("removeLogFileParameterInfo"))
            {
                //para1 logNum
                //para2 paraCode
                byte para1, para2;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                return genRemoveLogFileParameterInfo(para1, para2);
            }
            else if (action.Contains("addLogFileParameterInfo"))
            {
                //para1 logNum
                //para2 paraCode
                byte para1, para2;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                return genAddLogFileParameterInfo(para1, para2);
            }
            else if (action.Contains("enableLogFile"))
            {
                byte para1;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                return genSetEnable(0x01, para1);
            }
            else if (action.Contains("disableLogFile"))
            {
                byte para1;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                return genSetEnable(0x02, para1);
            }
            else if (action.Contains("downloadLogFile"))
            {
                byte para1, para2;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                return genDownload(para1, para2);
            }
            return null;
        }

        public IRequest[] genSetEnable(byte para, byte logNum)
        {
            Request req = new Request();
            req.requestFrame = new Request_50AbleFrame(para, logNum);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genDownload(byte logNum, byte sn)
        {
            Request req = new Request();
            req.requestFrame = new Request_51DownloadLogFrame(logNum, sn);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genCreateLogFile(byte[] para)
        {
            Request req = new Request();
            req.requestFrame = new Request_50FileNameFrame(para);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genDeleteLogFile(byte para1, byte[] para2)
        {
            Request req = new Request();
            req.requestFrame = new Request_52Frame(para1, para2);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genReadLogFileBaseInfo(byte para1)
        {
            Request req = new Request();
            req.requestFrame = new Request_4EQueryLogInfoFrame(para1);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genUpdateLogFileSettingField(byte para1, byte[] para2, byte[] para3, byte[] para4)
        {
            //para1 logNum
            //para2 settingName
            //para3 settingContent
            //para4 settingValue
            Request req = new Request();
            req.requestFrame = new Request_50SettingFieldFrame(para1, para2, para3, para4);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genRemoveLogFileParameterInfo(byte para1, byte para2)
        {
            //para1 logNum
            //para2 paraCode
            Request req = new Request();
            req.requestFrame = new Request_50ParameterFrame(0x05, para1, para2);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genAddLogFileParameterInfo(byte para1, byte para2)
        {
            //para1 logNum
            //para2 paraCode
            Request req = new Request();
            req.requestFrame = new Request_50ParameterFrame(0x04, para1, para2);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genReadLogFileParameterInfo(byte para1, byte para2)
        {
            Request req = new Request();
            req.requestFrame = new Request_4FSelectedParaFrame(para1, para2);
            return new RequestWrapper(req).getRequestArray();
        }

        public IRequest[] genReadLogFileSettingField(byte para1, byte para2)
        {
            Request req = new Request();
            req.requestFrame = new Request_4FSettingFieldFrame(para1, para2);
            return new RequestWrapper(req).getRequestArray();
        }

        //public IRequest[] genSetSettingField(byte[] para1, byte[] para2, byte[] para3)
        //{
        //    Request req = new Request();
        //    req.requestFrame = new Request_50SettingFieldFrame(para1, para2, para3);
        //    return new RequestWrapper(req).getRequestArray();
        //}

        public override IMetaModel handleResponse(IResponse response)
        {

            Response resp = (Response)response;
            LogFileMetaModel mModel = null;
            if (resp.responseFrame is Response_4EQueryLogInfoFrame)
            {
                mModel = new LogFileMetaModel();
                Response_4EQueryLogInfoFrame frame = (Response_4EQueryLogInfoFrame)resp.responseFrame;
                LogFileBaseInfoDataModel model = LogFileBaseInfoParser.parse(frame);
                mModel.type = LogFileMetaModel.TYPE.BASE_INFO;
                mModel.Data = model;
            }
            else if (resp.responseFrame is Response_4FSettingFieldFrame)
            {
                mModel = new LogFileMetaModel();
                mModel = new LogFileMetaModel();
                Response_4FSettingFieldFrame frame = (Response_4FSettingFieldFrame)resp.responseFrame;
                LogFileSettingFieldDataModel model = LogFileSettingFieldParser.parse(frame);
                mModel.type = LogFileMetaModel.TYPE.SETTING_FIELD;
                mModel.Data = model;
            }
            else if (resp.responseFrame is Response_4FSelectedParaFrame)
            {
                mModel = new LogFileMetaModel();
                Response_4FSelectedParaFrame frame = (Response_4FSelectedParaFrame)resp.responseFrame;
                LogFileParameterInfoDataModel model = LogFileParameterInfoParser.parse(frame);
                mModel.type = LogFileMetaModel.TYPE.SELECTED_PARAMETER;
                mModel.Data = model;
            }
            else if (resp.responseFrame is Response_51DownloadLogFrame)
            {
                mModel = new LogFileMetaModel();
                Response_51DownloadLogFrame frame = (Response_51DownloadLogFrame)resp.responseFrame;
                LogFileParameterValueDataModel values = LogFileParameterValueParser.parse(frame);
                mModel.type = LogFileMetaModel.TYPE.DOWNLOAD_VALUES;
                mModel.Data = values;
            }
            return mModel;
        }
    }
}
