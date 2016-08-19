using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_46Frame : CommonRequestFrame
    {           
        public Request_46Frame(byte data)
        {
            preBuild(FrameContext.slaveAddr, 0x46, new byte[]{data});
            build();
        }
    }
}