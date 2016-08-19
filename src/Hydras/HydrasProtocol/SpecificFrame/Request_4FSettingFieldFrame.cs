using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_4FSettingFieldFrame : CommonRequestFrame
    {           
        public Request_4FSettingFieldFrame(byte logNum, byte sn)
        {
            preBuild(FrameContext.slaveAddr, 0x4F, new byte[]{logNum, 0x00, sn});
            build();
        }
    }
}
