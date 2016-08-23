using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.BaseService;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasBase.AppInterfaceImpl.BasicController;

namespace HydrasBase.AppInterfaceImpl
{
    public class LogFileController : BasicStaticController
    {
        private int logNum;
        private DeviceStorageController deviceStorageController;

        public DeviceStorageController DeviceStorageController
        {
            get { return deviceStorageController; }
            set { deviceStorageController = value;
            subController.Add(value);
            }
        }

        private LogFileBaseInfoController logFileBaseInfoController;

        public LogFileBaseInfoController LogFileBaseInfoController
        {
            get { return logFileBaseInfoController; }
            set { logFileBaseInfoController = value;
            subController.Add(value);
            }
        }

        private LogFileSettingFieldController logFileSettingFieldController;

        public LogFileSettingFieldController LogFileSettingFieldController
        {
            get { return logFileSettingFieldController; }
            set { logFileSettingFieldController = value;
            subController.Add(value);
            }
        }

        private LogFileParameterInfoController logFileParameterInfoController;

        public LogFileParameterInfoController LogFileParameterInfoController
        {
            get { return logFileParameterInfoController; }
            set { logFileParameterInfoController = value;
            subController.Add(value);
            }
        }

        private LogFileParameterValueController logFileParameterValueController;

        public LogFileParameterValueController LogFileParameterValueController
        {
            get { return logFileParameterValueController; }
            set { logFileParameterValueController = value;
            subController.Add(value);
            }
        }

        public override void execute(string action)
        {
            if ("readAllLogFileBaseInfo".Equals(action))
            {
                readDeviceStorage();
                logNum = ((DeviceStorageDataModel)deviceStorageController.model).logFilesNum;
                readAllLogFile();
            }
            else if ("readDeviceStorage".Equals(action))
            {
                readDeviceStorage();
            }
            else if (action.Contains("readLogFileAllInfo") == true)
            {
                byte para1;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                readLogFileAllInfo(para1);
            }
        }

        private void readLogFileAllInfo(byte searchNum)
        {
            string action = null;
            action = genAction("readLogFileBaseInfo", new string[] { ((int)searchNum).ToString() });
            logFileBaseInfoController.execute(action);

            LogFileBaseInfoDataModel baseModel = 
                ((LogFileBaseInfoListDataModel)LogFileBaseInfoController.model).getElementByBaseNum(searchNum);

            action = genAction("readLogFileSettingField", new string[] { ((int)baseModel.logNum).ToString() });
            logFileSettingFieldController.execute(action);

            action = genAction("readLogFileParameterInfo", new string[] { ((int)baseModel.logNum).ToString(),
                        ((int)baseModel.parasNum).ToString()});
            logFileParameterInfoController.execute(action);

            action = genAction("downloadLogFile", new string[] { ((int)baseModel.logNum).ToString(),
                (BitConverter.ToUInt32(baseModel.size_scans, 0)).ToString()});
            logFileParameterValueController.execute(action);
        }

        private void readDeviceStorage()
        {
            deviceStorageController.execute("getDeviceStorage");
        }

        private void readAllLogFile()
        {
            logFileBaseInfoController.resetDataModel();
            if ((LogFileBaseInfoListDataModel)logFileBaseInfoController.model != null)
            {
                ((LogFileBaseInfoListDataModel)(logFileBaseInfoController.model)).Clear();
            }
            for (int i = 0; i < logNum; i++)
            {
                string action = genAction("readLogFileBaseInfo", new string[] { (i + 1).ToString() });
                logFileBaseInfoController.execute(action);
            }
        }
    }
}