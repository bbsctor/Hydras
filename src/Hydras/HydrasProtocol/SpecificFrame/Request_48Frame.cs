using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_48Frame : CommonRequestFrame
    {           
        public Request_48Frame(byte data)
        {
            preBuild(FrameContext.slaveAddr, 0x48, new byte[]{data});
            build();
        }
    }
}