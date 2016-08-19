using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class LogFileBaseInfoViewModel : BasicModel, IViewModel
    {

        private byte searchNum;

        public byte SearchNum
        {
            get { return searchNum; }
            set { searchNum = value; }
        }

        private byte logNum;

        public byte LogNum
        {
            get { return logNum; }
            set { logNum = value; }
        }

        private byte autoLogState;

        public byte AutoLogState
        {
            get { return autoLogState; }
            set { autoLogState = value; }
        }
        private string logFileName;

        public string LogFileName
        {
            get { return logFileName; }
            set { logFileName = value; }
        }
        private DateTime startTime;

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private byte parasNum;

        public byte ParasNum
        {
            get { return parasNum; }
            set { parasNum = value; }
        }
        private byte settingFieldsNum;

        public byte SettingFieldsNum
        {
            get { return settingFieldsNum; }
            set { settingFieldsNum = value; }
        }

        private uint size_bytes;

        public uint Size_bytes
        {
            get { return size_bytes; }
            set { size_bytes = value; }
        }
        private uint size_scans;

        public uint Size_scans
        {
            get { return size_scans; }
            set { size_scans = value; }
        }
    }
}