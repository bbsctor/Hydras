using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ConfigFrame.UITask;
using ConfigFrame.DynamicUI;
using ConfigFrame.Util;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UIManagers.Util;
using HydrasProtocol.StringMap;

namespace HydrasUI_WPF.UIManagers
{
    public class LogFileUIManager : StaticBaseUIManager
    {
        public enum Status { NO_SELSCTED, COMPLETE_LOG, UNCOMPLETE_LOG };
        private LogFileBaseInfoListViewModel vLogFileList = null;
        private LogFileSettingFieldListDataModel dSettingFieldList = null;
        private LogFileSettingFieldListViewModel vSettingFieldList = null;
        private UICommonService uiBasicService;
        public SondeParameterMapper mapper;
        public UserClock clock;
        public LogFileBaseInfoViewModel baseModel;
        public LogFileUIManager(Control control, UICommonService service)
            : base(control)
        {
            uiBasicService = service;
        }

        public void setUIAblity(Status status)
        {
            switch (status)
            {
                case Status.NO_SELSCTED:
                    //mainFrame.logFile_createButton.IsEnabled = true;
                    mainFrame.logFile_addParameterButton.IsEnabled = false;
                    mainFrame.logFile_deleteButton.IsEnabled = false;
                    mainFrame.logFile_disableButton.IsEnabled = false;
                    mainFrame.logFile_downloadButton.IsEnabled = false;
                    mainFrame.logFile_enableButton.IsEnabled = false;
                    mainFrame.logFile_removeParameterButton.IsEnabled = false;
                    mainFrame.logFile_savingButton.IsEnabled = false;
                    mainFrame.logFile_transferDBButton.IsEnabled = false;
                    
                    break;
                case Status.COMPLETE_LOG:
                    //mainFrame.logFile_createButton.IsEnabled = true;
                    mainFrame.logFile_addParameterButton.IsEnabled = false;
                    mainFrame.logFile_deleteButton.IsEnabled = true;
                    //mainFrame.logFile_disableButton.IsEnabled = false;
                    //mainFrame.logFile_statusLabel.Content = "已禁用";
                    mainFrame.logFile_statusAlertingLabel.Content = "已完成";
                    mainFrame.logFile_downloadButton.IsEnabled = true;
                    //mainFrame.logFile_enableButton.IsEnabled = true;
                    mainFrame.logFile_removeParameterButton.IsEnabled = false;
                    mainFrame.logFile_savingButton.IsEnabled = false;
                    mainFrame.logFile_transferDBButton.IsEnabled = false;

                    break;
                case Status.UNCOMPLETE_LOG:
                    //mainFrame.logFile_createButton.IsEnabled = true;
                    mainFrame.logFile_addParameterButton.IsEnabled = true;
                    mainFrame.logFile_deleteButton.IsEnabled = true;
                    //mainFrame.logFile_disableButton.IsEnabled = false;
                    //mainFrame.logFile_statusLabel.Content = "已禁用";
                    mainFrame.logFile_statusAlertingLabel.Content = "";
                    mainFrame.logFile_downloadButton.IsEnabled = false;
                    //mainFrame.logFile_enableButton.IsEnabled = true;
                    mainFrame.logFile_removeParameterButton.IsEnabled = true;
                    mainFrame.logFile_savingButton.IsEnabled = true;
                    mainFrame.logFile_transferDBButton.IsEnabled = false;

                    break;
            }
        }

        public override void cleanUI()
        {
            mainFrame.logFile_createDTLabel.Content = "";
            mainFrame.logFile_sizeLabel.Content = "";
            mainFrame.logFile_typeLabel.Content = "";
        }

        public override void updateUI()
        {
            //updateUI_availableSize();
            updateUI_logFileList();
            updateUI_allParameter();
        }

        public void updateUI_availableSize()
        {
            DeviceStorageViewModel model = uiBasicService.getDeviceStorageViewModel();
            mainFrame.logFile_availableSizeLabel.Content = model.bytesLeft;
        }

