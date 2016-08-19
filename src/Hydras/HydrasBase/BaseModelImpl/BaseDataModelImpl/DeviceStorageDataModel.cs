using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class DeviceStorageDataModel : BasicModel, IDataModel
    {
        public byte logFilesNum;
        public byte maxLogFilesNum;
        public byte[] memoryAvailable;
        public byte[] bytesLeft;
        public byte[] daysLeft;
        public byte[] internalBatteryLeft;
        public byte[] internalBatteryDaysLeft;
        public byte[] externalBatteryLeft;
        public byte[] externalBatteryDaysLeft;

        public override void update(IModel model)
        {
            update(model, new string[]{"logFilesNum", "maxLogFilesNum", "memoryAvailable",
                "bytesLeft", "daysLeft", "internalBatteryLeft", "internalBatteryDaysLeft", "externalBatteryLeft",
                "externalBatteryDaysLeft"});
        }
    }
}
