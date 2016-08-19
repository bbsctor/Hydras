using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_49DetailFrame:CommonRequestFrame
    {
        public Request_49DetailFrame(byte para1, byte para2)
        {
            preBuild(FrameContext.slaveAddr, 0x49, new byte[]{para1, 0x00, para2});
            build();
        }
    }
}
