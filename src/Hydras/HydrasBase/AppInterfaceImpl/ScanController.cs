using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.BaseService;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasBase.AppInterfaceImpl.BasicController;
using HydrasProtocol.ExceptionDefine;

namespace HydrasBase.AppInterfaceImpl
{
    public class ScanController:HydrasBasicController
    {
        public List<SerialPortModel> portList;
        public SerialPortModel currentPort;

        private ScanSondeController scanSondeController;
        

        public ScanSondeController ScanSondeController
        {
            get { return scanSondeController; }
            set { scanSondeController = value;
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

        private DeviceStorageController deviceStorageController;

        public DeviceStorageController DeviceStorageController
        {
            get { return deviceStorageController; }
            set { deviceStorageController = value;
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

        private LogFileParameterValueController logFileParameterValueController;

        public LogFileParameterValueController LogFileParameterValueController
        {
            get { return logFileParameterValueController; }
            set { logFileParameterValueController = value;
            subController.Add(value);
            }
        }

        public override void execute(string action)
        {
            if ("scan".Equals(action))
            {
                if (scanSondeController.model != null)
                {
                    ((SondeInfoListDataModel)(scanSondeController.model)).Clear();
                }

                string[] availablePortList = System.IO.Ports.SerialPort.GetPortNames();
                for (int i = 0; i < portList.Count; i++)
                {
                    if (availablePortList.Contains(portList[i].Port))
                    {
                        try
                        {
                            CommService.ConnectParameterModel = portList[i];
                            CommService.connect();
                            scanSondeController.CurrentPort = portList[i];
                            scanSondeController.CommService.ConnectParameterModel = portList[i];
                            scanSondeController.execute("scanSonde");
                            securityLevelController.execute("setPassword");
                            securityLevelController.execute("getSecurityLevel");
                            logFileController.CommService.ConnectParameterModel = portList[i];
                            logFileController.execute("readAllLogFileBaseInfo");

                            ((SondeInfoListDataModel)scanSondeController.model).getElementByPortModel(portList[i])
                                .updateLoglist(((LogFileBaseInfoListDataModel)logFileController.LogFileBaseInfoController.model));

                            stopInteractController.execute("stopInteract");
                        }
                        catch (AbnormalResponseException are)
                        {
                            Console.WriteLine(are.ToString());
                        }
                        catch (TimeoutException te)
                        {
                            Console.WriteLine(te.ToString());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            throw;
                        }
                        finally
                        {
                            cService.disconnect();
                        }
                    }
                    
                }
            }
            else if (action.Contains("downloadLogFile") == true)
            {
                cService.ConnectParameterModel = currentPort;
                cService.connect();

                scanSondeController.execute("scanSonde");
                securityLevelController.execute("setPassword");

                byte para1;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                readLogFileAllInfo(para1);
                stopInteractController.execute("stopInteract");
                cService.disconnect();
            }
        }

        private void readLogFileAllInfo(byte searchNum)
        {
            string action = ActionStrAssistant.buildActionStr("readLogFileAllInfo",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)searchNum).ToString()),
                    });
            logFileController.execute(action);
        }
    }
}
