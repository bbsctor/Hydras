﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class ParameterSetupBaseInfoParser
    {
        public static ParameterSetupBaseInfoDataModel parse(Response_49BaseFrame frame)
        {
            ParameterSetupBaseInfoDataModel model = new ParameterSetupBaseInfoDataModel();
            model.para1 = frame.paraCode1.Data[0];
            model.para2 = frame.paraCode2.Data[0];

            return model;
        }
    }
}

