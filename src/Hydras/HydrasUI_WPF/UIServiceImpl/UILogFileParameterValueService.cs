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
    public class UILogFileParameterValueService : UIBasicService
    {
        public UILogFileParameterValueService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("logFileParameterValueController");
            ((LogFileParameterValueController)controller).CommService.ConnectParameterModel = portModel;
        }

        public void downloadLogFile(byte logNum, uint sn)
        {
            string action = ActionStrAssistant.buildActionStr("downloadLogFile",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString()),
                        new ActionStrAssistant.ParameterValue("para2", ((uint)sn).ToString())
                    });
            controller.execute(action);
        }
    }
}