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
    public class UILogFileSettingFieldService : UIBasicService
    {
        public UILogFileSettingFieldService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("logFileSettingFieldController");
            ((LogFileSettingFieldController)controller).CommService.ConnectParameterModel = portModel;
        }
        public void getLogFileSettingField(byte logNum)
        {
            string action = ActionStrAssistant.buildActionStr("readLogFileSettingField",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString())
                    });
            controller.execute(action);
        }

        internal void setLogFileSettingField(byte logNum)
        {
            string action = ActionStrAssistant.buildActionStr("updateLogFileSettingField",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString())
                    });
            controller.execute(action);
        }
    }
}

