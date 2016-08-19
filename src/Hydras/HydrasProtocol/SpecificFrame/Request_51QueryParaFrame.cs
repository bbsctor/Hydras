using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_51QueryParaFrame : CommonRequestFrame
    {
        public Request_51QueryParaFrame(byte[] data)
        {
            preBuild(FrameContext.slaveAddr, 0x51, data);
            build();
        }
    }
}