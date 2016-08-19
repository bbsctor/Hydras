using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_4FSelectedParaFrame : CommonRequestFrame
    {           
        public Request_4FSelectedParaFrame(byte logNum,byte sn)
        {
            preBuild(FrameContext.slaveAddr, 0x4F, new byte[]{logNum, 0x01, sn});
            build();
        }
    }
}