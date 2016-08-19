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
using HydrasUI_WPF.Util;
using HydrasBase.AppInterfaceImpl.BasicController;

namespace HydrasUI_WPF.UIServiceImpl
{
    public class UILogFileService : UIBasicService
    {
        public UILogFileService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("logFileController");
            ((LogFileController)controller).CommService.ConnectParameterModel = portModel;
        }
        public void getAllLogFileBaseInfo()
        {
            controller.execute("readAllLogFileBaseInfo");
        }

        public void updateLogFileAllInfo(byte logNum)
        {
            string action = ActionStrAssistant.buildActionStr("readLogFileAllInfo",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)logNum).ToString())
                    });
            controller.execute(action);
        }
    }
}
