using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class SDIParameterParser
    {
        public static SDIParameterDataModel parse(Response_67QueryingFrame frame)
        {
            SDIParameterDataModel model = new SDIParameterDataModel();
            model.sdiParameterOrder = frame.data.Data;

            return model;
        }
    }
}