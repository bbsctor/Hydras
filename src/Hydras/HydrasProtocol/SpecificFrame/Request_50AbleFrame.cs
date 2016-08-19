using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_50AbleFrame : CommonRequestFrame
    {            
        public Request_50AbleFrame(byte able, byte logNum)
        {
            preBuild(FrameContext.slaveAddr, 0x50, new byte[]{able, logNum});
            build();
        }
    }
}
