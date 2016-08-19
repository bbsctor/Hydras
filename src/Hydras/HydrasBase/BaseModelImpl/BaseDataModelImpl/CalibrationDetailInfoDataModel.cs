using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class CalibrationDetailInfoDataModel : BasicModel, IDataModel
    {
        public byte paraCode1;
        public byte fixed1;
        public byte paraCode2;
        public byte[] paraName;
        public byte[] paraNameForShort;
        public byte[] paraUnit;
        public byte[] countFormat1;
        public byte[] paraValue;
        public byte[] countFormat2;
        public byte[] scope;
        public byte[] prompt;
    }
}
