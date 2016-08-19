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
using HydrasProtocol.StringMap;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasBase.Util;

namespace HydrasUI_WPF
{
    /// <summary>
    /// OnlineParameterTableWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OnlineParameterTableWindow : Window
    {
        private SondeParameterMapper mapper;
        public bool isReadyForDisplay = false;
        private List<OnlineParameterValueViewModel> dataList;
        public SerialPortModel port;

        public OnlineParameterTableWindow(byte[] paraList, SerialPortModel port)
        {
            InitializeComponent();
            this.port = port;
            mapper =
                SondeParameterMapperContext.getParameterMapper(port);
            createColumn(paraList);
            isReadyForDisplay = true;
            dataList = new List<OnlineParameterValueViewModel>();
        }

        private void createColumn(byte[] list)
        {
            DataGridTextColumn tempColumn;
            for (int i = 0; i < list.Length; i++)
            {
                tempColumn = new DataGridTextColumn();
                string para = mapper.getStringByCode(list[i]);
                tempColumn.Header = para;
                int index = mapper.getSerialNumByCode(list[i]) - 1;
                tempColumn.Binding = new Binding("Values[" + index + "]");
                this.dataGrid1.Columns.Add(tempColumn);
            }
        }

        public void update(OnlineParameterValueViewModel vModel)
        {
            dataList.Add(vModel);
            this.dataGrid1.ItemsSource = dataList;
            this.dataGrid1.Items.Refresh();
        }
    }
}
