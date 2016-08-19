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
    public class UIParaSetupInfoService : UIBasicService
    {
        public UIParaSetupInfoService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("parameterSetupInfoController");
            ((ParameterSetupInfoController)controller).CommService.ConnectParameterModel = portModel;
        }
        public void getBaseInfo()
        {
            controller.execute("getBaseInfo");
        }
        public void getDetailInfo()
        {
            controller.execute("getDetailInfo");
        }

        public void setParameter(byte baseCode)
        {
            string action = ActionStrAssistant.buildActionStr("setParameter",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)baseCode).ToString())
                    });
            controller.execute(action);
        }
    }
}

