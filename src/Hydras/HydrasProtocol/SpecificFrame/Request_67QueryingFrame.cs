using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_67QueryingFrame : CommonRequestFrame
    {            
        public Request_67QueryingFrame()
        {
            preBuild(FrameContext.slaveAddr, 0x67, null);
            build();
        }
    }
}