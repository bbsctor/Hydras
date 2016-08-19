using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasBase.AppInterfaceImpl.BasicController;

namespace HydrasBase.AppInterfaceImpl
{
    public class OperateSondeController : BasicStaticController
    {
        private HandsShakeController handsShakeController;

        public HandsShakeController HandsShakeController
        {
            get { return handsShakeController; }
            set { handsShakeController = value;
            subController.Add(value);
            }
        }

        private SecurityLevelController securityLevelController;

        public SecurityLevelController SecurityLevelController
        {
            get { return securityLevelController; }
            set { securityLevelController = value;
            subController.Add(value);
            }
        }

        private SystemAndSettingController systemAndSettingController;

        public SystemAndSettingController SystemAndSettingController
        {
            get { return systemAndSettingController; }
            set { systemAndSettingController = value;
            subController.Add(value);
            }
        }

        private ParameterInfoController parameterInfoController;

        public ParameterInfoController ParameterInfoController
        {
            get { return parameterInfoController; }
            set { parameterInfoController = value;
            subController.Add(value);
            }
        }

        private ParameterSetupInfoController parameterSetupInfoController;

        public ParameterSetupInfoController ParameterSetupBaseInfoController
        {
            get { return parameterSetupInfoController; }
            set { parameterSetupInfoController = value;
            subController.Add(value);
            }
        }

        private CalibrationInfoController calibrationInfoController;

        public CalibrationInfoController CalibrationBaseInfoController
        {
            get { return calibrationInfoController; }
            set { calibrationInfoController = value;
            subController.Add(value);
            }
        }

        private SDIParameterController sdiParameterController;

        public SDIParameterController SdiParameterController
        {
            get { return sdiParameterController; }
            set { sdiParameterController = value;
            subController.Add(value);
            }
        }

        private LogFileController logFileController;

        public LogFileController LogFileController
        {
            get { return logFileController; }
            set { logFileController = value;
            subController.Add(value);
            }
        }

        private StopInteractController stopInteractController;

        public StopInteractController StopInteractController
        {
            get { return stopInteractController; }
            set { stopInteractController = value;
            subController.Add(value);
            }
        }

        private SerialPortModel port;

        public SerialPortModel Port
        {
            get { return port; }
            set { 
                port = value;
                cService.ConnectParameterModel = port;
            }
        }

        public override void execute(string action)
        {
            cService.connect();
            try
            {
                if ("operateSonde".Equals(action))
                {
                    this.CommService.ConnectParameterModel = port;
                    handsShakeController.execute("handsShake");
                    securityLevelController.execute("setPassword");
                    securityLevelController.execute("getSecurityLevel");
                    systemAndSettingController.execute("getSystemAndSetting");

                    parameterInfoController.execute("getParameterInfo");

                    parameterSetupInfoController.CommService.ConnectParameterModel = port;
                    parameterSetupInfoController.execute("getBaseInfo");
                    parameterSetupInfoController.execute("getDetailInfo");

                    calibrationInfoController.CommService.ConnectParameterModel = port;
                    calibrationInfoController.execute("getBaseInfo");
                    calibrationInfoController.execute("getDetailInfo");


                    sdiParameterController.execute("getOrder");
                    logFileController.execute("readAllLogFileBaseInfo");
                }
                if ("close".Equals(action))
                {
                    stopInteractController.execute("stopInteract");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                cService.disconnect();
            }
            
        }
    }
}
