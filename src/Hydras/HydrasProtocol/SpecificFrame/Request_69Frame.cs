using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_69Frame : CommonRequestFrame
    {            
        public Request_69Frame(byte data1, byte data2)
        {
            preBuild(FrameContext.slaveAddr, 0x69, new byte[]{data1, data2});
            build();
        }
    }
}