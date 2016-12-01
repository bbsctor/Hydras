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
using System.Windows.Shapes;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasUI_WPF.UIServiceImpl;

namespace HydrasUI_WPF
{
    /// <summary>
    /// OptionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OptionWindow : Window
    {
        private string[] portArray;
        private string[] baudRateArray;
        private string[] autoBaudRateArray = new String[] { "9600", "19200"};


        public OptionWindow()
        {
            InitializeComponent();
            initOption();
            this.baudRateComboBox.SelectedIndex = 3;
        }

        private void initOption()
        {
            //SondeDataOptionDataModel model = UIBasicService.getSondeDataOptionDataModel();
            //if (model.baudRateArray != null && model.portArray != null)
            //{
            //    buildPortList(model.portArray);
            //    initBaudRate(model.baudRateArray);
            //}
            this.scanAllRadioButton.IsChecked = HydrasUI_WPF.Properties.Settings.Default.option_isScanAllPorts;
            if (this.scanAllRadioButton.IsChecked == false)
            {
                this.scanListPortsRadioButton.IsChecked = true;
                this.portListTextBox.Text = HydrasUI_WPF.Properties.Settings.Default.option_portList;
            }
            this.baudRateComboBox.Text = HydrasUI_WPF.Properties.Settings.Default.option_baudrate;
            this.autoBaudRateCheckBox.IsChecked = HydrasUI_WPF.Properties.Settings.Default.option_isAutoBaudRate;
        }

        private void initBaudRate(string[] baudRates)
        {
            if (baudRates.Length == 1)
            {
                this.baudRateComboBox.Text = baudRates[0];
            }
        }

        private void OK_button_Click(object sender, RoutedEventArgs e)
        {
            if (scanAllRadioButton.IsChecked == true)
            {
                portArray = System.IO.Ports.SerialPort.GetPortNames();
            }
            else if (scanListPortsRadioButton.IsChecked == true)
            {
                portArray = parsePortList();
            }

            if (autoBaudRateCheckBox.IsChecked == true)
            {
                baudRateArray = autoBaudRateArray;
            }
            else if (autoBaudRateCheckBox.IsChecked == false)
            {
                ComboBoxItem item = baudRateComboBox.SelectedItem as ComboBoxItem;
                baudRateArray = new string[]{item.Content.ToString()};
            }

            this.DialogResult = true;
        }

        private void Cancel_button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public string[] getPortArray()
        {
            //return new string[] { "COM5" };
            return portArray;
        }

        public string[] getBaudrateArray()
        {
            //return new string[] { "19200"};
            return baudRateArray;
        }

        private void scanAllRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            portArray = System.IO.Ports.SerialPort.GetPortNames();
        }

        private void scanListPortsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            portArray = parsePortList();
        }

        private string[] parsePortList()
        {
            string temp = portListTextBox.Text;
            string[] ports = temp.Split(';');
            for (int i = 0; i < ports.Length; i++)
            {
                ports[i] = "COM" + ports[i];
            }
            return ports;
        }

        private void buildPortList(string[] ports)
        {
            string temp = null;
            for (int i = 0; i < ports.Length; i++)
            {
                ports[i] = ports[i].Substring(3, 1);
                if (i < ports.Length - 1)
                {
                    temp += ports[i] + ";";
                }
                else
                {
                    temp += ports[i];
                }
            }
            this.portListTextBox.Text = temp;
        }

        private void baudRateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = baudRateComboBox.SelectedItem as ComboBoxItem;
            baudRateArray = new string[] { item.Content.ToString() };
        }

        private void portListTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string[] temp = this.portListTextBox.Text.Split(';');
            for (int i = 0; i < temp.Length; i++)
            {
                int port;
                if (int.TryParse(temp[i], out port) == false)
                {
                    MessageBox.Show("格式错误");
                    break;
                }

            }
        }

        private void autoBaudRateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.baudRateComboBox.IsEnabled = false;
        }

        private void autoBaudRateCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.baudRateComboBox.IsEnabled = true;
        }

        private void scanListPortsRadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            this.portListTextBox.IsEnabled = true;
        }

        private void scanAllRadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            this.portListTextBox.IsEnabled = false;
        }
    }
}
