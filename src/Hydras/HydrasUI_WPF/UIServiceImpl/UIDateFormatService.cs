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
    public class UIDateFormatService : UIBasicService
    {
        public UIDateFormatService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("dateFormatController");
            ((DateFormatController)controller).CommService.ConnectParameterModel = portModel;
        }
        public void getDateFormat()
        {
            controller.execute("getDateFormat");
        }

        public void setDateFormat()
        {
            controller.execute("setDateFormat");
        }
    }
}
