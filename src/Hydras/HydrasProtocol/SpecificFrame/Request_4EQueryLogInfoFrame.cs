using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_4EQueryLogInfoFrame : CommonRequestFrame
    {           
        public Request_4EQueryLogInfoFrame(byte temp)
        {
            preBuild(FrameContext.slaveAddr, 0x4E, new byte[]{temp});
            build();
        }
    }
}
