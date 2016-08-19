using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl.FrameParser
{
    public class SecurityLevelParser
    {
        public static SecurityLevelDataModel parse(Response_43Frame frame)
        {
            SecurityLevelDataModel model = new SecurityLevelDataModel();
            model.level1Pwd = frame.level1Pwd.Data;
            model.level2Pwd = frame.level2Pwd.Data;
            model.level3Pwd = frame.level3Pwd.Data;

            return model;
        }
    }
}
