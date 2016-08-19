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
    public class LogFileParameterValueController : HydrasBasicController
    {
        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();

            if (action.Contains("downloadLogFile") == true)
            {
                byte para1, para2;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                for (int i = 0; i < para2; i++)
                {
                    downloadLogFile(para1, i);
                }

            }
        }

        private void downloadLogFile(byte logNum, int sn)
        {
            string action = genAction("downloadLogFile", new string[] { ((int)logNum).ToString(),
            sn.ToString()});
            base.execute(action);
        }

        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                LogFileMetaModel cModel = mModel as LogFileMetaModel;
                if (cModel != null)
                {
                    LogFileParameterValueDataModel values = 
                        (LogFileParameterValueDataModel)cModel.Data;
                    switch (cModel.type)
                    {
                        case LogFileMetaModel.TYPE.DOWNLOAD_VALUES:
                            ((LogFileParameterValueListDataModel)model).updateElement(values);
                            break;
                    }
                }
            }
        }
    }
}

