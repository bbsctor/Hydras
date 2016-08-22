using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.UITask;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.CommunicationService;
using HydrasFacade;
using HydrasBase.AppInterfaceImpl;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasUI_WPF.Util;

namespace HydrasUI_WPF.UIServiceImpl
{
    public class UICommonService
    {
        public SondeInfoDataModel sondeInfoDataModel;

        public UICommonService(SerialPortModel portModel)
        {
            this.sondeInfoDataModel = 
                ((SondeInfoListDataModel)ModelRepository.getModelByType(typeof(SondeInfoListDataModel))).
                getElementByPortModel(portModel);
        }

        public static ICommunicationService getCommunicationService()
        {
            return SpringContext.getCommunicationService(); 
        }

        public void cleanRepository()
        {
            sondeInfoDataModel.sondeDetailDataModel = new SondeDetailDataModel();
        }

        public SondeInfoViewModel getSondeInfoView()
        {
            SondeInfoViewModel vModel = new SondeInfoViewModel();
            SondeInfoDataModel model = sondeInfoDataModel;

            return (SondeInfoViewModel)SondeInfoModelConverter.getInstance().genViewModel(model);
        }

        public ParameterSetupBaseInfoListDataModel getParameterSetupBaseInfoList()
        {
            return sondeInfoDataModel.sondeDetailDataModel.parameterSetupBaseInfoListDataModel;
        }

        public CalibrationBaseInfoListDataModel getCalibrationBaseInfoList()
        {
            return sondeInfoDataModel.sondeDetailDataModel.calibrationBaseInfoListDataModel;
        }

        public ParameterInfoListViewModel getParameterInfoList()
        {
            ParameterInfoListDataModel dModel = sondeInfoDataModel.sondeDetailDataModel.parameterInfoListDataModel;
            return (ParameterInfoListViewModel)ParameterInfoListModelConverter
                .getInstance().genViewModel(dModel);
        }

        public SystemAndSettingListDataModel getSystemAndSettingList()
        {
            return sondeInfoDataModel.sondeDetailDataModel.systemAndSettingListDataModel;
        }

        public DateFormatDataModel getDateFormatDataModel()
        {
            return sondeInfoDataModel.sondeDetailDataModel.dateFormatDataModel;
        }

        public DateFormatViewModel getDateFormatViewModel()
        {
            DateFormatDataModel dModel = sondeInfoDataModel.sondeDetailDataModel.dateFormatDataModel;
            return (DateFormatViewModel)DateFormatModelConverter.getInstance().genViewModel(dModel);
        }

        public LogFileBaseInfoListViewModel getLogFileBaseInfoListViewModel()
        {
            LogFileBaseInfoListDataModel logFileList = sondeInfoDataModel.sondeDetailDataModel.logFileBaseInfoListDataModel;

            return (LogFileBaseInfoListViewModel)LogFileBaseInfoListModelConverter.getInstance().genViewModel(logFileList);
        }

        public LogFileSettingFieldListDataModel getLogFileSettingFieldListDataModel()
        {
            return sondeInfoDataModel.sondeDetailDataModel.logFileSettingFieldListDataModel;
        }

        public LogFileParameterInfoListViewModel getLogFileParameterInfoListViewModel()
        {
            LogFileParameterInfoListDataModel logFileList = 
                sondeInfoDataModel.sondeDetailDataModel.logFileParameterInfoListDataModel;

            return (LogFileParameterInfoListViewModel)LogFileParameterInfoListModelConverter.
                getInstance().genViewModel(logFileList);
        }

        public LogFileParameterValueListViewModel getLogFileParameterValueListViewModel()
        {
            LogFileParameterValueListDataModel list = 
                sondeInfoDataModel.sondeDetailDataModel.logFileParameterValueListDataModel;
            return (LogFileParameterValueListViewModel)LogFileParameterValueListModelConverter.
                getInstance().genViewModel(list);
        }

        public OnlineParameterValueViewModel getOnlineParameterValueViewModel()
        {
            ParameterInfoListViewModel formatModel = getParameterInfoList();
            OnlineParameterValueDataModel list = sondeInfoDataModel.sondeDetailDataModel.onlineParameterValueDataModel;
            return (OnlineParameterValueViewModel)OnlineParameterValueModelConverter.
                getInstance().genViewModel(list, formatModel);
        }

        public void updateLogFileParameterInfoDataModel(LogFileParameterInfoListDataModel ldModel)
        {
            LogFileParameterInfoListDataModel logFileList = 
                sondeInfoDataModel.sondeDetailDataModel.logFileParameterInfoListDataModel;
            logFileList.update(ldModel);
        }

        public static SondeDataOptionDataModel getSondeDataOptionDataModel()
        {
            return (SondeDataOptionDataModel)ModelRepository.
                getModelByType(typeof(SondeDataOptionDataModel));
        }

        public SecurityLevelViewModel getSecurityLevelViewModel()
        {
            SecurityLevelDataModel list = sondeInfoDataModel.sondeDetailDataModel.securityLevelDataModel;
            return (SecurityLevelViewModel)SecurityLevelModelConverter.
                getInstance().genViewModel(list);
        }

        public SecurityLevelDataModel getSecurityLevelDataModel()
        {
            return sondeInfoDataModel.sondeDetailDataModel.securityLevelDataModel;
        }

        public DeviceStorageViewModel getDeviceStorageViewModel()
        {
            DeviceStorageDataModel list = sondeInfoDataModel.sondeDetailDataModel.deviceStorageDataModel;
            return (DeviceStorageViewModel)DeviceStorageModelConverter.
                getInstance().genViewModel(list);
        }

        public string buildLogFile(SondeInfoViewModel sondeInfo, LogFileBaseInfoViewModel logFileBaseInfoViewModel)
        {
            LogFileSettingFieldListViewModel logSettingFieldListViewModel;
            LogFileParameterInfoListViewModel logParameterInfoListViewModel;
            LogFileParameterValueListViewModel logParameterValueListViewModel;

            logSettingFieldListViewModel = (LogFileSettingFieldListViewModel)LogFileSettingFieldListModelConverter.
                getInstance().genViewModel(getLogFileSettingFieldListDataModel());
            logParameterInfoListViewModel =
                (LogFileParameterInfoListViewModel)getLogFileParameterInfoListViewModel();
            logParameterValueListViewModel =
                (LogFileParameterValueListViewModel)getLogFileParameterValueListViewModel();

            return LogFileBuilder.buildLogFileContent(sondeInfo, logFileBaseInfoViewModel,
                logSettingFieldListViewModel, logParameterInfoListViewModel, logParameterValueListViewModel);
        }
    }
}
