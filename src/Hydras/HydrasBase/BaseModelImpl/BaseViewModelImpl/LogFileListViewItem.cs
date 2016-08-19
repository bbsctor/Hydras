using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class LogFileListViewItem
    {
        private SondeInfoViewModel sonde;

        public SondeInfoViewModel Sonde
        {
            get { return sonde; }
            set { sonde = value; }
        }
        private LogFileBaseInfoViewModel logFile;

        public LogFileBaseInfoViewModel LogFile
        {
            get { return logFile; }
            set { logFile = value; }
        }
    }
}
