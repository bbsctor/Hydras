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
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasUI_WPF
{
    /// <summary>
    /// StorageInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StorageInfoWindow : Window
    {
        public StorageInfoWindow(DeviceStorageViewModel vModel)
        {
            InitializeComponent();
            if (vModel != null)
            {
                this.logFileLabel.Content = vModel.logFilesNum;
                this.maxFileNumLabel.Content = vModel.maxLogFilesNum;
                this.availableMemeory.Content = vModel.memoryAvailable;
                this.bytesLeftLabel.Content = vModel.bytesLeft;
                this.daysLeftLabel.Content = vModel.daysLeft;
                this.externalBatteryDaysLeftLabel.Content = vModel.externalBatteryDaysLeft;
                this.externalBatteryLeftLabel.Content = vModel.externalBatteryLeft;
                this.internalBatteryDaysLeftLabel.Content = vModel.internalBatteryDaysLeft;
                this.internalBatteryLeftLabel.Content = vModel.internalBatteryLeft;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
