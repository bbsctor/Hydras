using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using ConfigFrame.UITask;
using ConfigFrame.DynamicUI;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;
using HydrasUI_WPF.UIServiceImpl;
using HydrasUI_WPF.UIManagers.Util;

namespace HydrasUI_WPF.UIManagers
{
    public class LogFileDownloadUIManager
    {
        private LogFileDownloadWindow mainFrame;
        private UICommonService uiBasicService;
        public LogFileDownloadUIManager(Control control, UICommonService service)
        {
            mainFrame = (LogFileDownloadWindow)control;
            uiBasicService = service;
        }

        public void updateUI(LogFileBaseInfoViewModel baseModel)
        {
            SondeInfoViewModel sondeInfo = uiBasicService.getSondeInfoView();
            string content = uiBasicService.buildLogFile(sondeInfo, baseModel);
            mainFrame.textBox.Text = content;

        }
    }
}