        public void updateUI_logFileList()
        {
            mainFrame.logFile_fileComboBox.Items.Clear();
            vLogFileList = uiBasicService.getLogFileBaseInfoListViewModel();
            ComboBoxItem cbItem;
            for (int i = 0; i < vLogFileList.Count; i++)
            {
                cbItem = new ComboBoxItem();
                cbItem.Content = vLogFileList[i].LogFileName;
                mainFrame.logFile_fileComboBox.Items.Add(cbItem);
            }
            setUIAblity(Status.NO_SELSCTED);
            DeviceStorageViewModel deviceModel = uiBasicService.getDeviceStorageViewModel();
            int temp;
            int.TryParse(deviceModel.maxLogFilesNum, out temp);
            if (temp > vLogFileList.Count)
            {
                mainFrame.logFile_createButton.IsEnabled = true;
            }
            else
            {
                mainFrame.logFile_createButton.IsEnabled = false;
            }
        }

        public void updateUI_logBaseInfo(string fileName)
        {
            LogFileBaseInfoViewModel vModel = vLogFileList.getModelByFileName(fileName);

            mainFrame.logFile_createDTLabel.Content = vModel.StartTime.ToString();
            mainFrame.logFile_sizeLabel.Content = vModel.Size_bytes + "/" + vModel.Size_scans;

            if (vModel.AutoLogState == 0x04)
            {
                mainFrame.logFile_disableButton.IsEnabled = true;
                mainFrame.logFile_enableButton.IsEnabled = false;
                mainFrame.logFile_statusLabel.Content = "已启用";
            }
            else if (vModel.AutoLogState == 0x84)
            {
                mainFrame.logFile_disableButton.IsEnabled = false;
                mainFrame.logFile_enableButton.IsEnabled = true;
                mainFrame.logFile_statusLabel.Content = "已禁用";
            }

            if (vModel.Size_scans > 0)
            {
                setUIAblity(Status.COMPLETE_LOG);
            }
            else
            {
                setUIAblity(Status.UNCOMPLETE_LOG);
            }

            mainFrame.logFile_typeLabel.Content = "时序记录文件";
        }

        public void updateUI_settingField()
        {
            dSettingFieldList = uiBasicService.getLogFileSettingFieldListDataModel();
            vSettingFieldList = (LogFileSettingFieldListViewModel)LogFileSettingFieldListModelConverter
                .getInstance().genViewModel(dSettingFieldList);
            LogFileSettingFieldViewModel vItem;

            //updateStartLogging
            vItem = vSettingFieldList.getModelByFileNum(0x01);
            updateUI_startLogging(vItem);

            //updateStopLogging
            vItem = vSettingFieldList.getModelByFileNum(0x02);
            updateUI_stopLogging(vItem);

            //updateLoggingInterval
            vItem = vSettingFieldList.getModelByFileNum(0x03);
            updateUI_loggingInterval(vItem);

            //updateSensorWarmup
            vItem = vSettingFieldList.getModelByFileNum(0x04);
            updateUI_sensorWarmup(vItem);

            //updateCirculatorWarmup
            vItem = vSettingFieldList.getModelByFileNum(0x05);
            updateUI_circulatorWarmup(vItem);

            //updateAudio
            vItem = vSettingFieldList.getModelByFileNum(0x06);
            updateUI_audio(vItem);

            //DateTime temp = mainFrame.logFile_stopLoggingDate.Value.Date
            //    + (mainFrame.logFile_stopLoggingTime.Value - mainFrame.logFile_stopLoggingTime.Value.Date);
            //if (temp < clock.getDateTime())
            //{
            //    setUIAblity(Status.COMPLETE_LOG);
            //}
            //else
            //{
            //    setUIAblity(Status.UNCOMPLETE_LOG);
            //}
        }

