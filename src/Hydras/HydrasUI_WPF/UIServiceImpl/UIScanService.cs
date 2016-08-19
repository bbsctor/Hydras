using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using HydrasBase.AppInterfaceImpl.BasicController;
using ConfigFrame.UITask;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.BaseService;
using HydrasFacade;
using HydrasBase.AppInterfaceImpl;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;

namespace HydrasUI_WPF.UIServiceImpl
{
    public class UIScanService :IUIService
    {
        private static IController controller;
        static UIScanService()
        {
            controller = SpringContext.getControllerByName("scanController"); 
        }
        public static void rescan()
        {
            controller.execute("rescan");
        }

        public static void scan(List<SerialPortModel> portList)
        {
            ((ScanController)controller).portList = portList;
            controller.execute("scan");
        }

        public static SondeInfoListDataModel getSensorListDataModel()
        {
            return (SondeInfoListDataModel)ModelRepository.
                getModelByType(typeof(SondeInfoListDataModel));
        }

        public static LogFileBaseInfoListViewModel getLogFileBaseInfoListView()
        {
            LogFileBaseInfoListViewModel viewList = new LogFileBaseInfoListViewModel();
            LogFileBaseInfoListDataModel logFileList = (LogFileBaseInfoListDataModel)ModelRepository.
                getModelByType(typeof(LogFileBaseInfoListDataModel));

            return (LogFileBaseInfoListViewModel)LogFileBaseInfoListModelConverter.getInstance().genViewModel(logFileList);
        }

        public static void downloadLogFile(SerialPortModel portModel, byte searchNum)
        {
            ((ScanController)controller).currentPort = portModel;
            string action = ActionStrAssistant.buildActionStr("downloadLogFile",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", ((int)searchNum).ToString()),
                    });
            controller.execute(action);
        }

    }
}
