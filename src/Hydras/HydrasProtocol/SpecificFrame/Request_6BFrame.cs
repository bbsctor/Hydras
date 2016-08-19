using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_6BFrame : CommonRequestFrame
    {
        public Request_6BFrame(byte[] data)
        {
            preBuild(FrameContext.slaveAddr, 0x6B, data);
            build();
        }
    }
}