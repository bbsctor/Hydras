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
    public class SystemUIManager : StaticBaseUIManager
    {
        private UICommonService uiBasicService;
        private SystemAndSettingListDataModel dListModel;
        private SystemAndSettingListViewModel vListModel;
        public UserClock userClock;

        public SystemUIManager(Control control, UICommonService service)
            : base(control)
        {
            uiBasicService = service;
        }

        public override void updateUI()
        {
            updateSondeInfo();
            dListModel = uiBasicService.getSystemAndSettingList();
            vListModel = (SystemAndSettingListViewModel)SystemAndSettingListModelConverter
                .getInstance().genViewModel(dListModel);


            SystemAndSettingViewModel vItem;
            //update ID
            vItem = vListModel.getViewModelItem(0x05);
            updateUI_ID(vItem);

            //updateDateTime
            vItem = vListModel.getViewModelItem(0x08);
            updateUI_dateTime(vItem);

            //updateCirculator
            vItem = vListModel.getViewModelItem(0x01);
            updateUI_circulator(vItem);

            //updateAudio
            vItem = vListModel.getViewModelItem(0x02);
            updateUI_audio(vItem);
        }

        private void updateUI_ID(SystemAndSettingViewModel vItem)
        {
            mainFrame.system_instrumentIDTextBox.Text = StringByteConverter.byteArrayToString(vItem.SettingContent);
        }

        public void updateUI_dateTime(SystemAndSettingViewModel vItem)
        {

            uint seconds;
            DateTime dateTime;
            byte[] temp = new byte[4];
            System.Array.Copy(vItem.SettingContent, temp, 4);
            seconds = BitConverter.ToUInt32(temp, 0);
            uint day = seconds / (60 * 60 * 24);
            uint hour = seconds % (60 * 60 * 24) / 3600;
            uint minute = seconds % (60 * 60) / 60;
            uint second = seconds % 60;
            dateTime = new DateTime(1970, 1, 1) + new TimeSpan((int)day, (int)hour, (int)minute, (int)second);

            userClock.stop();

            mainFrame.system_displayDate.Value = dateTime.Date;
            mainFrame.system_displayTime.Value = DateTime.Now.Date + dateTime.TimeOfDay;
            mainFrame.system_manualDate.Value = dateTime.Date;
            mainFrame.system_manualTime.Value = DateTime.Now.Date + dateTime.TimeOfDay;

            userClock.reset(dateTime);
            userClock.start();
        }

        private void updateUI_circulator(SystemAndSettingViewModel vItem)
        {
            if (vItem.SettingContent[0] == 0x00)
            {
                mainFrame.system_circulatorStartButton.IsEnabled = true;
                mainFrame.system_circulatorStopButton.IsEnabled = false;
            }
            else if (vItem.SettingContent[0] == 0x01)
            {
                mainFrame.system_circulatorStartButton.IsEnabled = false;
                mainFrame.system_circulatorStopButton.IsEnabled = true;
            }
        }

        private void updateUI_audio(SystemAndSettingViewModel vItem)
        {
            if (vItem.SettingContent[0] == 0x00)
            {
                mainFrame.system_audioOpenButton.IsEnabled = true;
                mainFrame.system_audioCloseButton.IsEnabled = false;
            }
            else if (vItem.SettingContent[0] == 0x01)
            {
                mainFrame.system_audioOpenButton.IsEnabled = false;
                mainFrame.system_audioCloseButton.IsEnabled = true;
            }
        }

        public void updateDataModel_ID()
        {
            SystemAndSettingViewModel vModel = new SystemAndSettingViewModel();
            vModel.ParaCode1 = 0x05;
            vModel.SettingContent = StringByteConverter.stringToByteArray(mainFrame.system_instrumentIDTextBox.Text);
            SystemAndSettingDataModel dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            SystemAndSettingDataModel dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);
        }

        public void updateDataModel_PCDateTime()
        {

            byte[] temp = DateTimeByteConverter.getSecondsByte(DateTime.Now);
            SystemAndSettingViewModel vModel = new SystemAndSettingViewModel();
            vModel.ParaCode1 = 0x08;
            vModel.SettingContent = temp;
            SystemAndSettingDataModel dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            SystemAndSettingDataModel dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);
        }

        public void updateDataModel_manualDateTime()
        {

            byte[] temp = DateTimeByteConverter.getSecondsByte(mainFrame.system_manualDate.Value.Date
                + (mainFrame.system_manualTime.Value - mainFrame.system_manualTime.Value.Date));
            SystemAndSettingViewModel vModel = new SystemAndSettingViewModel();
            vModel.ParaCode1 = 0x08;
            vModel.SettingContent = temp;
            SystemAndSettingDataModel dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            SystemAndSettingDataModel dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);
        }

        public void updateDataModel_circulator(bool isOn)
        {
            SystemAndSettingViewModel vModel = new SystemAndSettingViewModel();
            vModel.ParaCode1 = 0x01;
            if (isOn == true)
            {
                vModel.SettingContent = new byte[]{0x01};
            }
            else
            {
                vModel.SettingContent = new byte[]{ 0x00 };
            }
            
            SystemAndSettingDataModel dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            SystemAndSettingDataModel dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);
        }

        public void updateDataModel_audio(bool isOn)
        {
            SystemAndSettingViewModel vModel = new SystemAndSettingViewModel();
            vModel.ParaCode1 = 0x02;
            if (isOn == true)
            {
                vModel.SettingContent = new byte[] { 0x01 };
            }
            else
            {
                vModel.SettingContent = new byte[] { 0x00 };
            }

            SystemAndSettingDataModel dOldModel = dListModel.getElementByItemNum(vModel.ParaCode1);
            SystemAndSettingDataModel dNewModel = (SystemAndSettingDataModel)SystemAndSettingModelConverter
                .getInstance().genDataModel(vModel);
            dOldModel.update(dNewModel);
        }

        public void updateSondeInfo()
        {
            SondeInfoViewModel vModel = uiBasicService.getSondeInfoView();
            mainFrame.system_manufacturerTextBox.Text = vModel.Manufacturer;
            mainFrame.system_modelTextBox.Text = vModel.Model;
            mainFrame.system_serialNumTextBox.Text = vModel.SerialNum;
            mainFrame.system_softwareVersionTextBox.Text = vModel.SoftwareVersion;
            mainFrame.system_modbusVersionTextBox.Text = vModel.ModbusVersion;
            mainFrame.system_manufacturerDateTextBox.Text = vModel.ManufactureDate.ToShortDateString();
        }
    }
}

