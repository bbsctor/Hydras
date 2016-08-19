using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class LogFileParameterValueParser
    {
        public static LogFileParameterValueDataModel parse(Response_51DownloadLogFrame frame)
        {
            LogFileParameterValueDataModel lModel = new LogFileParameterValueDataModel();
            byte[] temp1, temp2;
            for (int i = 0; i < frame.values.Length; i++)
            {
                temp1 = frame.values[i].Data;
                temp2 = new byte[4];
                System.Array.Copy(temp1, 1, temp2, 0, 4);
                lModel.values.Add(temp2);
            }

            return lModel;
        }
    }
}
