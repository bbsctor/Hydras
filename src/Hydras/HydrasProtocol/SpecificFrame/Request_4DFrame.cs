using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_4DFrame : CommonRequestFrame
    {            
        public Request_4DFrame()
        {
            preBuild(FrameContext.slaveAddr, 0x4D, new byte[]{0x00, 0x80});
            build();
        }
    }
}
