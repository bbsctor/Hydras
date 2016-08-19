using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ConfigFrame.UITask;
using ConfigFrame.DynamicUI;
using ConfigFrame.Util;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UIManagers.Util;

namespace HydrasUI_WPF.UIManagers
{
    public class SettingUIManager : StaticBaseUIManager
    {
        private SystemAndSettingListDataModel dListModel;
        private SystemAndSettingListViewModel vListModel;
        private UICommonService uiBasicService;

        public SettingUIManager(Control control, UICommonService service)
            : base(control)
        {
            uiBasicService = service;
        }

        public override void updateUI()
        {
            dListModel = uiBasicService.getSystemAndSettingList();
            vListModel = (SystemAndSettingListViewModel)SystemAndSettingListModelConverter
                .getInstance().genViewModel(dListModel);


            SystemAndSettingViewModel vItem;

            //updateSdiAddr
            vItem = vListModel.getViewModelItem(0x03);
            updateUI_sdiAddr(vItem);

            //updateSdiDelay
            vItem = vListModel.getViewModelItem(0x04);
            updateUI_sdiDelay(vItem);

            //updateSdiMode
            vItem = vListModel.getViewModelItem(0x09);
            updateUI_sdiMode(vItem);

            //updateModbusAddr
            vItem = vListModel.getViewModelItem(0x06);
            updateUI_modbusAddr(vItem);

            //update baudRate
            vItem = vListModel.getViewModelItem(0x07);
            updateUI_baudRate(vItem);

            //update logFiles
            vItem = vListModel.getViewModelItem(0x0B);
            updateUI_logFiles(vItem);

            //update autoLog
            vItem = vListModel.getViewModelItem(0x0A);
            updateUI_autoLog(vItem);

            //update dateFormat
            DateFormatViewModel dfvModel = uiBasicService.getDateFormatViewModel();
            updateUI_dateFormat(dfvModel);
        }

        private void updateUI_modbusAddr(SystemAndSettingViewModel vItem)
        {
            byte addr = vItem.SettingContent[0];

            mainFrame.setting_modbusAddrTextBox.Text = addr.ToString();
        }

        private void updateUI_baudRate(SystemAndSettingViewModel vItem)
        {
            string baudRate = StringByteConverter.byteArrayToString(vItem.SettingContent).Substring(0,1);
            if (baudRate.Equals("0"))
            {
                baudRate += ":1200";
            }
            else if (baudRate.Equals("1"))
            {
                baudRate += ":19200";
            }
            else if (baudRate.Equals("2"))
            {
                baudRate += ":2400";
            }
            else if (baudRate.Equals("4"))
            {
                baudRate += ":4800";
            }
            else if (baudRate.Equals("9"))
            {
                baudRate += ":9600";
            }
            mainFrame.setting_baudrateComboBox.Text = baudRate;
        }

        

        private void updateUI_sdiAddr(SystemAndSettingViewModel vItem)
        {
            mainFrame.setting_sdiAddrTextBox.Text = StringByteConverter.
                byteArrayToString(new byte[]{vItem.SettingContent[0]});
        }

        private void updateUI_sdiDelay(SystemAndSettingViewModel vItem)
        {
            mainFrame.setting_sdiDelayTextBox.Text = BitConverter.ToUInt16(vItem.SettingContent, 0).ToString();
        }

        private void updateUI_sdiMode(SystemAndSettingViewModel vItem)
        {
            if (vItem.SettingContent[0] == 0x00)
            {
                mainFrame.setting_sdiModeCheckBox.IsChecked = false;
            }
            else if (vItem.SettingContent[0] == 0x01)
            {
                mainFrame.setting_sdiModeCheckBox.IsChecked = true;
            }
        }

        private void updateUI_logFiles(SystemAndSettingViewModel vItem)
        {
            byte files = vItem.SettingContent[0];
            string temp = null;
            switch (files)
            {
                case 0x01:
                    temp = "1:File(5-sec)";
                    break;
                case 0x02:
                    temp = "2:Files(10-sec)";
                    break;
                case 0x03:
                    temp = "3:Files(20-sec)";
                    break;
                case 0x04:
                    temp = "4:Files(30-sec)";
                    break;
            }
            mainFrame.setting_logFileComboBox.Text = temp;
        }

