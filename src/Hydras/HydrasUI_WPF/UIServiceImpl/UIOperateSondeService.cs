using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.UITask;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasFacade;
using HydrasBase.AppInterfaceImpl;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasBase.AppInterfaceImpl.BasicController;

namespace HydrasUI_WPF.UIServiceImpl
{
    public class UIOperateSondeService :IUIService
    {
        private static IController controller;
        static UIOperateSondeService()
        {
            controller = SpringContext.getControllerByName("operateSondeController");
        }
        public static void operateSonde(SerialPortModel port)
        {
            ((OperateSondeController)controller).Port = port;
            controller.execute("operateSonde");
        }

        public static void closePort()
        {
            controller.execute("close");
            ((OperateSondeController)controller).CommService.disconnect();
        }
    }
}