        private void updateUI_startLogging(LogFileSettingFieldViewModel vItem)
        {
            DateTime dt = (DateTime)vItem.SettingValue;
            mainFrame.logFile_startLoggingDate.Value = dt.Date;
            mainFrame.logFile_startLoggingTime.Value = DateTime.Now.Date + dt.TimeOfDay;
        }

        private void updateUI_stopLogging(LogFileSettingFieldViewModel vItem)
        {
            DateTime dt = (DateTime)vItem.SettingValue;
            mainFrame.logFile_stopLoggingDate.Value = dt.Date;
            mainFrame.logFile_stopLoggingTime.Value = DateTime.Now.Date + dt.TimeOfDay;
        }

        private void updateUI_loggingInterval(LogFileSettingFieldViewModel vItem)
        {
            DateTime dt = (DateTime)vItem.SettingValue;
            mainFrame.logFile_interval.Value = DateTime.Now.Date + dt.TimeOfDay;
        }

        private void updateUI_sensorWarmup(LogFileSettingFieldViewModel vItem)
        {
            DateTime dt = (DateTime)vItem.SettingValue;
            mainFrame.logFile_sensorWarmup.Value = DateTime.Now.Date + dt.TimeOfDay;
        }

        private void updateUI_circulatorWarmup(LogFileSettingFieldViewModel vItem)
        {
            DateTime dt = (DateTime)vItem.SettingValue;
            mainFrame.logFile_circulatorWarmup.Value = DateTime.Now.Date + dt.TimeOfDay;
        }

        private void updateUI_audio(LogFileSettingFieldViewModel vItem)
        {
            if (((byte[])vItem.SettingValue)[0] == 0x01)
            {
                mainFrame.logFile_audio.IsChecked = true;
            }
            else if (((byte[])vItem.SettingValue)[0] == 0x00)
            {
                mainFrame.logFile_audio.IsChecked = false;
            }
        }

        private void updateUI_allParameter()
        {
            ParameterInfoListViewModel vModel = (ParameterInfoListViewModel)uiBasicService
                .getParameterInfoList();
            mainFrame.logFile_allParameterListView.ItemsSource = vModel;            
        }

        public void updateUI_selectedParameter()
        {
            LogFileParameterInfoListViewModel vModel = (LogFileParameterInfoListViewModel)uiBasicService
               .getLogFileParameterInfoListViewModel();
            for (int i = 0; i < vModel.Count; i++)
            {
                string temp = mapper.getStringByCode(vModel[i].ParaCode);
                vModel[i].ParaName = temp.Substring(0, temp.IndexOf('['));
                int index = temp.IndexOf('[');
                vModel[i].CalUnit = temp.Substring(index + 1, temp.Length - index - 2);
            }
            mainFrame.logFile_selParameterListView.ItemsSource = vModel;

        }

        public void updateUI_logFileStatus()
        {

            updateUI_availableSize();
            vLogFileList = uiBasicService.getLogFileBaseInfoListViewModel();
            LogFileBaseInfoViewModel vModel = vLogFileList.getModelByLogNum(baseModel.LogNum);
            mainFrame.logFile_sizeLabel.Content = vModel.Size_bytes + "/" + vModel.Size_scans;
            //if(baseModel.Size_scans > 0)
            //{
            //    mainFrame.logFile_statusAlertingLabel.Content = "日志记录中...";
            //}
            //else if (baseModel.Size_scans == 0)
            //{
                mainFrame.logFile_statusAlertingLabel.Content = "日志记录中...";
            //}
        }

        public override void updateDataModel()
        {
            updateDataModel_logFileParameterInfo();
            updateDataModel_settingField();
        }

        public void updateDataModel_logFileParameterInfo()
        {
            LogFileParameterInfoListViewModel lvModel = (LogFileParameterInfoListViewModel)(mainFrame.
                logFile_selParameterListView.ItemsSource);
            LogFileParameterInfoListDataModel ldModel = 
                (LogFileParameterInfoListDataModel)LogFileParameterInfoListModelConverter.
                getInstance().genDataModel(lvModel);
            uiBasicService.updateLogFileParameterInfoDataModel(ldModel);
        }

