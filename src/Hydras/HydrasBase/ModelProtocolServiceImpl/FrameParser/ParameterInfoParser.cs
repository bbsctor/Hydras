using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class ParameterInfoParser
    {
        public static ParameterInfoDataModel parse(Response_48Frame frame)
        {
            ParameterInfoDataModel model = new ParameterInfoDataModel();
            model.sn = frame.sn.Data[0];

            if (frame.paraNum.Data != null)
            {
                model.paraNum = frame.paraNum.Data[0];
            }

            model.calFormat1 = frame.calFormat1.Data;
            model.calFormat2 = frame.calFormat2.Data;
            model.calUnit = frame.calUnit.Data;
            if (frame.constByte1.Data != null)
            {
                model.constByte1 = frame.constByte1.Data[0];
            }

            model.formatAndScope = frame.formatAndScope.Data;
            if (frame.sn.Data != null)
            {
                model.sn = frame.sn.Data[0];
            }
            if (frame.paraCode.Data != null)
            {
                model.paraCode = frame.paraCode.Data[0];
            }
            model.paraName = frame.paraName.Data;
            model.paraNameForShort = frame.paraNameForShort.Data;
            if (frame.paraNum.Data != null)
            {
                model.paraNum = frame.paraNum.Data[0];
            }

            return model;
        }
    }
}

