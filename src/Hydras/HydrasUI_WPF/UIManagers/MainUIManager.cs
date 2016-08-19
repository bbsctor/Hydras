using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConfigFrame.UITask;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl.Util;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasUI_WPF.UIServiceImpl;

namespace HydrasUI_WPF.UIManagers
{
    public class MainUIManager : BasicBlockUIManager
    {
        private MainWindow frame;

        public MainUIManager(Control control)
            : base(control)
        {
            frame = (MainWindow)control;
        }


        public override void updateUI()
        {
            frame.logListView.Items.Clear();
            updateSondeList();
            frame.main_savePathTextBox.Text = HydrasUI_WPF.Properties.Settings.Default.main_savePath;
        }

        public void updateSondeList()
        {
            SondeInfoListDataModel sondeListDataModel = UIScanService.getSensorListDataModel();
            SondeInfoListViewModel sondeListViewModel = 
                (SondeInfoListViewModel)SondeInfoListModelConverter.getInstance().genViewModel(sondeListDataModel);
            frame.sondeInfoListView.DataContext = sondeListViewModel;
            for (int i = 0; i < sondeListDataModel.Count; i++)
            {
                updateLogFileList(sondeListDataModel[i]);
            }
        }

        public void updateLogFileList(SondeInfoDataModel sondeModel)
        {
            LogFileBaseInfoListViewModel originModel = 
                (LogFileBaseInfoListViewModel)LogFileBaseInfoListModelConverter.getInstance().
                genViewModel(sondeModel.sondeDetailDataModel.logFileBaseInfoListDataModel);
                //genViewModel(sondeModel.logList);
            LogFileListViewItem tempItem;
            if (originModel != null)
            {
                for (int i = 0; i < originModel.Count; i++)
                {
                    if (originModel[i].Size_scans > 0)
                    {
                        tempItem = new LogFileListViewItem();
                        tempItem.Sonde = (SondeInfoViewModel)SondeInfoModelConverter.getInstance().genViewModel(sondeModel);
                        tempItem.LogFile = originModel[i];
                        frame.logListView.Items.Add(tempItem);
                    }
                }
            }
        }


    }
}
