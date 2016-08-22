using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.UITask;
using ConfigFrame.Util;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasBase.Util;
using HydrasFacade;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UITaskImpl;
using HydrasUI_WPF.UIManagers;
using HydrasUI_WPF.UIManagers.Util;
using HydrasUI_WPF.Util;
using HydrasProtocol.StringMap;

namespace HydrasUI_WPF
{
    /// <summary>
    /// SondeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SondeWindow : Window
    {
        public SerialPortModel port;
        public UserClock userClock;

        private UIManager globalUIManager;
        private SystemUIManager systemUIManager;
        private ParameterSetupUIManager parameterSetupUIManager;
        private CalibrationUIManager calibrationUIManager;
        private OnlineParameterUIManager onlineParameterUIManager;
        private SettingUIManager settingUIManager;
        private LogFileUIManager logFileUIManager;

        private OperateSondeTaskGroup operateSondeTask;
        private OnlineParameterValueTaskGroup onlineParameterValueTaskGroup;
        private OnlineParameterValueTaskGroup calibrationParameterValueTaskGroup;
        private LogFileStatusTaskGroup logFileStatusTaskGroup;
        private SynDateTimeTaskGroup synDateTimeTaskGroup;

        private byte securityLevel;
        private UICommonService uiBasicService;
        private UILogFileService uiLogFileService;
        private UILogFileBaseInfoService uiLogFileBaseInfoService;
        private UIDateFormatService uiDateFormatService;
        private UICalibrationInfoService uiCalibrationInfoService;
        private UISystemAndSettingService uiSystemAndSettingService;
        private UILogFileSettingFieldService uiLogFileSettingFieldService;
        private UILogFileParameterInfoService uiLogFileParameterInfoService;
        private UILogFileParameterValueService uiLogFileParameterValueService;
        private UIParaSetupInfoService uiParaSetupInfoService;
        private UISecurityLevelService uiSecurityLevelService;

        private ParameterSetupUIProxy parameterSetupUIProxy;
        private CalibrationUIProxy calibrationUIProxy;

        SerialPortModel sModel;

        public SondeWindow(SerialPortModel sModel)
        {
            InitializeComponent();
            this.sModel = sModel;
            this.Title += sModel.Port;

            SondeWindowContext.addSondeWindow(sModel, this);
            new Thread(doBackGrounpWork).Start();
            initSecurityLevel();
        }

        private void doBackGrounpWork()
        {
            this.hydrasOnlineParameterControl.MainFrame = this;


            port = sModel;
            uiBasicService = new UICommonService(sModel);
            this.hydrasOnlineParameterControl.uiBasicService = uiBasicService;

            initUIService(sModel);
            ICommunicationService communicationService = UICommonService.getCommunicationService();
            communicationService.ConnectParameterModel = port;
            communicationService.connect();
            userClock = new UserClock(updateDateTime, this);
            initUIManagers();
            initTaskGroup();
            initData();


            startSynDateTime();
        }

        private void startSynDateTime()
        {
            synDateTimeTaskGroup =
                new SynDateTimeTaskGroup(this, systemUIManager.updateUI); ;
            synDateTimeTaskGroup.port = port;
            synDateTimeTaskGroup.CurrentMode = BasicTaskGroup.Mode.MULTI_THREAD;
            synDateTimeTaskGroup.Interval = 10000;
            synDateTimeTaskGroup.synchronizedDateTime();
        }

        private void initUIService(SerialPortModel sModel)
        {
            uiLogFileService = new UILogFileService(sModel);
            uiLogFileBaseInfoService = new UILogFileBaseInfoService(sModel);
            uiDateFormatService = new UIDateFormatService(sModel);
            uiCalibrationInfoService = new UICalibrationInfoService(sModel);
            uiSystemAndSettingService = new UISystemAndSettingService(sModel);
            uiLogFileSettingFieldService = new UILogFileSettingFieldService(sModel);
            uiLogFileParameterInfoService = new UILogFileParameterInfoService(sModel);
            uiLogFileParameterValueService = new UILogFileParameterValueService(sModel);
            uiParaSetupInfoService = new UIParaSetupInfoService(sModel);
            uiSecurityLevelService = new UISecurityLevelService(sModel);
        }

        private void initSecurityLevel()
        {
            this.securityLevel = 0x02;

            setSecurityLevelButton(0x02);
        }

        private void setSecurityLevelButton(byte level)
        {
            switch (level)
            {
                case 0x00:
                    this.security_level0Button.IsEnabled = false;
                    this.security_level1Button.IsEnabled = true;
                    this.security_level2Button.IsEnabled = true;
                    this.security_level3Button.IsEnabled = true;
                    this.security_pwdButton.IsEnabled = false;
                    break;
                case 0x01:
                    this.security_level0Button.IsEnabled = true;
                    this.security_level1Button.IsEnabled = false;
                    this.security_level2Button.IsEnabled = true;
                    this.security_level3Button.IsEnabled = true;
                    this.security_pwdButton.IsEnabled = false;
                    break;
                case 0x02:
                    this.security_level0Button.IsEnabled = true;
                    this.security_level1Button.IsEnabled = true;
                    this.security_level2Button.IsEnabled = false;
                    this.security_level3Button.IsEnabled = true;
                    this.security_pwdButton.IsEnabled = false;
                    break;
                case 0x03:
                    this.security_level0Button.IsEnabled = true;
                    this.security_level1Button.IsEnabled = true;
                    this.security_level2Button.IsEnabled = true;
                    this.security_level3Button.IsEnabled = true;
                    this.security_pwdButton.IsEnabled = true;
                    break;
            }
        }

        private void initUIProxy()
        {
            parameterSetupUIProxy = new ParameterSetupUIProxy(uiBasicService);
            parameterSetupUIProxy.MainFrame = this;
            calibrationUIProxy = new CalibrationUIProxy(uiBasicService);
            calibrationUIProxy.MainFrame = this;
        }

        public void updateDateTime(DateTime dt)
        {
            system_displayDate.Value = dt.Date;
            system_displayTime.Value = DateTime.Now.Date + dt.TimeOfDay;
            this.clockStatusBarItem.Content = dt.ToString();
        }

        private void initUIManagers()
        {
            globalUIManager = new UIManager();

            systemUIManager = new SystemUIManager(this, uiBasicService);
            systemUIManager.userClock = userClock;
            parameterSetupUIManager = new ParameterSetupUIManager(this.parameterSetupTabItem, uiBasicService);
            parameterSetupUIManager.port = port;
            parameterSetupUIProxy = new ParameterSetupUIProxy(uiBasicService);
            parameterSetupUIProxy.MainFrame = this;
            parameterSetupUIManager.uiProxy = parameterSetupUIProxy;
            calibrationUIManager = new CalibrationUIManager(this.calibrationTabItem, uiBasicService);
            calibrationUIManager.port = port;
            calibrationUIProxy = new CalibrationUIProxy(uiBasicService);
            calibrationUIProxy.MainFrame = this;
            calibrationUIManager.uiProxy = calibrationUIProxy;

            onlineParameterUIManager = new OnlineParameterUIManager(this.hydrasOnlineParameterControl, uiBasicService);
            onlineParameterUIManager.clock = userClock;
            settingUIManager = new SettingUIManager(this, uiBasicService);
            logFileUIManager = new LogFileUIManager(this, uiBasicService);
            logFileUIManager.mapper = SondeParameterMapperContext.getParameterMapper(port);
            logFileUIManager.clock = userClock;

            globalUIManager.add("system", systemUIManager);
            globalUIManager.add("parameterSetup", parameterSetupUIManager);
            globalUIManager.add("calibration", calibrationUIManager);
            globalUIManager.add("onlineParameter", onlineParameterUIManager);
            globalUIManager.add("setting", settingUIManager);
            globalUIManager.add("logFile", logFileUIManager);
        }

        private void initTaskGroup()
        {
            operateSondeTask = new OperateSondeTaskGroup(this, globalUIManager.updateUI);
            operateSondeTask.CurrentMode = BasicTaskGroup.Mode.SINGLE_THREAD;
        }

        private void initData()
        {
            operateSondeTask.port = port;
            operateSondeTask.operate();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (hydrasOnlineParameterControl.monitorStatus == 
                AssistantUserControl.HydrasOnlineParameterControl.MonitorStatus.MONITORING_ON)
            {
                MessageBox.Show("在线监测正在执行中...");
                e.Cancel = true;
                return;
            }
            finishCalibrationTask();
            operateSondeTask.finish();
            synDateTimeTaskGroup.finish();
            UIOperateSondeService.closePort();
            uiBasicService.cleanRepository();
            SondeWindowContext.removeSondeWindow(port);
        }

        private void system_setIDButton_Click(object sender, RoutedEventArgs e)
        {
            systemUIManager.updateDataModel_ID();
            uiSystemAndSettingService.setID();
            systemUIManager.updateUI();
        }

        private void system_synPCTimeButton_Click(object sender, RoutedEventArgs e)
        {
            systemUIManager.updateDataModel_PCDateTime();
            uiSystemAndSettingService.setDateTime();
            systemUIManager.updateUI();
        }

        private void system_manualTimeButton_Click(object sender, RoutedEventArgs e)
        {
            systemUIManager.updateDataModel_manualDateTime();
            uiSystemAndSettingService.setDateTime();
            systemUIManager.updateUI();
        }

        private void system_circulatorStartButton_Click(object sender, RoutedEventArgs e)
        {
            startCirculator();
        }

        public void startCirculator()
        {
            systemUIManager.updateDataModel_circulator(true);
            uiSystemAndSettingService.setCirculator();
            systemUIManager.updateUI();
        }

        private void system_circulatorStopButton_Click(object sender, RoutedEventArgs e)
        {
            stopCirculator();
        }

        public void stopCirculator()
        {
            systemUIManager.updateDataModel_circulator(false);
            uiSystemAndSettingService.setCirculator();
            systemUIManager.updateUI();
        }

        private void system_audioOpenButton_Click(object sender, RoutedEventArgs e)
        {
            systemUIManager.updateDataModel_audio(true);
            uiSystemAndSettingService.setAudio();
            systemUIManager.updateUI();
        }

        private void system_audioCloseButton_Click(object sender, RoutedEventArgs e)
        {
            systemUIManager.updateDataModel_audio(false);
            uiSystemAndSettingService.setAudio();
            systemUIManager.updateUI();
        }

        private void setting_commSaveButton_Click(object sender, RoutedEventArgs e)
        {
            settingUIManager.updateDataModel_communication();
            uiSystemAndSettingService.setCommunication();
            settingUIManager.updateUI();
        }

        private void setting_sdiEnableCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            setSdiEnable(true);
        }

