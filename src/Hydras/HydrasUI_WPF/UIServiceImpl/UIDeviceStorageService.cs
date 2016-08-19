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
    public class UIDeviceStorageService : UIBasicService
    {
        public UIDeviceStorageService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("deviceStorageController");
            ((DeviceStorageController)controller).CommService.ConnectParameterModel = portModel;
        }
        public void getInfo()
        {
            controller.execute("getDeviceStorage");
        }
    }
}
