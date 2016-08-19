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
    public class CalibrationUIProxy
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
        public Dictionary<byte, HydrasCalibrationLabelGroup> labelGroupDict;
        private UICommonService uiBasicService;

        public CalibrationUIProxy(UICommonService service)
        {
            
            controlDict = new Dictionary<byte, List<UserControl>>();
            labelGroupDict = new Dictionary<byte, HydrasCalibrationLabelGroup>();
            tabPageDict = new Dictionary<byte, string>();
            uiBasicService = service;
        }

        public void updateUI_realTimeInfo(OnlineParameterValueViewModel vModel)
        {
            
            TabItem subItem = (TabItem)mainTabControl.SelectedItem;
            byte code = mapper.getCodeByString(subItem.Header.ToString());
            HydrasCalibrationLabelGroup labelGroup = null;
            labelGroupDict.TryGetValue(code, out labelGroup);
            if (labelGroup != null)
            {
                string temp = subItem.Header.ToString();
                int index1 = temp.IndexOf('[');
                int index2 = temp.IndexOf(']');
                string unit = temp.Substring(index1, index2 - index1 + 1);
                byte sn = mapper.getSerialNumByString(subItem.Header.ToString());
                labelGroup.currentValueLabel.Content = vModel.Values[(int)sn - 1] + unit;
                labelGroup.dateTimeLabel.Content = mainFrame.userClock.getDateTime().ToString();
                sn = mapper.getSerialNumByCode(0x02);
                labelGroup.temperatureLabel.Content = vModel.Values[(int)sn - 1] + "[F]";
            }
        }

        public void initUI(CalibrationBaseInfoListDataModel listModel)
        {
            initUI_tabControl(listModel);
            for (int i = 0; i < listModel.Count; i++)
            {
                initUI_tabPage(listModel[i]);
            }
        }

        public void initUI_tabControl(CalibrationBaseInfoListDataModel listModel)
        {
            mapper =
                SondeParameterMapperContext.getParameterMapper(mainFrame.port);
            List<string> itemHeaders = new List<string>();
            ParameterInfoListViewModel vList = uiBasicService.getParameterInfoList();
            ParameterInfoViewModel vModel;
            string temp;
            for (int i = 0; i < listModel.Count; i++)
            {
                vModel = (ParameterInfoViewModel)vList.getModelByParaCode(listModel[i].para1);
                temp = mapper.getStringByCode(listModel[i].para1);
                if (temp != null)
                {
                    itemHeaders.Add(temp);
                    tabPageDict.Add(listModel[i].para1, temp);
                }
            }
            mainTabControl = DynamicControlAssistant.createTabControlWithItem(itemHeaders.ToArray());
        }

        public void initUI_tabPage(CalibrationBaseInfoDataModel baseModel)
        {
            StackPanel sPanel = new StackPanel();
            
            CalibrationDetailInfoListDataModel dListModel = baseModel.detailList;
            if (dListModel.Count > 0)
            {
                CalibrationDetailInfoListViewModel vListModel =
               (CalibrationDetailInfoListViewModel)CalibrationDetailInfoListModelConverter.getInstance()
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

        private StackPanel createTabPageContent(CalibrationDetailInfoListViewModel vList)
        {
            StackPanel sp = new StackPanel();

            CalibrationDetailInfoViewModel vModel = null;
            List<UserControl> controlList = new List<UserControl>();
            for (int i = 0; i < vList.Count; i++)
            {
                vModel = vList[i];
                if (i == 0)
                {
                    HydrasCalibrationLabelGroup lg = new HydrasCalibrationLabelGroup();
                    sp.Children.Add(lg);
                    labelGroupDict.Add(vModel.ParaCode1, lg);
                }

                HydrasTextBoxSettingControl temp = new HydrasTextBoxSettingControl();
                temp.groupBox.Header = vModel.ParaName;
                temp.promptLabel.Content = vModel.Prompt;
                temp.textBox.Text = vModel.ParaValue.ToString();
                temp.ControlCode = vModel.ParaCode2;
                sp.Children.Add(temp);
                controlList.Add(temp);
            }

            controlDict.Add(vModel.ParaCode1, controlList);

            if (vList.Count > 0)
            {
                WrapPanel wPanel = new WrapPanel();
                Button saveButton = DynamicControlAssistant.createButton(80, 20, HydrasUI_WPF.Properties.Resources.saveButtonText);
                saveButton.Click += new RoutedEventHandler(this.saveButton_Click);
                wPanel.Children.Add(saveButton);

                Button resetButton = DynamicControlAssistant.createButton(80, 20, HydrasUI_WPF.Properties.Resources.resetButtonText);
                resetButton.Click += new RoutedEventHandler(this.resetButton_Click);
                wPanel.Children.Add(resetButton);

                sp.Children.Add(wPanel);
            }
            return sp;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.calibrationSaveButtonClick(((TabItem)mainTabControl.SelectedItem).Header.ToString());
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.calibrationResetButtonClick(((TabItem)mainTabControl.SelectedItem).Header.ToString());
        }
    }
}

