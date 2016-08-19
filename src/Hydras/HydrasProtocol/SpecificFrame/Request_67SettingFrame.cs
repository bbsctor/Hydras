using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_67SettingFrame : CommonRequestFrame
    {            
        public Request_67SettingFrame(byte[] data)
        {
            preBuild(FrameContext.slaveAddr, 0x67, data);
            build();
        }
    }
}