        private void setting_sdiEnableCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            setSdiEnable(false);
        }

        private void setSdiEnable(bool state)
        {
            setting_sdiAddrTextBox.IsEnabled = state;
            setting_sdiDelayTextBox.IsEnabled = state;
            setting_sdiModeCheckBox.IsEnabled = state;
        }

        private void setting_logFileComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (setting_logFileComboBox.IsEnabled == true)
            {
                settingUIManager.updateDataModel_logFiles();
                uiSystemAndSettingService.setLogFiles();
                settingUIManager.updateUI();
            }
        }

        private void setting_logFileCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settingUIManager.updateDataModel_autoLog();
            uiSystemAndSettingService.setAutoLog();
            settingUIManager.updateUI();
        }

        private void setting_logFileCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            settingUIManager.updateDataModel_autoLog();
            uiSystemAndSettingService.setAutoLog();
            settingUIManager.updateUI();
        }

        private void setting_dateTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settingUIManager.updateDataModel_dateFormat();
            uiDateFormatService.setDateFormat();
            settingUIManager.updateUI();
        }

        private void setting_dateTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settingUIManager.updateDataModel_dateFormat();
            uiDateFormatService.setDateFormat();
            settingUIManager.updateUI();
        }

        private void setting_dateTimeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            settingUIManager.updateDataModel_dateFormat();
            uiDateFormatService.setDateFormat();
            settingUIManager.updateUI();
        }

        private void logFile_fileComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.logFile_fileComboBox.SelectedIndex != -1)
            {
                ComboBoxItem item = this.logFile_fileComboBox.SelectedItem as ComboBoxItem;

                logFileUIManager.updateUI_logBaseInfo(item.Content.ToString());
                LogFileBaseInfoViewModel baseModel = uiBasicService.getLogFileBaseInfoListViewModel().
                    getModelByFileName(item.Content.ToString());
                uiLogFileSettingFieldService.getLogFileSettingField(baseModel.LogNum);
                logFileUIManager.updateUI_settingField();

                uiLogFileParameterInfoService.getLogFileParameterInfo(baseModel.LogNum, baseModel.ParasNum);
                logFileUIManager.updateUI_selectedParameter();
            }
            else
            {
                logFileUIManager.cleanUI();
            }
        }

        private void logFile_addParameterButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.logFile_allParameterListView.SelectedItem != null)
            {
                ParameterInfoViewModel ovm = this.logFile_allParameterListView.SelectedItem as
                    ParameterInfoViewModel;
                LogFileParameterInfoViewModel lvm = HydrasBase.BaseModelImpl.BaseViewModelImpl.Util.
                    ParameterInfoModelConverter.toLogFileParameterInfoModel(ovm);
                if (((LogFileParameterInfoListViewModel)this.logFile_selParameterListView.ItemsSource).
                    Contains(lvm) == false)
                {
                    ((LogFileParameterInfoListViewModel)this.logFile_selParameterListView.ItemsSource).Add(lvm);
                    this.logFile_selParameterListView.Items.Refresh();  
                }
            }
        }

        private void logFile_removeParameterButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.logFile_selParameterListView.SelectedItem != null)
            {
                LogFileParameterInfoViewModel lvm = this.logFile_selParameterListView.SelectedItem as
                    LogFileParameterInfoViewModel;
                ((LogFileParameterInfoListViewModel)this.logFile_selParameterListView.ItemsSource).Remove(lvm);
                this.logFile_selParameterListView.Items.Refresh();
            }
        }

        private void logFile_savingButton_Click(object sender, RoutedEventArgs e)
        {
            logFileUIManager.updateDataModel();
            ComboBoxItem item = this.logFile_fileComboBox.SelectedItem as ComboBoxItem;
            LogFileBaseInfoViewModel baseModel = uiBasicService.getLogFileBaseInfoListViewModel().
                getModelByFileName(item.Content.ToString());

            uiLogFileSettingFieldService.setLogFileSettingField(baseModel.LogNum);
            uiLogFileSettingFieldService.getLogFileSettingField(baseModel.LogNum);
            logFileUIManager.updateUI_settingField();

            uiLogFileParameterInfoService.setLogFileParameterInfo(baseModel.LogNum);

            uiLogFileBaseInfoService.getLogFileBaseInfo(baseModel.LogNum);
            baseModel = uiBasicService.getLogFileBaseInfoListViewModel().
                getModelByFileName(item.Content.ToString());
            uiLogFileParameterInfoService.getLogFileParameterInfo(baseModel.LogNum, baseModel.ParasNum);
            logFileUIManager.updateUI_selectedParameter();


        }

        private void logFile_createButton_Click(object sender, RoutedEventArgs e)
        {
            LogFileCreateWindow lfcw = new LogFileCreateWindow();
            if (lfcw.ShowDialog() == true)
            {
                string fileName = lfcw.fileNameTextBox.Text;
                if (fileNameIsAvaiable(fileName) == true)
                {
                    uiLogFileBaseInfoService.createLogFile(fileName);
                    uiLogFileService.getAllLogFileBaseInfo();
                    logFileUIManager.updateUI_logFileList();
                    for (int i = 0; i < this.logFile_fileComboBox.Items.Count; i++)
                    {
                        if (fileName.Equals(((ComboBoxItem)(this.logFile_fileComboBox.Items[i])).Content.ToString()))
                        {
                            MessageBox.Show("创建成功！");
                            this.logFile_fileComboBox.Text = fileName;
                            return;
                        }
                    }
                    MessageBox.Show("创建失败！");     
                }
            }
        }

        private bool fileNameIsAvaiable(string fileName)
        {
            return true;
        }

        private void logFile_deleteButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = this.logFile_fileComboBox.SelectedItem as ComboBoxItem;
            LogFileBaseInfoViewModel baseModel = uiBasicService.getLogFileBaseInfoListViewModel().
                getModelByFileName(item.Content.ToString());
            uiLogFileBaseInfoService.deleteLogFile(baseModel.LogNum, item.Content.ToString());
            this.logFile_fileComboBox.SelectedIndex = -1;
            uiLogFileService.getAllLogFileBaseInfo();
            logFileUIManager.updateUI_logFileList();
        }

        private void logFile_enableButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = this.logFile_fileComboBox.SelectedItem as ComboBoxItem;
            LogFileBaseInfoViewModel baseModel = uiBasicService.getLogFileBaseInfoListViewModel().
                getModelByFileName(item.Content.ToString());
            logFileUIManager.baseModel = baseModel;
            uiLogFileBaseInfoService.enableLogFile(baseModel.LogNum);
            this.logFile_statusLabel.Content = "已启用";
            logFileStatusTaskGroup = new LogFileStatusTaskGroup(this, logFileUIManager.updateUI_logFileStatus);
            logFileStatusTaskGroup.port = this.port;
            logFileStatusTaskGroup.CurrentMode = BasicTaskGroup.Mode.MULTI_THREAD;
            logFileStatusTaskGroup.onlineStatus(baseModel.LogNum);

            this.logFile_enableButton.IsEnabled = false;
            this.logFile_disableButton.IsEnabled = true;
        }

        private void logFile_disableButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = this.logFile_fileComboBox.SelectedItem as ComboBoxItem;
            LogFileBaseInfoViewModel baseModel = uiBasicService.getLogFileBaseInfoListViewModel().
                getModelByFileName(item.Content.ToString());
            uiLogFileBaseInfoService.disableLogFile(baseModel.LogNum);
            this.logFile_statusLabel.Content = "已禁用";
            if (logFileStatusTaskGroup != null)
            {
                logFileStatusTaskGroup.finish();
            }

            this.logFile_enableButton.IsEnabled = true;
            this.logFile_disableButton.IsEnabled = false;
            this.logFile_statusAlertingLabel.Content = "";
        }

        private void logFile_downloadButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = this.logFile_fileComboBox.SelectedItem as ComboBoxItem;

            LogFileBaseInfoViewModel baseModel = uiBasicService.getLogFileBaseInfoListViewModel().
                getModelByFileName(item.Content.ToString());
            //UILogFileService.updateLogFileAllInfo(baseModel.LogNum);
            uiLogFileParameterValueService.downloadLogFile(baseModel.LogNum, baseModel.Size_scans);

            LogFileDownloadWindow window = new LogFileDownloadWindow();
            LogFileDownloadUIManager uiManager = new LogFileDownloadUIManager(window, uiBasicService);
            uiManager.updateUI(baseModel);
            window.Show();
        }

        public void online_startButton_Click(int seconds)
        {
            onlineParameterValueTaskGroup = 
                new OnlineParameterValueTaskGroup(this, onlineParameterUIManager.updateUI_parameterValue);
            onlineParameterValueTaskGroup.port = port;
            onlineParameterValueTaskGroup.CurrentMode = BasicTaskGroup.Mode.MULTI_THREAD;
            onlineParameterValueTaskGroup.Interval = seconds * 1000;
            onlineParameterValueTaskGroup.getOnlineParameterValue();

        }

        public void online_stopButton_Click()
        {
            if (onlineParameterValueTaskGroup != null)
            {
                onlineParameterValueTaskGroup.finish();
            }
            onlineParameterUIManager.parameterSampleInfoModel = null;
        }

        public void parameterSetupSaveButtonClick(string header)
        {
            SondeParameterMapper mapper =
                SondeParameterMapperContext.getParameterMapper(port);
            parameterSetupUIManager.updateDataModel(header);
            uiParaSetupInfoService.setParameter(mapper.getCodeByString(header));
        }

        public void calibrationSaveButtonClick(string header)
        {
            try
            {
                calibrationParameterValueTaskGroup.stopLoop();
                //finishCalibrationTask();
                Thread.Sleep(1000);
                SondeParameterMapper mapper =
                    SondeParameterMapperContext.getParameterMapper(port);
                calibrationUIManager.updateDataModel(header);
                uiCalibrationInfoService.setParameter(mapper.getCodeByString(header));
                MessageBox.Show("校准成功！");
                
            }
            catch (Exception e)
            {
                MessageBox.Show("校准失败！");
            }
            finally
            {
                //calibrationParameterValueTaskGroup.startLoop();
                startCalibrationTask();
            }
        }

        public void calibrationResetButtonClick(string header)
        {
            try
            {
                //finishCalibrationTask();
                calibrationParameterValueTaskGroup.stopLoop();
                Thread.Sleep(1000);
                SondeParameterMapper mapper =
                    SondeParameterMapperContext.getParameterMapper(port);
                uiCalibrationInfoService.reset(mapper.getCodeByString(header));
                MessageBox.Show("重置成功！");
                
            }
            catch (Exception e)
            {
                MessageBox.Show("重置失败！");
            }
            finally
            {
                //calibrationParameterValueTaskGroup.startLoop();
                startCalibrationTask();
            }
        }

        private void security_level0Button_Click(object sender, RoutedEventArgs e)
        {
            this.securityLevel = 0x00;
            setSecurityLevelButton(0x00);
        }

        private void security_level1Button_Click(object sender, RoutedEventArgs e)
        {
            SecurityLevelViewModel vModel = uiBasicService.getSecurityLevelViewModel();
            if (vModel.level1Pwd != null && vModel.level1Pwd.Equals("") == false)
            {
                validatePassword(0x01, vModel.level1Pwd);
            }
            else
            {
                this.securityLevel = 0x01;
                setSecurityLevelButton(0x01);
            }
        }

        private void security_level2Button_Click(object sender, RoutedEventArgs e)
        {
            SecurityLevelViewModel vModel = uiBasicService.getSecurityLevelViewModel();
            if (vModel.level2Pwd != null && vModel.level2Pwd.Equals("") == false)
            {
                validatePassword(0x02, vModel.level2Pwd);
            }
            else
            {
                this.securityLevel = 0x02;
                setSecurityLevelButton(0x02);
            }
        }

        private void security_level3Button_Click(object sender, RoutedEventArgs e)
        {
            SecurityLevelViewModel vModel = uiBasicService.getSecurityLevelViewModel();
            if (vModel.level3Pwd != null && vModel.level3Pwd.Equals("") == false)
            {
                validatePassword(0x03, vModel.level3Pwd);
            }
            else
            {
                this.securityLevel = 0x03;
                setSecurityLevelButton(0x03);
            }
        }

        private void validatePassword(byte level, string original)
        {
            ValidatePasswordWindow lfcw = new ValidatePasswordWindow();
            if (lfcw.ShowDialog() == true)
            {
                string pwd = lfcw.pwdTextBox.Text;
                if (pwd.Equals(original) == true)
                {
                    this.securityLevel = level;
                    setSecurityLevelButton(level);
                }
            }
        }

        private void security_pwdButton_Click(object sender, RoutedEventArgs e)
        {
            SecurityLevelViewModel viewModel = uiBasicService.getSecurityLevelViewModel();
            SetPasswordWindow lfcw = new SetPasswordWindow(viewModel);
            if (lfcw.ShowDialog() == true)
            {
                SecurityLevelViewModel vModel = new SecurityLevelViewModel();
                vModel.level1Pwd = lfcw.pwd1TextBox.Text;
                vModel.level2Pwd = lfcw.pwd2TextBox.Text;
                vModel.level3Pwd = lfcw.pwd3TextBox.Text;

                SecurityLevelDataModel dModel = 
                    (SecurityLevelDataModel)SecurityLevelModelConverter.getInstance().genDataModel(vModel);
                SecurityLevelDataModel oldModel = uiBasicService.getSecurityLevelDataModel();
                oldModel.update(dModel);
                uiSecurityLevelService.setAllLevelPassword();
            }
        }

        private void calibrationTabItem_LostFocus(object sender, RoutedEventArgs e)
        {
            finishCalibrationTask();
        }

        private void finishCalibrationTask()
        {
            if (calibrationParameterValueTaskGroup != null)
            {
                calibrationParameterValueTaskGroup.finish();
            }
        }

        private void setting_logFileButton_Click(object sender, RoutedEventArgs e)
        {
            DeviceStorageViewModel vModel = uiBasicService.getDeviceStorageViewModel();
            StorageInfoWindow lfcw = new StorageInfoWindow(vModel);
            if (lfcw.ShowDialog() == true)
            {

            }
        }

        private void resetTabItem()
        {
            if (hydrasOnlineParameterControl.monitorStatus ==
                AssistantUserControl.HydrasOnlineParameterControl.MonitorStatus.MONITORING_ON)
            {
                sondeTabControl.SelectedIndex = 3;
                return;
            }
        }

        private void systemTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            resetTabItem();
        }

        private void parameterSetupTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            resetTabItem();
        }

        private void calibrationTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            resetTabItem();
            startCalibrationTask();
        }

        private void startCalibrationTask()
        {
            if (sondeTabControl.SelectedIndex == 2)
            {
                calibrationParameterValueTaskGroup =
                new OnlineParameterValueTaskGroup(this, calibrationUIManager.updateUI_realTimeInfo);
                calibrationParameterValueTaskGroup.port = port;
                calibrationParameterValueTaskGroup.CurrentMode = BasicTaskGroup.Mode.MULTI_THREAD;
                calibrationParameterValueTaskGroup.Interval = 1000;
                calibrationParameterValueTaskGroup.getOnlineParameterValue();
            }
        }

        private void settingTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            resetTabItem();
        }

        private void tabItem1_GotFocus(object sender, RoutedEventArgs e)
        {
            resetTabItem();
        }

        private void onlineMonitoringTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hydrasOnlineParameterControl.modeComboBox.SelectedIndex = 0;
        }
    }
}
