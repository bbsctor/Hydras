using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ConfigFrame.UITask;
using ConfigFrame.DynamicUI;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasBase.Util;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UIManagers.Util;
using HydrasProtocol.StringMap;
using HydrasUI_WPF.AssistantUserControl;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasUI_WPF.UIManagers
{
    public class ParameterSetupUIManager : TabPageUIManager
    {

        private UICommonService uiBasicService;
        public ParameterSetupUIProxy uiProxy;
        private SondeParameterMapper mapper;
        public SerialPortModel port;
        public ParameterSetupUIManager(Control control, UICommonService service)
            : base(control)
        {
            uiBasicService = service;
        }

        public override void updateUI()
        {
            ParameterSetupBaseInfoListDataModel listModel = uiBasicService.getParameterSetupBaseInfoList();
            uiProxy.initUI(listModel);

            this.tabItem.Content = uiProxy.mainTabControl;
        }

        public void updateDataModel(string pageHeader)
        {
            mapper =
                SondeParameterMapperContext.getParameterMapper(port);
            byte paraCode = mapper.getCodeByString(pageHeader);
            ParameterSetupBaseInfoListDataModel listModel = uiBasicService.getParameterSetupBaseInfoList();
            ParameterSetupBaseInfoDataModel baseModel = listModel.getElementByBaseNum(paraCode);
            ParameterSetupDetailInfoListDataModel dListModel = baseModel.detailList;
            ParameterSetupDetailInfoDataModel dModel;
            HydrasComboBoxSettingControl tempComboControl;
            HydrasTextBoxSettingControl tempTextControl;
            List<UserControl> controlList;

            uiProxy.controlDict.TryGetValue(paraCode, out controlList);

            for (int i = 0; i < dListModel.Count; i++)
            {
                 if (controlList[i] as HydrasComboBoxSettingControl != null)
                 {
                     tempComboControl = (HydrasComboBoxSettingControl)controlList[i];
                     byte controlCode = tempComboControl.ControlCode;
                     dModel = dListModel.getElementByNum(controlCode);
                     byte value;
                     ComboBoxItem item = tempComboControl.comboBox.SelectedItem as ComboBoxItem;
                     byte.TryParse(item.Content.ToString().Substring(0, 1), out value);
                     dModel.settingValue = value;
                 }
                 else if (controlList[i] as HydrasTextBoxSettingControl != null)
                 {
                     tempTextControl = (HydrasTextBoxSettingControl)controlList[i];
                     byte controlCode = tempTextControl.ControlCode;
                     dModel = dListModel.getElementByNum(controlCode);
                     byte value;
                     byte.TryParse(tempTextControl.textBox.Text, out value);
                     dModel.settingValue = value;
                 }
            }


        }
    }
}
