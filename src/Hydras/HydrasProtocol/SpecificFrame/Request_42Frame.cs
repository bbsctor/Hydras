using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_42Frame : CommonRequestFrame
    {            
        public Request_42Frame()
        {
            preBuild(FrameContext.slaveAddr, 0x42, null);
            build();
        }
    }
}