        private void updateUI_autoLog(SystemAndSettingViewModel vItem)
        {
            if (vItem.SettingContent[0] == 0x00)
            {
                mainFrame.setting_logFileCheckBox.IsChecked = false;
            }
            else if (vItem.SettingContent[0] == 0x01)
            {
                mainFrame.setting_logFileCheckBox.IsChecked = true;
            }
        }

        private void updateUI_dateFormat(DateFormatViewModel vItem)
        {
            //ComboBoxItem cbi = new ComboBoxItem();
            //cbi.Content = vItem.DateFormat;
            mainFrame.setting_dateTimeComboBox.Text = vItem.DateFormat;
            //mainFrame.setting_dateTimeComboBox.SelectedItem = cbi;

            if (vItem.UseDelimiter == 0x00)
            {
                mainFrame.setting_dateTimeCheckBox.IsChecked = false;
            }
            else if (vItem.UseDelimiter == 0x01)
            {
                mainFrame.setting_dateTimeCheckBox.IsChecked = true;
            }
        }

        public void updateDataModel_communication()
        {
            SystemAndSettingViewModel vModel;
            SystemAndSettingDataModel dOldModel;
            SystemAndSettingDataModel dNewModel;
            vModel = new SystemAndSettingViewModel();

            //update sdiAddrModel
            vModel.ParaCode1 = 0x03;
           
            vModel.SettingContent = StringByteConverter.stringToByteArray(mainFrame.setting_sdiAddrTextBox.Text);
            dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);

            //update sdiDelayModel
            vModel.ParaCode1 = 0x04;
            ushort delay;
            ushort.TryParse(mainFrame.setting_sdiDelayTextBox.Text, out delay);
            vModel.SettingContent = BitConverter.GetBytes(delay);
            dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);

            //update sdiModel
            vModel.ParaCode1 = 0x09;

            if (mainFrame.setting_sdiModeCheckBox.IsChecked == true)
            {
                vModel.SettingContent = new byte[] { 0x01 };
            }
            else
            {
                vModel.SettingContent = new byte[] { 0x00 };
            }
            
            dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);

            //update modbusAddrModel
            vModel.ParaCode1 = 0x06;
            byte addr;
            byte.TryParse(mainFrame.setting_modbusAddrTextBox.Text, out addr);
            vModel.SettingContent = new byte[] { addr };
            dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);

            //update baudRateModel
            vModel.ParaCode1 = 0x07;
            ListBoxItem item = mainFrame.setting_baudrateComboBox.SelectedItem as ListBoxItem;
            
            vModel.SettingContent = StringByteConverter.stringToByteArray(item.Content.ToString().Substring(0, 1));
            dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);
        }

        public void updateDataModel_logFiles()
        {
            SystemAndSettingViewModel vModel;
            SystemAndSettingDataModel dOldModel;
            SystemAndSettingDataModel dNewModel;
            vModel = new SystemAndSettingViewModel();

            vModel.ParaCode1 = 0x0B;
            ListBoxItem item = mainFrame.setting_logFileComboBox.SelectedItem as ListBoxItem;

            byte temp;
            byte.TryParse(item.Content.ToString().Substring(0, 1), out temp);
            vModel.SettingContent = new byte[]{temp};
            dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);
        }

        public void updateDataModel_autoLog()
        {
            SystemAndSettingViewModel vModel;
            SystemAndSettingDataModel dOldModel;
            SystemAndSettingDataModel dNewModel;
            vModel = new SystemAndSettingViewModel();

            vModel.ParaCode1 = 0x0A;

            if (mainFrame.setting_logFileCheckBox.IsChecked == true)
            {
                vModel.SettingContent = new byte[] { 0x01 };
            }
            else
            {
                vModel.SettingContent = new byte[] { 0x00 };
            }

            dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);
        }

        public void updateDataModel_dateFormat()
        {
            DateFormatViewModel vModel;
            DateFormatDataModel dOldModel;
            DateFormatDataModel dNewModel;
            vModel = new DateFormatViewModel();
            ListBoxItem item = mainFrame.setting_dateTimeComboBox.SelectedItem as ListBoxItem;
            vModel.DateFormat = item.Content.ToString();
            if (mainFrame.setting_dateTimeCheckBox.IsChecked == false)
            {
                vModel.UseDelimiter = 0x00;
            }
            else
            {
                vModel.UseDelimiter = 0x01;
            }

            dOldModel = uiBasicService.getDateFormatDataModel();
            dNewModel = (DateFormatDataModel)DateFormatModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);
        }
    }
}
