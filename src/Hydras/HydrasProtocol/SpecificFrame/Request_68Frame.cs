using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_68Frame : CommonRequestFrame
    {
        public Request_68Frame(byte para1)
        {
            preBuild(FrameContext.slaveAddr, 0x68, new byte[]{para1});
            build();
        }
    }
}