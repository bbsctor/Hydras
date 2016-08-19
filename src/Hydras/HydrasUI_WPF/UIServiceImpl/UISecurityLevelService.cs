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
    public class UISecurityLevelService : UIBasicService
    {
        public UISecurityLevelService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("securityLevelController");
            ((SecurityLevelController)controller).CommService.ConnectParameterModel = portModel;
        }


        public void setAllLevelPassword()
        {
            controller.execute("setAllLevelPassword");
        }
    }
}