        public void updateDataModel_settingField()
        {
            updateDataModel_startLogging();
            updateDataModel_stopLogging();
            updateDataModel_loggingInterval();
            updateDataModel_sensorWarmup();
            updateDataModel_circulatorWarmup();
            updateDataModel_audio();
        }

        private void updateDataModel_startLogging()
        {
            byte[] temp = DateTimeByteConverter.getSecondsByte(mainFrame.logFile_startLoggingDate.Value.Date
                + (mainFrame.logFile_startLoggingTime.Value - mainFrame.logFile_startLoggingTime.Value.Date));

            LogFileSettingFieldViewModel vModel = new LogFileSettingFieldViewModel();
            vModel.Sn = 0x01;
            vModel.SettingValue = temp;

            LogFileSettingFieldDataModel oldModel = dSettingFieldList.getElementBySn(0x01);
            oldModel.update(LogFileSettingFieldModelConverter.getInstance().genDataModel(vModel));
        }

        private void updateDataModel_stopLogging()
        {
            byte[] temp = DateTimeByteConverter.getSecondsByte(mainFrame.logFile_stopLoggingDate.Value.Date
                + (mainFrame.logFile_stopLoggingTime.Value - mainFrame.logFile_stopLoggingTime.Value.Date));

            LogFileSettingFieldViewModel vModel = new LogFileSettingFieldViewModel();
            vModel.Sn = 0x02;
            vModel.SettingValue = temp;

            LogFileSettingFieldDataModel oldModel = dSettingFieldList.getElementBySn(0x02);
            oldModel.update(LogFileSettingFieldModelConverter.getInstance().genDataModel(vModel));
        }

        private void updateDataModel_loggingInterval()
        {
            byte[] temp = DateTimeByteConverter.getSecondsByte(mainFrame.logFile_interval.Value);

            LogFileSettingFieldViewModel vModel = new LogFileSettingFieldViewModel();
            vModel.Sn = 0x03;
            vModel.SettingValue = temp;

            LogFileSettingFieldDataModel oldModel = dSettingFieldList.getElementBySn(0x03);
            oldModel.update(LogFileSettingFieldModelConverter.getInstance().genDataModel(vModel));
        }

        private void updateDataModel_sensorWarmup()
        {
            byte[] temp = DateTimeByteConverter.getSecondsByte(mainFrame.logFile_sensorWarmup.Value);

            LogFileSettingFieldViewModel vModel = new LogFileSettingFieldViewModel();
            vModel.Sn = 0x04;
            vModel.SettingValue = temp;

            LogFileSettingFieldDataModel oldModel = dSettingFieldList.getElementBySn(0x04);
            oldModel.update(LogFileSettingFieldModelConverter.getInstance().genDataModel(vModel));
        }

        private void updateDataModel_circulatorWarmup()
        {
            byte[] temp = DateTimeByteConverter.getSecondsByte(mainFrame.logFile_circulatorWarmup.Value);

            LogFileSettingFieldViewModel vModel = new LogFileSettingFieldViewModel();
            vModel.Sn = 0x05;
            vModel.SettingValue = temp;

            LogFileSettingFieldDataModel oldModel = dSettingFieldList.getElementBySn(0x05);
            oldModel.update(LogFileSettingFieldModelConverter.getInstance().genDataModel(vModel));
        }

        private void updateDataModel_audio()
        {
            byte temp;
            if (mainFrame.logFile_audio.IsChecked == true)
            {
                temp = 0x01;
            }
            else
            {
                temp = 0x00;
            }

            LogFileSettingFieldViewModel vModel = new LogFileSettingFieldViewModel();
            vModel.Sn = 0x06;
            vModel.SettingValue = new byte[]{temp};

            LogFileSettingFieldDataModel oldModel = dSettingFieldList.getElementBySn(0x06);
            oldModel.update(LogFileSettingFieldModelConverter.getInstance().genDataModel(vModel));
        }
    }
}
