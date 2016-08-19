using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_49BaseFrame:CommonRequestFrame
    {
        public Request_49BaseFrame(byte para)
        {
            preBuild(FrameContext.slaveAddr, 0x49, new byte[]{para, 0x00, 0x00});
            build();
        }
    }
}