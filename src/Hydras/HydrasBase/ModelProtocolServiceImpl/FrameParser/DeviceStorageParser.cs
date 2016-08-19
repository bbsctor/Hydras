using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class DeviceStorageParser
    {
        public static DeviceStorageDataModel parse(Response_4EQueryDeviceStorageFrame frame)
        {
            DeviceStorageDataModel model = new DeviceStorageDataModel();
            model.bytesLeft = frame.bytesLeft.Data;
            model.daysLeft = frame.daysLeft.Data;
            model.externalBatteryDaysLeft = frame.externalBatteryDaysLeft.Data;
            model.externalBatteryLeft = frame.externalBatteryLeft.Data;
            model.internalBatteryDaysLeft = frame.internalBatteryDaysLeft.Data;
            model.internalBatteryLeft = frame.internalBatteryLeft.Data;
            model.logFilesNum = frame.logFilesNum.Data[0];
            model.maxLogFilesNum = frame.maxLogFilesNum.Data[0];
            model.memoryAvailable = frame.memoryAvailable.Data;

            return model;
        }
    }
}
