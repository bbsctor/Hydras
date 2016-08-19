using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.UITask;
using ConfigFrame.Util;
using ConfigFrame.CommunicationService;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasBase.Util;
using HydrasFacade;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UITaskImpl;
using HydrasUI_WPF.UIManagers;
using HydrasUI_WPF.UIManagers.Util;
using HydrasUI_WPF.AssistantUserControl;
using HydrasProtocol.StringMap;

namespace HydrasUI_WPF
{
    public partial class TimeGraphForm : Form
    {
        public bool isReadyForDisplay = false;
        private byte[] paraCodeList;
        private UICommonService uiBasicService;
        public UserClock clock;
        public SerialPortModel port;

        public TimeGraphForm(byte[] list, UICommonService service, DateTimeIntervalType intervalType)
        {
            InitializeComponent();
            uiBasicService = service;
            this.paraCodeList = list;
            initBaseInfo(intervalType);           
        }

        private void initBaseInfo(DateTimeIntervalType intervalType)
        {
            this.multiDimensionDynamicChartControl1.setAxisXInfo("HH:mm:ss",
                intervalType, 5);
            ParameterInfoListViewModel listModel = uiBasicService.getParameterInfoList();
            ParameterInfoViewModel vModel;
            for (int i = 0; i < paraCodeList.Length; i++)
            {
                vModel = (ParameterInfoViewModel)listModel.getModelByParaCode(paraCodeList[i]);
                createSeries(vModel);
            }
            isReadyForDisplay = true;
        }

        private void createSeries(ParameterInfoViewModel vModel)
        {
            if (vModel.ParaCode != 0x10)
            {
                float low = BitConverter.ToSingle(vModel.ScopeLow, 0);
                float high = BitConverter.ToSingle(vModel.ScopeHigh, 0);
                this.multiDimensionDynamicChartControl1.createSeries(vModel.ParaName + "[" + vModel.CalUnit + "]", 
                    new double[]{low, high});
            }
        }

        public void updateSeries(OnlineParameterValueViewModel vModel)
        {
            MultiDimensionDynamicChartControl.RealTimeDataPoint point =
                    new MultiDimensionDynamicChartControl.RealTimeDataPoint();
            point.dateTime = clock.getDateTime();
            for (int i = 0; i < paraCodeList.Length; i++)
            {
                byte sn = SondeParameterMapperContext.getParameterMapper(port).getSerialNumByCode(paraCodeList[i]);
                float temp;
                float.TryParse(vModel.Values[sn - 1], out temp);
                point.valueList.Add(temp);
            }
            this.multiDimensionDynamicChartControl1.addPointAndRefresh(point);
        }

        private void TimeGraphForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
        }
    }
}
