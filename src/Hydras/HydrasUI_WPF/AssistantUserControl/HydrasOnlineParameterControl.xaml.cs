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
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using ConfigFrame.Util;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasUI_WPF.UIManagers;
using HydrasUI_WPF.UITaskImpl;
using HydrasUI_WPF.UIServiceImpl;
using HydrasProtocol.StringMap;
using HydrasBase.Util;

namespace HydrasUI_WPF.AssistantUserControl
{
    /// <summary>
    /// HydrasOnlineDataTableControl.xaml 的交互逻辑
    /// </summary>
    public partial class HydrasOnlineParameterControl : UserControl
    {
        private SondeWindow mainFrame;

        public SondeWindow MainFrame
        {
            get { return mainFrame; }
            set { mainFrame = value; }
        }

        public enum MonitorStatus { MONITORING_ON, MONITORING_OFF };
        public MonitorStatus monitorStatus = MonitorStatus.MONITORING_OFF;

        private const string timeSeriesString = "时间序列";
        private const string verticalProfileString = "垂直剖面";
        private const string manualString = "手动捕捉";
        private const string modeString = "监测模式";
        private const string timeIntervalString = "监测时间间隔";
        private const string depthIntervalString = "监测深度间隔";
        private const string captureString = "捕捉";
        private const string startString = "开始";
        private const string stopString = "停止";
        private const string stabilityCheckString = "使用稳定性监测";
        private const string stabilityCfgString = "配置";

        public TextBox online_monitorInterval;
        private System.Windows.Forms.Integration.WindowsFormsHost timeIntervalHost;
        public System.Windows.Forms.DateTimePicker online_timeInterval;
        public TextBox online_depthIncrementTextBox;
        public ComboBox online_depthUnitComboBox;
        public Button online_arrowButton;
        public Button online_captureButton;

        private SondeParameterMapper mapper;
        public TimeGraphForm timeSeriesForm;
        public OnlineParameterTableWindow tableWindow;

        public List<ParameterItem> paraList;
        private List<OnlineParameterValueViewModel> valueList;

        public UICommonService uiBasicService;
        public enum Status { INITIALED, TIME_SERIES, OTHER };
        public HydrasOnlineParameterControl()
        {
            InitializeComponent();
            paraList = new List<ParameterItem>();
            valueList = new List<OnlineParameterValueViewModel>();
            setUIAblity(Status.INITIALED);
        }

        public void setUIAblity(Status status)
        {
            switch (status)
            {
                case Status.INITIALED:
                    modeStartButton.IsEnabled = true;
                    modeStopButton.IsEnabled = false;
                    DBButton.IsEnabled = false;
                    depthGraphButton.IsEnabled = false;
                    downButton.IsEnabled = false;
                    excelButton.IsEnabled = false;
                    //online_arrowButton.IsEnabled = false;
                    //online_captureButton.IsEnabled = false;
                    tableButton.IsEnabled = false;
                    textButton.IsEnabled = false;
                    timeGraphButton.IsEnabled = false;

                    break;
                case Status.TIME_SERIES:
                    modeStartButton.IsEnabled = true;
                    modeStopButton.IsEnabled = false;
                    DBButton.IsEnabled = false;
                    depthGraphButton.IsEnabled = false;
                    downButton.IsEnabled = false;
                    excelButton.IsEnabled = true;
                    //online_arrowButton.IsEnabled = false;
                    //online_captureButton.IsEnabled = false;
                    tableButton.IsEnabled = true;
                    textButton.IsEnabled = true;
                    timeGraphButton.IsEnabled = true;

                    break;
                case Status.OTHER:
                    modeStartButton.IsEnabled = true;
                    modeStopButton.IsEnabled = false;
                    DBButton.IsEnabled = false;
                    depthGraphButton.IsEnabled = false;
                    downButton.IsEnabled = false;
                    excelButton.IsEnabled = false;
                    //online_arrowButton.IsEnabled = false;
                    //online_captureButton.IsEnabled = false;
                    tableButton.IsEnabled = false;
                    textButton.IsEnabled = false;
                    timeGraphButton.IsEnabled = false;

                    break;
            }
        }

