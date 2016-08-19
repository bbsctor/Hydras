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
    public class UISystemAndSettingService : UIBasicService
    {
        public UISystemAndSettingService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("systemAndSettingController");
            ((SystemAndSettingController)controller).CommService.ConnectParameterModel = portModel;
        }
        public void getSystemAndSetting()
        {
            controller.execute("getSystemAndSetting");
        }

        public void setID()
        {
            controller.execute("setID");
        }

        public void setDateTime()
        {
            controller.execute("setDateTime");
        }

        public void setCirculator()
        {
            controller.execute("setCirculator");
        }

        public void setAudio()
        {
            controller.execute("setAudio");
        }

        public void setCommunication()
        {
            controller.execute("setCommunication");
        }


        public void setLogFiles()
        {
            controller.execute("setLogFiles");
        }

        public void setAutoLog()
        {
            controller.execute("setAutoLog"); ;
        }
    }
}