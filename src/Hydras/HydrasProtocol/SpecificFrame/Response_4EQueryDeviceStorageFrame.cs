using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_4EQueryDeviceStorageFrame : CommonResponseFrame
    {

        public BasicElement unknown;
        public BasicElement logFilesNum;
        public BasicElement maxLogFilesNum;
        public BasicElement memoryAvailable;
        public BasicElement bytesLeft;
        public BasicElement daysLeft;
        public BasicElement internalBatteryLeft;
        public BasicElement internalBatteryDaysLeft;
        public BasicElement externalBatteryLeft;
        public BasicElement externalBatteryDaysLeft;

        public Response_4EQueryDeviceStorageFrame()
        {
            unknown = new BasicElement();
            unknown.Name = "unknown";
            unknown.Size = 2;

            logFilesNum = new BasicElement();
            logFilesNum.Name = "logFilesNum";
            logFilesNum.Size = 1;

            maxLogFilesNum = new BasicElement();
            maxLogFilesNum.Name = "maxLogFilesNum";
            maxLogFilesNum.Size = 1;

            memoryAvailable = new BasicElement();
            memoryAvailable.Name = "memoryAvailable";
            memoryAvailable.Size = 4;

            bytesLeft = new BasicElement();
            bytesLeft.Name = "bytesLeft";
            bytesLeft.Size = 4;

            daysLeft = new BasicElement();
            daysLeft.Name = "daysLeft";
            daysLeft.Size = 4;

            internalBatteryLeft = new BasicElement();
            internalBatteryLeft.Name = "internalBatteryLeft";
            internalBatteryLeft.Size = 4;

            internalBatteryDaysLeft = new BasicElement();
            internalBatteryDaysLeft.Name = "internalBatteryDaysLeft";
            internalBatteryDaysLeft.Size = 4;

            externalBatteryLeft = new BasicElement();
            externalBatteryLeft.Name = "externalBatteryLeft";
            externalBatteryLeft.Size = 4;

            externalBatteryDaysLeft = new BasicElement();
            externalBatteryDaysLeft.Name = "externalBatteryDaysLeft";
            externalBatteryDaysLeft.Size = 4;

            base.data.RelateElements = new BasicElement[]{
                unknown,
                logFilesNum,
                maxLogFilesNum,
                memoryAvailable,
                bytesLeft,
                daysLeft,
                internalBatteryLeft,
                internalBatteryDaysLeft,
                externalBatteryLeft,
                externalBatteryDaysLeft
            };
        }
    }
}
