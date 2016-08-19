using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class DateFormatParser
    {
        public static DateFormatDataModel parse(Response_69Frame frame)
        {
            DateFormatDataModel model = new DateFormatDataModel();
            model.format = frame.format.Data[0];
            model.useDelimiter = frame.useDelimiter.Data[0];

            return model;
        }
    }
}
