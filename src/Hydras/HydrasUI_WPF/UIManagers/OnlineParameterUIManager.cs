using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using ConfigFrame.UITask;
using ConfigFrame.DynamicUI;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UIManagers.Util;
using HydrasUI_WPF.AssistantUserControl;
using ConfigFrame.Util;

namespace HydrasUI_WPF.UIManagers
{
    public class OnlineParameterUIManager : BasicBlockUIManager
    {
        public HydrasOnlineParameterControl parameterControl;
        public ParameterSampleInfoModel parameterSampleInfoModel;
        private UICommonService uiBasicService;
        public UserClock clock;

        public OnlineParameterUIManager(Control control, UICommonService service)
            : base(control)
        {
            parameterControl = (HydrasOnlineParameterControl)control;
            parameterControl.uiBasicService = service;
            uiBasicService = service;
        }

        public override void updateUI()
        {
            updateUI_parameterBaseInfo();
        }

        private void updateUI_parameterBaseInfo()
        {
            ParameterInfoListViewModel vModel = uiBasicService.getParameterInfoList();
            for (int i = 0; i < vModel.Count; i++)
            {
                if (vModel[i].ParaCode != 0x10)
                {
                    parameterControl.paraList.Add(new
                    HydrasOnlineParameterControl.ParameterItem(vModel[i].ParaName, vModel[i].CalUnit));
                }
            }
            parameterControl.mainDataGrid.ItemsSource = parameterControl.paraList;
        }

        public void updateUI_parameterValue()
        {
            if (parameterSampleInfoModel == null)
            {
                parameterSampleInfoModel = new ParameterSampleInfoModel();
            }
            
            OnlineParameterValueViewModel vModel = uiBasicService.getOnlineParameterValueViewModel();
            updateUI_parameterSampleInfo(vModel);
            parameterControl.updateValue(vModel);
            
            updateUI_timeGraph(vModel);
            updateUI_table(vModel);
        }

        private void updateUI_timeGraph(OnlineParameterValueViewModel vModel)
        {
            if (parameterControl.timeSeriesForm != null && parameterControl.timeSeriesForm.isReadyForDisplay == true)
            {
                parameterControl.timeSeriesForm.updateSeries(vModel);
            }
        }

        private void updateUI_table(OnlineParameterValueViewModel vModel)
        {
            if (parameterControl.tableWindow != null && 
                parameterControl.tableWindow.IsActive && parameterControl.tableWindow.isReadyForDisplay == true)
            {
                parameterControl.tableWindow.update(vModel);
            }
        }

        private void updateUI_parameterSampleInfo(OnlineParameterValueViewModel vModel)
        {
            if (parameterSampleInfoModel.firstSample == null)
            {
                parameterSampleInfoModel.firstSample = clock.getDateTime().ToString();
            }

            parameterSampleInfoModel.lastSample = clock.getDateTime().ToString();
            parameterSampleInfoModel.sampleCount++;

            parameterControl.firstSampleLabel.Content = parameterSampleInfoModel.firstSample.ToString();
            parameterControl.lastSampleLabel.Content = parameterSampleInfoModel.lastSample.ToString();
            parameterControl.sampleNumLabel.Content = parameterSampleInfoModel.sampleCount.ToString();
            parameterControl.internalBatteryLabel.Content = vModel.Values[1];
            parameterControl.externalBatteryLabel.Content = vModel.Values[2];
        }

        public class ParameterSampleInfoModel
        {
            public string firstSample;
            public string lastSample;
            public int sampleCount;
            public float internalBattery;
            public float externalBattery;
        }
    }
}
