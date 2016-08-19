using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ConfigFrame.UITask;
using ConfigFrame.DynamicUI;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasBase.Util;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UIManagers.Util;
using HydrasProtocol.StringMap;
using HydrasUI_WPF.AssistantUserControl;

namespace HydrasUI_WPF.UIManagers
{
    public class CalibrationUIManager : TabPageUIManager
    {
        public CalibrationUIProxy uiProxy;
        private UICommonService uiBasicService;
        private SondeParameterMapper mapper;
        public SerialPortModel port;

        public CalibrationUIManager(Control control, UICommonService service)
            : base(control)
        {
            //uiProxy = new CalibrationUIProxy(service);
            
            uiBasicService = service;
        }

        public override void updateUI()
        {
            CalibrationBaseInfoListDataModel listModel = uiBasicService.getCalibrationBaseInfoList();
            uiProxy.initUI(listModel);

            this.tabItem.Content = uiProxy.mainTabControl;
        }

        public void updateUI_realTimeInfo()
        {
            OnlineParameterValueViewModel vModel = uiBasicService.getOnlineParameterValueViewModel();
            uiProxy.updateUI_realTimeInfo(vModel);
        }

        public void updateDataModel(string pageHeader)
        {
            mapper =
                SondeParameterMapperContext.getParameterMapper(port);
            byte paraCode = mapper.getCodeByString(pageHeader);
            CalibrationBaseInfoListDataModel listModel = uiBasicService.getCalibrationBaseInfoList();
            CalibrationBaseInfoDataModel baseModel = listModel.getElementByBaseNum(paraCode);
            CalibrationDetailInfoListDataModel dListModel = baseModel.detailList;
            CalibrationDetailInfoDataModel dModel;
            HydrasTextBoxSettingControl tempTextControl;
            List<UserControl> controlList;

            uiProxy.controlDict.TryGetValue(paraCode, out controlList);

            for (int i = 0; i < dListModel.Count; i++)
            {
                if (controlList[i] as HydrasTextBoxSettingControl != null)
                {
                    tempTextControl = (HydrasTextBoxSettingControl)controlList[i];
                    byte controlCode = tempTextControl.ControlCode;
                    dModel = dListModel.getElementByNum(controlCode);
                    float value;
                    float.TryParse(tempTextControl.textBox.Text, out value);
                    dModel.paraValue = BitConverter.GetBytes(value);
                }
            }
        }
    }
}
