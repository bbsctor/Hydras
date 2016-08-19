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
using HydrasBase.Util;
using HydrasProtocol.StringMap;

namespace HydrasUI_WPF.UIServiceImpl
{
    public class UICalibrationInfoService : UIBasicService
    {
        private SondeParameterMapper mapper;
        public UICalibrationInfoService(SerialPortModel portModel)
        {
            controller = SpringContext.getControllerByName("calibrationInfoController");
            ((CalibrationInfoController)controller).CommService.ConnectParameterModel = portModel;
            mapper = SondeParameterMapperContext.getParameterMapper(portModel);
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

        public void reset(byte baseCode)
        {
            byte sn = mapper.getSerialNumByCode(baseCode);
            string action = ActionStrAssistant.buildActionStr("reset",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)sn).ToString())
                    });
            controller.execute(action);
        }
    }
}