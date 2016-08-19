using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_52Frame : CommonRequestFrame
    {
        public Request_52Frame(byte data1, byte[] data2)
        {
            byte[] temp = new byte[] { data1 };
            temp = temp.Concat(base.lengthCopmlete(data2, 33)).ToArray();
            preBuild(FrameContext.slaveAddr, 0x52, temp);
            build();
        }
    }
}
