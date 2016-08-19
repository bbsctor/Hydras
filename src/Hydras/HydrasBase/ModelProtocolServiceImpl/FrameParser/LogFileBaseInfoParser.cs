using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class LogFileBaseInfoParser
    {
        public static LogFileBaseInfoDataModel parse(Response_4EQueryLogInfoFrame frame)
        {
            LogFileBaseInfoDataModel model = new LogFileBaseInfoDataModel();
            model.autoLogState = frame.autoLogState.Data[0];
            model.logFileName = frame.logFileName.Data;
            model.logNum = frame.logNum.Data[0];
            model.startTime = frame.startTime.Data;
            model.searchNum = frame.searchNum.Data[0];
            model.parasNum = frame.parasNum.Data[0];
            model.settingFieldsNum = frame.settingFieldsNum.Data[0];
            model.size_bytes = frame.size_bytes.Data;
            model.size_scans = frame.size_scans.Data;

            return model;
        }
    }
}
