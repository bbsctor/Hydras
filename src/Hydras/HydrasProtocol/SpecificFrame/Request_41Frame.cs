using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_41Frame : CommonRequestFrame
    {

        //帧头                          帧的数据域      帧尾
        //地址码	功能码	数据域长度  9 Byte      CRC低	CRC高
        //01	    41	    09          8759-3      AA	C8
        public Request_41Frame()
        {
            preBuild(FrameContext.slaveAddr, 0x41, new byte[]{0x38, 0x37, 0x35, 0x39, 0x2D, 0x33, 0x00, 0x00, 0x00});
            build();
        }
    }
}
