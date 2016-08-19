using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.IO;
using System.ComponentModel;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.UITask;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasFacade;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UITaskImpl;
using HydrasUI_WPF.UIManagers;
using HydrasProtocol.ExceptionDefine;
using HydrasUI_WPF.Util;

namespace HydrasUI_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScanSondeTaskGroup scanTask;
        private MainUIManager mainUIManager;
        private SondeDataOptionUIManager sondeDataOptionUIManager;
        private BackgroundWorker backgroundWorker;

        public MainWindow()
        {
            InitializeComponent();
            MainWindowContext.mainWindow = this;
            SpringContext.initContext();
            initUIManager();
            initTask();
            initDefaultData();
            InitializeBackgroundWorker();
            backgroundWorker.RunWorkerAsync();
            //new System.Threading.Thread(this.doBackGroundWork).Start();
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork +=
                new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker_RunWorkerCompleted);
        }

        private void backgroundWorker_DoWork(object sender,
            DoWorkEventArgs e)
        {
            doBackGroundWork();
        }

        private void backgroundWorker_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void doBackGroundWork()
        {
            scan();
        }

        private void initUIManager()
        {
            mainUIManager = new MainUIManager(this);
        }

        private void initTask()
        {
            scanTask = new ScanSondeTaskGroup(this, mainUIManager.updateUI);
            scanTask.CurrentMode = BasicTaskGroup.Mode.SINGLE_THREAD;
        }

        private void initDefaultData()
        {
            if ((HydrasUI_WPF.Properties.Settings.Default.option_isScanAllPorts == true ||
                "".Equals(HydrasUI_WPF.Properties.Settings.Default.option_portList) == false) &&
                (HydrasUI_WPF.Properties.Settings.Default.option_isAutoBaudRate == true ||
                "".Equals(HydrasUI_WPF.Properties.Settings.Default.option_baudrate) == false))
            {
                SondeDataOptionDataModel newModel = new SondeDataOptionDataModel();
                if (HydrasUI_WPF.Properties.Settings.Default.option_isScanAllPorts == true)
                {
                    newModel.portArray = System.IO.Ports.SerialPort.GetPortNames();
                }
                else if ("".Equals(HydrasUI_WPF.Properties.Settings.Default.option_portList) == false)
                {
                    string temp = HydrasUI_WPF.Properties.Settings.Default.option_portList;
                    string[] ports = temp.Split(';');
                    for (int i = 0; i < ports.Length; i++)
                    {
                        ports[i] = "COM" + ports[i];
                    }
                    newModel.portArray = ports;
                }

                if (HydrasUI_WPF.Properties.Settings.Default.option_isAutoBaudRate == true)
                {
                    newModel.baudRateArray = new string[] {"19200"};
                }
                else if ("".Equals(HydrasUI_WPF.Properties.Settings.Default.option_baudrate) == false)
                {
                    newModel.baudRateArray = new string[] { HydrasUI_WPF.Properties.Settings.Default.option_baudrate };
                }
                SondeDataOptionDataModel oldModel = UICommonService.getSondeDataOptionDataModel();
                oldModel.update(newModel);
            }
        }

        private void scan()
        {
            SondeDataOptionDataModel model = UICommonService.getSondeDataOptionDataModel();
            if (HydrasUI_WPF.Properties.Settings.Default.option_isScanAllPorts == false &&
                 "".Equals(HydrasUI_WPF.Properties.Settings.Default.option_portList) == true ||
                HydrasUI_WPF.Properties.Settings.Default.option_isAutoBaudRate == false &&
                 "".Equals(HydrasUI_WPF.Properties.Settings.Default.option_baudrate) == true)
            {
                showOptionWindow();
            }
            scanTask.portList = MultiSerialPortModelGen.genPortList(model.portArray, model.baudRateArray);
            try
            {
                scanTask.scan();
            }
            catch (UnauthorizedAccessException uae)
            {
                MessageBox.Show("端口被占用，请关闭可能占用端口的相关程序！");
            }
            catch (AbnormalResponseException are)
            {
                scan();
            }
            finally
            {

            }
        }

        private void main_rescanButton_Click(object sender, RoutedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void main_operateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sondeInfoListView.SelectedItem == null)
            {
                MessageBox.Show("请选择要打开的探头！");
            }
            else
            {
                SondeInfoViewModel vModel = (SondeInfoViewModel)sondeInfoListView.SelectedItem;
                new SondeWindow(vModel.PortModel).Show();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            scanTask.finish();
        }

        private void main_optionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            showOptionWindow();
        }

        private void showOptionWindow()
        {
            OptionWindow lfcw = new OptionWindow();
            sondeDataOptionUIManager = new SondeDataOptionUIManager(lfcw);
            if (lfcw.ShowDialog() == true)
            {
                sondeDataOptionUIManager.updateDataModel();
            }
        }

        private void main_downloadButton_Click(object sender, RoutedEventArgs e)
        {
            LogFileListViewItem temp;
            SerialPortModel portModel;
            LogFileBaseInfoViewModel logFileBaseInfoModel;
            if (this.logListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("没有选定的日志！");
                return;
            }

            for (int i = 0; i < this.logListView.SelectedItems.Count; i++)
            {
                temp = (LogFileListViewItem)this.logListView.SelectedItems[i];
                portModel = temp.Sonde.PortModel;
                logFileBaseInfoModel = temp.LogFile;

                try
                {
                    UIScanService.downloadLogFile(portModel, logFileBaseInfoModel.SearchNum);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("下载过程出错，请重新下载！");
                }
                string fileName = HydrasUI_WPF.Util.LogFileBuilder.buildLogFileName(temp.Sonde, logFileBaseInfoModel);

                UICommonService uiBasicService = new UICommonService(portModel);
                string content = uiBasicService.buildLogFile(temp.Sonde, logFileBaseInfoModel);
                createTextFile(fileName, content);
                
                HydrasUI_WPF.Properties.Settings.Default.main_savePath = this.main_savePathTextBox.Text;
                HydrasUI_WPF.Properties.Settings.Default.Save();
            }
        }

        public void createTextFile(string fileName, string text)
        {
            string savePath = this.main_savePathTextBox.Text;
            FileStream fs = new FileStream(savePath + "\\" + fileName + ".txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            
            sw.Write(text);
            sw.Close();
            fs.Close();
        }

        private void main_editPathButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               this.main_savePathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
