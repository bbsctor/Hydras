using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_50ParameterFrame : CommonRequestFrame
    {            
        public Request_50ParameterFrame(byte para1, byte para2, byte para3)
        {
            preBuild(FrameContext.slaveAddr, 0x50, new byte[]{para1, para2, para3, 0x00});
            build();
        }
    }
}
