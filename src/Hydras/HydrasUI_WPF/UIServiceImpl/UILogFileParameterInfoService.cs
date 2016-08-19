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
    public class UILogFileParameterInfoService : UIBasicService
    {
        public UILogFileParameterInfoService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("logFileParameterInfoController");
            ((LogFileParameterInfoController)controller).CommService.ConnectParameterModel = portModel;
        }
        public void getLogFileParameterInfo(byte logNum, byte parasNum)
        {
            string action = ActionStrAssistant.buildActionStr("readLogFileParameterInfo",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString()),
                        new ActionStrAssistant.ParameterValue("para2", ((int)parasNum).ToString())
                    });
            controller.execute(action);
        }

        public void setLogFileParameterInfo(byte logNum)
        {
            string action = ActionStrAssistant.buildActionStr("updateLogFileParameterInfo",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString())
                    });
            controller.execute(action);
        }
    }
}


