using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_4BDetailFrame:CommonRequestFrame
    {
        public Request_4BDetailFrame(byte para1, byte para2)
        {
            preBuild(FrameContext.slaveAddr, 0x4B, new byte[]{para1, 0x00, para2});
            build();
        }
    }
}