        public void updateValue(OnlineParameterValueViewModel vModel)
        {
            valueList.Add(vModel);
            for (int i = 0; i < paraList.Count; i++)
            {
                paraList[i].SetValue(ParameterItem.ValueProperty, vModel.Values[i + 1]);
            }
        }

        private void timeGraphButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] list = getChackedParameter();
            if (list.Length > 0)
            {
                DateTimeIntervalType intervalType = DateTimeIntervalType.Seconds;
                if (online_timeInterval.Value.TimeOfDay.Hours > 0)
                {
                    intervalType = DateTimeIntervalType.Hours;
                }
                else if (online_timeInterval.Value.TimeOfDay.Minutes > 0)
                {
                    intervalType = DateTimeIntervalType.Minutes;
                }
                else if (online_timeInterval.Value.TimeOfDay.Seconds > 0)
                {
                    intervalType = DateTimeIntervalType.Seconds;
                }

                timeSeriesForm = new TimeGraphForm(list, uiBasicService,
                    intervalType);
                timeSeriesForm.clock = mainFrame.userClock;
                timeSeriesForm.port = mainFrame.port;
                if (timeSeriesForm.ShowDialog() == System.Windows.Forms.DialogResult.Abort)
                {
                    timeSeriesForm = null;
                }
            }
        }

        private byte[] getChackedParameter()
        {
            List<byte> result = new List<byte>();
            List<ParameterItem> paraList =
                (List<ParameterItem>)mainDataGrid.ItemsSource;
            mapper =
                SondeParameterMapperContext.getParameterMapper(mainFrame.port);
            for (int i = 0; i < paraList.Count; i++)
            {
                if ((bool)paraList[i].GetValue(ParameterItem.IsCheckedProperty) == true)
                {
                    string paraName = (this.mainDataGrid.Items[i] as ParameterItem).ParaName + "[" +
                        (this.mainDataGrid.Items[i] as ParameterItem).CalUnit + "]";
                    result.Add(mapper.getCodeByString(paraName));
                }
            }
            return result.ToArray();
        }

        private void modeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.modeComboBox.SelectedIndex > -1)
            {
                ComboBoxItem item = this.modeComboBox.SelectedItem as ComboBoxItem;
                string temp = item.Content.ToString();
                initModeParameterPanel(temp);
            }
        }

        private void initModeParameterPanel(string mode)
        {
            modeParameterPanel.Children.Clear();
            switch (mode)
            {
                case timeSeriesString:
                    Label label1 = new Label();
                    label1.Content = timeIntervalString;
                    modeParameterPanel.Children.Add(label1);
                    timeIntervalHost = new System.Windows.Forms.Integration.WindowsFormsHost();
                    online_timeInterval = new System.Windows.Forms.DateTimePicker();
                    online_timeInterval.Format = System.Windows.Forms.DateTimePickerFormat.Time;
                    online_timeInterval.ShowUpDown = true;

                    int seconds = HydrasUI_WPF.Properties.Settings.Default.online_timeInterval;
                    TimeSpan ts = getTimeSpan(seconds);
                    online_timeInterval.Value = DateTime.Now.Date + ts;

                    timeIntervalHost.Child = online_timeInterval;
                    modeParameterPanel.Children.Add(timeIntervalHost);

                    break;
                case verticalProfileString:
                    Label label2 = new Label();
                    label2.Content = depthIntervalString;
                    modeParameterPanel.Children.Add(label2);
                    online_depthIncrementTextBox = new TextBox();
                    online_depthIncrementTextBox.Width = 100;
                    modeParameterPanel.Children.Add(online_depthIncrementTextBox);
                    online_depthUnitComboBox = new ComboBox();
                    online_depthUnitComboBox.Width = 100;
                    modeParameterPanel.Children.Add(online_depthUnitComboBox);
                    online_arrowButton = new Button();
                    online_arrowButton.Width = 50;
                    modeParameterPanel.Children.Add(online_arrowButton);

                    break;
                case manualString:
                    online_captureButton = new Button();
                    online_captureButton.Content = captureString;
                    modeParameterPanel.Children.Add(online_captureButton);

                    break;
            }
        }

        private TimeSpan getTimeSpan(int seconds)
        {
            return new TimeSpan(seconds / 3600, seconds % 3600 / 60, seconds % 60);
        }

        private void modeStartButton_Click(object sender, RoutedEventArgs e)
        {
            int seconds = getIntervalSeconds(online_timeInterval.Value.TimeOfDay);
            mainFrame.online_startButton_Click(seconds);
            valueList.Clear();
            if (this.modeComboBox.SelectedIndex > -1)
            {
                ComboBoxItem item = this.modeComboBox.SelectedItem as ComboBoxItem;
                string temp = item.Content.ToString();
                if (temp.Equals(timeSeriesString))
                {
                    setUIAblity(Status.TIME_SERIES);
                }
                else
                {
                    setUIAblity(Status.OTHER);
                }
            }
            this.modeStopButton.IsEnabled = true;
            this.modeStartButton.IsEnabled = false;
            monitorStatus = MonitorStatus.MONITORING_ON;
        }

        private int getIntervalSeconds(TimeSpan ts)
        {
            return ts.Hours * 3600 + ts.Minutes * 60 + ts.Seconds;
        }

        private void modeStopButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.online_stopButton_Click();
            this.modeStartButton.IsEnabled = true;
            this.modeStopButton.IsEnabled = false;
            monitorStatus = MonitorStatus.MONITORING_OFF;
        }

        private void textButton_Click(object sender, RoutedEventArgs e)
        {
            saveFile("Text Documents (*.txt)|*.txt", "export.txt");
        }

        private void excelButton_Click(object sender, RoutedEventArgs e)
        {
            saveFile("Excel Documents (*.xls)|*.xls", "export.xls");
        }

        private void saveFile(string filter, string fileName)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = filter;
            sfd.FileName = fileName;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> list = new List<byte>();
                list.Add(0x10);
                list.AddRange(getChackedParameter());
                string stOutput = "";
                // Export titles:
                string sHeaders = "";
                for (int i = 0; i < list.Count; i++)
                {
                    string para = mapper.getStringByCode(list[i]);
                    sHeaders = sHeaders.ToString() + para + "\t";
                }
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < valueList.Count; i++)
                {
                    string stLine = "";
                    for (int j = 0; j < list.Count; j++)
                        stLine = stLine.ToString() + valueList[i].Values[mapper.getSerialNumByCode(list[j]) - 1] + "\t";
                    stOutput += stLine + "\r\n";
                }
                Encoding utf16 = Encoding.GetEncoding(-0);
                byte[] output = utf16.GetBytes(stOutput);
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
        }

        private void tableButton_Click(object sender, RoutedEventArgs e)
        {
            List<byte> list = new List<byte>();
            list.Add(0x10);
            list.AddRange(getChackedParameter());
            tableWindow = new OnlineParameterTableWindow(list.ToArray(), mainFrame.port);
            tableWindow.Show();
        }


        public class ParameterItem : DependencyObject
        {
            public static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register("Value", typeof(string), typeof(ParameterItem));

            public static string GetValue(UIElement element)
            {
                return (string)element.GetValue(ValueProperty);
            }

            public static void SetValue(UIElement element, string value)
            {
                element.SetValue(ValueProperty, value);
            }

            public static readonly DependencyProperty IsCheckedProperty =
                DependencyProperty.Register("IsChecked", typeof(bool), typeof(ParameterItem));

            public static bool GetIsChecked(UIElement element)
            {
                return (bool)element.GetValue(IsCheckedProperty);
            }

            public static void SetIsChecked(UIElement element, bool value)
            {
                element.SetValue(IsCheckedProperty, value);
            }

            private bool isChecked;

            public bool IsChecked
            {
                get { return isChecked; }
                set { isChecked = value; }
            }

            private string paraName;

            public string ParaName
            {
                get { return paraName; }
                set { paraName = value; }
            }
            private string calUnit;

            public string CalUnit
            {
                get { return calUnit; }
                set { calUnit = value; }
            }
            private string value;

            public string Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

            public ParameterItem(string paraName, string calUnit)
            {
                this.paraName = paraName;
                this.calUnit = calUnit;
            }
        }

        private void circulatorStartButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.startCirculator();
        }

        private void circulatorStopButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.stopCirculator();
        }
    }
}
