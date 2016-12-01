using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ConfigFrame.UITask;
using ConfigFrame.DynamicUI;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UIManagers.Util;

namespace HydrasUI_WPF.UIManagers
{
    public class SondeDataOptionUIManager
    {
        private OptionWindow mainFrame;
        public SondeDataOptionUIManager(Control control)
        {
            mainFrame = (OptionWindow)control;
        }

        public void updateUI()
        {

        }

        public void updateDataModel()
        {
            SondeDataOptionDataModel newModel = new SondeDataOptionDataModel();
            //List<string> userDefPortList = new List<string>(mainFrame.getPortArray());
            //string[] availablePortList = System.IO.Ports.SerialPort.GetPortNames();
            //List<string> availableUserDefPortList = new List<string>();
            //for (int i = 0; i < userDefPortList.Count; i++)
            //{
            //    if (availablePortList.Contains(userDefPortList[i]) == true)
            //    {
            //        availableUserDefPortList.Add(userDefPortList[i]);
            //    }
            //}
            newModel.portArray = mainFrame.getPortArray();
            newModel.baudRateArray = mainFrame.getBaudrateArray();
            SondeDataOptionDataModel oldModel = UICommonService.getSondeDataOptionDataModel();
            oldModel.update(newModel);



            if (mainFrame.autoBaudRateCheckBox.IsChecked == true)
            {
                HydrasUI_WPF.Properties.Settings.Default.option_isAutoBaudRate = true; 
            }
            else if (mainFrame.autoBaudRateCheckBox.IsChecked == false)
            {
                HydrasUI_WPF.Properties.Settings.Default.option_isAutoBaudRate = false;
                ComboBoxItem item = mainFrame.baudRateComboBox.SelectedItem as ComboBoxItem;
                HydrasUI_WPF.Properties.Settings.Default.option_baudrate = item.Content.ToString();
            }

            HydrasUI_WPF.Properties.Settings.Default.option_portList = mainFrame.portListTextBox.Text;
            if (mainFrame.scanAllRadioButton.IsChecked == true)
            {
                HydrasUI_WPF.Properties.Settings.Default.option_isScanAllPorts = true;
            }
            else if (mainFrame.scanAllRadioButton.IsChecked == false)
            {
                HydrasUI_WPF.Properties.Settings.Default.option_isScanAllPorts = false;
            }


            HydrasUI_WPF.Properties.Settings.Default.Save();
        }
    }
}

