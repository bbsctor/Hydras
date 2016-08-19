using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasBase.AppInterfaceImpl.BasicController;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class LogFileBaseInfoController : HydrasBasicController
    {
        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();

            if (action.Contains("readLogFileBaseInfo") == true)
            {
                int para;
                int.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para);
                readLogFileBaseInfo(para);
            }
            else if (action.Contains("createLogFile") == true)
            {
                string para = ActionStrAssistant.getParameterStrValue(action, "para1");
                createLogFile(para);
            }
            else if (action.Contains("deleteLogFile") == true)
            {
                byte para1;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                string para2 = ActionStrAssistant.getParameterStrValue(action, "para2");
                deleteLogFile(para1, para2);
            }
            else if (action.Contains("enableLogFile") == true)
            {
                byte para;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para);
                enableLogFile(para);
            }
            else if (action.Contains("disableLogFile") == true)
            {
                byte para;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para);
                disableLogFile(para);
            }
        }

        private void readLogFileBaseInfo(int num)
        {
            string action = genAction("readLogFileBaseInfo", new string[]{num.ToString()});
            base.execute(action);
        }

        private void createLogFile(string fileName)
        {
            string action = genAction("createLogFile", new string[] { fileName });
            base.execute(action);
        }

        private void deleteLogFile(byte logNum, string fileName)
        {
            string action = genAction("deleteLogFile", new string[] {((int)logNum).ToString(), fileName });
            base.execute(action);
        }

        private void enableLogFile(byte logNum)
        {
            string action = genAction("enableLogFile", new string[] { ((int)logNum).ToString()});
            base.execute(action);
        }

        private void disableLogFile(byte logNum)
        {
            string action = genAction("disableLogFile", new string[] { ((int)logNum).ToString() });
            base.execute(action);
        }

        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                LogFileMetaModel cModel = mModel as LogFileMetaModel;
                if (cModel != null)
                {
                    LogFileBaseInfoDataModel sondePara = (LogFileBaseInfoDataModel)cModel.Data;
                    switch (cModel.type)
                    {
                        case LogFileMetaModel.TYPE.BASE_INFO:

                            ((LogFileBaseInfoListDataModel)model).updateElement(sondePara);
                            break;
                    }
                }
            }
        }
    }
}
