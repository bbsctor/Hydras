using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;
using HydrasProtocol.ExceptionDefine;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class SondeInfoParser
    {
        public static SondeInfoDataModel parse(Response_40Frame frame)
        {
            SondeInfoDataModel model = null;
            if (frame.Type == Response_40Frame.TYPE.NORMAL)
            {
                model = new SondeInfoDataModel();
                model.manufacturer = frame.manufacturer.Data;
                //System.Array.Reverse(frame.manufactureDate.Data);
                model.manufactureDate = frame.manufactureDate.Data;
                model.serialNum = frame.serialNum.Data;
                model.model = frame.model.Data;
                model.softwareVersion = frame.softwareVersion.Data;
                model.modbusVersion = frame.modbusVersion.Data;
            }
            else if (frame.Type == Response_40Frame.TYPE.ABNORMAL)
            {
                throw new AbnormalResponseException();
            }
            return model;
        }
    }
}
