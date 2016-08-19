using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_4BAllFrame:CommonRequestFrame
    {
        public Request_4BAllFrame(byte para)
        {
            preBuild(FrameContext.slaveAddr, 0x4B, new byte[]{para, 0x00, 0x00});
            build();
        }
    }
}