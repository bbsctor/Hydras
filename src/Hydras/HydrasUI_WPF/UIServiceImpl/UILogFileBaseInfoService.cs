using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.UITask;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.BaseService;
using HydrasFacade;
using HydrasBase.AppInterfaceImpl;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasBase.AppInterfaceImpl.BasicController;

namespace HydrasUI_WPF.UIServiceImpl
{
    public class UILogFileBaseInfoService : UIBasicService
    {
        public UILogFileBaseInfoService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("logFileBaseInfoController");
            ((LogFileBaseInfoController)controller).CommService.ConnectParameterModel = portModel;
        }
        public void getLogFileBaseInfo(byte logNum)
        {
            string action = ActionStrAssistant.buildActionStr("readLogFileBaseInfo",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString())
                    });
            controller.execute(action);
        }

        public void createLogFile(string fileName)
        {
            string action = ActionStrAssistant.buildActionStr("createLogFile",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", fileName)
                    });
            controller.execute(action);
        }

        public void deleteLogFile(byte logNum, string fileName)
        {
            string action = ActionStrAssistant.buildActionStr("deleteLogFile",
                     new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString()),
                        new ActionStrAssistant.ParameterValue("para2", fileName)
                    });
            controller.execute(action);
        }

        public void enableLogFile(byte logNum)
        {
            string action = ActionStrAssistant.buildActionStr("enableLogFile",
                     new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString())
                    });
            controller.execute(action);
        }

        public void disableLogFile(byte logNum)
        {
            string action = ActionStrAssistant.buildActionStr("disableLogFile",
                     new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString())
                    });
            controller.execute(action);
        }
    }
}



