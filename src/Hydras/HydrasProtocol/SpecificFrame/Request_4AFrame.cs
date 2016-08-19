using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_4AFrame : CommonRequestFrame
    {
        public Request_4AFrame(byte para1, byte[] para2, byte[] para3, byte para4)
        {
            byte[] temp = new byte[] { para1, 0x00 };
            temp = temp.Concat(base.lengthCopmlete(para2, 9)).ToArray();
            temp = temp.Concat(base.lengthCopmlete(para3, 9)).ToArray();
            temp = temp.Concat(base.lengthCopmlete(new byte[]{para4}, 16)).ToArray();
            preBuild(FrameContext.slaveAddr, 0x4A, temp);
            build();
        }
    }
}
