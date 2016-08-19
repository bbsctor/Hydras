using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_44Frame : CommonRequestFrame
    {
        public Request_44Frame(byte data1, byte[] data2)
        {
            byte[] temp1 = new byte[] { data1 };
            byte[] temp2 = base.lengthCopmlete(data2, 9);
            preBuild(FrameContext.slaveAddr, 0x44, temp1.Concat(temp2).ToArray());
            build();
        }
    }
}

