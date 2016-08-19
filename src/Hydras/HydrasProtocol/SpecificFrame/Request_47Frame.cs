using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_47Frame : CommonRequestFrame
    {
        public Request_47Frame(byte[] data1, byte[] data2, byte[] data3)
        {
            byte[] temp1 = base.lengthCopmlete(data1, 9);
            byte[] temp2 = base.lengthCopmlete(data2, 9);
            byte[] temp3 = base.lengthCopmlete(data3, 16);
            preBuild(FrameContext.slaveAddr, 0x47, temp1.Concat(temp2).ToArray().Concat(temp3).ToArray());
            build();
        }
    }
}
