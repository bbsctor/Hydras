using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_4EQueryLogInfoFrame : CommonResponseFrame
    {
//send: 01-4E-01-01-A1-9F
        //recv: 01-4E-32-01(searchNum)-01(logNum)- 04(status) -4C-6F-67-20-46-69-6C-65-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00
//        -00-00-00-00-00-00-00-00-00-00- 46-90-D9-56 -D0-01(bytes)-00-00- 1D(scans29)-00-00-00-03(parasNum)-
//        06(settingFieldsNum)- C9-BA

        public BasicElement searchNum;
        public BasicElement logNum;
        public BasicElement autoLogState;
        public BasicElement logFileName;
        public BasicElement unknown1;
        public BasicElement startTime;
        public BasicElement size_bytes;
        public BasicElement size_scans;
        public BasicElement parasNum;
        public BasicElement settingFieldsNum;

        public Response_4EQueryLogInfoFrame()
        {
            searchNum = new BasicElement();
            searchNum.Name = "searchNum";
            searchNum.Size = 1;

            logNum = new BasicElement();
            logNum.Name = "logNum";
            logNum.Size = 1;

            autoLogState = new BasicElement();
            autoLogState.Name = "autoLogState";
            autoLogState.Size = 1;

            logFileName = new BasicElement();
            logFileName.Name = "logFileName";
            logFileName.Size = 29;

            unknown1 = new BasicElement();
            unknown1.Name = "unknown1";
            unknown1.Size = 4;

            startTime = new BasicElement();
            startTime.Name = "startTime";
            startTime.Size = 4;

            size_bytes = new BasicElement();
            size_bytes.Name = "size_bytes";
            size_bytes.Size = 4;

            size_scans = new BasicElement();
            size_scans.Name = "size_scans";
            size_scans.Size = 4;

            parasNum = new BasicElement();
            parasNum.Name = "parasNum";
            parasNum.Size = 1;

            settingFieldsNum = new BasicElement();
            settingFieldsNum.Name = "settingFieldsNum";
            settingFieldsNum.Size = 1;

            base.data.RelateElements = new BasicElement[]{
                searchNum,
                logNum,
                autoLogState,
                logFileName,
                unknown1,
                startTime,
                size_bytes,
                size_scans,
                parasNum,
                settingFieldsNum
            };
        }
    }
}

