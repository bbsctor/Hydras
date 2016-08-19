using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class DeviceStorageViewModel : BasicModel, IViewModel
    {
        public string logFilesNum;
        public string maxLogFilesNum;
        public string memoryAvailable;
        public string bytesLeft;
        public string daysLeft;
        public string internalBatteryLeft;
        public string internalBatteryDaysLeft;
        public string externalBatteryLeft;
        public string externalBatteryDaysLeft;
    }
}
