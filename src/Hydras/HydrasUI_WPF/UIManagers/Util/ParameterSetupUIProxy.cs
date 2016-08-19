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
using ConfigFrame.DynamicUI;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasBase.Util;
using HydrasProtocol.StringMap;
using HydrasUI_WPF.UITaskImpl;
using HydrasUI_WPF.AssistantUserControl;
using HydrasUI_WPF.UIServiceImpl;

namespace HydrasUI_WPF.UIManagers.Util
{
    public class ParameterSetupUIProxy
    {
        private SondeWindow mainFrame;
        public SondeWindow MainFrame
        {
            get { return mainFrame; }
            set { mainFrame = value; }
        }
        private Dictionary<byte, string> tabPageDict;

        public TabControl mainTabControl;
        private SondeParameterMapper mapper;
        public Dictionary<byte, List<UserControl>> controlDict;
        private UICommonService uiBasicService;

        public ParameterSetupUIProxy(UICommonService service)
        {
            
            controlDict = new Dictionary<byte, List<UserControl>>();
            tabPageDict = new Dictionary<byte, string>();
            uiBasicService = service;
        }

        public void initUI(ParameterSetupBaseInfoListDataModel listModel)
        {
            initUI_tabControl(listModel);
            for (int i = 0; i < listModel.Count; i++)
            {
                if (listModel[i].para1 != 0x10)
                {
                    initUI_tabPage(listModel[i]);
                }
            }
        }

        public void initUI_tabControl(ParameterSetupBaseInfoListDataModel listModel)
        {
            mapper =
                SondeParameterMapperContext.getParameterMapper(mainFrame.port);
            List<string> itemHeaders = new List<string>();
            ParameterInfoListViewModel vList = uiBasicService.getParameterInfoList();
            ParameterInfoViewModel vModel;
            string temp;
            for (int i = 0; i < listModel.Count; i++)
            {
                if (listModel[i].para1 != 0x10)
                {
                    vModel = (ParameterInfoViewModel)vList.getModelByParaCode(listModel[i].para1);
                    temp = mapper.getStringByCode(listModel[i].para1);
                    if (temp != null)
                    {
                        itemHeaders.Add(temp);
                        tabPageDict.Add(listModel[i].para1, temp);
                    }
                }
            }
            mainTabControl = DynamicControlAssistant.createTabControlWithItem(itemHeaders.ToArray());
        }

        public void initUI_tabPage(ParameterSetupBaseInfoDataModel baseModel)
        {
            ParameterSetupDetailInfoListDataModel dListModel = baseModel.detailList;
            if (dListModel.Count > 0)
            {
                ParameterSetupDetailInfoListViewModel vListModel =
               (ParameterSetupDetailInfoListViewModel)ParaSetupDetailInfoListModelConverter.getInstance()
               .genViewModel(dListModel);

                Panel panel = createTabPageContent(vListModel);
                //string itemHeader = mapper.getStringByCode(baseModel.para1);
                string itemHeader;
                tabPageDict.TryGetValue(baseModel.para1, out itemHeader);
                if (itemHeader != null)
                {
                    TabItem item = (TabItem)TabItemFinder.findTabItemByHeader(mainTabControl, itemHeader);
                    item.Content = panel;
                }
            }
        }

        private StackPanel createTabPageContent(ParameterSetupDetailInfoListViewModel vList)
        {
            StackPanel sp = new StackPanel();
            ParameterSetupDetailInfoViewModel vModel = null;
            List<UserControl> controlList = new List<UserControl>();
            for (int i = 0; i < vList.Count; i++)
            {
                vModel = vList[i];
                if (vModel.Type == ParameterSetupDetailInfoViewModel.TYPE.TEXT)
                {
                    HydrasTextBoxSettingControl temp = new HydrasTextBoxSettingControl();
                    temp.groupBox.Header = vModel.SettingName;
                    temp.promptLabel.Content = vModel.Prompt;
                    temp.textBox.Text = vModel.SettingValue.ToString();
                    temp.ControlCode = vModel.ParaCode2;
                    sp.Children.Add(temp);
                    controlList.Add(temp);
                }
                else if (vModel.Type == ParameterSetupDetailInfoViewModel.TYPE.COMBO)
                {
                    HydrasComboBoxSettingControl temp = new HydrasComboBoxSettingControl();
                    for (int j =0; j < vModel.SelItems.Length; j++)
                    {
                        ComboBoxItem cbItem = new ComboBoxItem();
                        cbItem.Content = vModel.SelItems[j];
                        temp.comboBox.Items.Add(cbItem);
                        int itemNum;
                        int.TryParse(vModel.SelItems[j].Substring(0,1), out itemNum);
                        if (itemNum == (int)vModel.SettingValue)
                        {
                            temp.comboBox.SelectedItem = cbItem;
                        }
                    }
                    
                    temp.label.Content = vModel.Text;
                    temp.ControlCode = vModel.ParaCode2;
                    sp.Children.Add(temp);
                    controlList.Add(temp);
                }
            }

            controlDict.Add(vModel.ParaCode1, controlList);

            if (vList.Count > 0)
            {
                Button saveButton = DynamicControlAssistant.createButton(80, 20, HydrasUI_WPF.Properties.Resources.saveButtonText);
                saveButton.Click += new RoutedEventHandler(this.saveButton_Click);
                sp.Children.Add(saveButton);
            }
            return sp;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.parameterSetupSaveButtonClick(((TabItem)mainTabControl.SelectedItem).Header.ToString());
        }
    }
}
