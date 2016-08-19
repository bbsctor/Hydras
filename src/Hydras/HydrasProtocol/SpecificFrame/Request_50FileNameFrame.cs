using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_50FileNameFrame : CommonRequestFrame
    {            
        public Request_50FileNameFrame(byte[] fileName)
        {
            byte[] temp = new byte[]{0x00, 0x04};
            temp = temp.Concat(base.lengthCopmlete(fileName, 33)).ToArray();
            preBuild(FrameContext.slaveAddr, 0x50, temp);
            build();
        }
    }
}
