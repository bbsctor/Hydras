using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_43Frame : CommonRequestFrame
    {

        //帧头                            帧尾
        //地址码	功能码	数据域长度    CRC低	CRC高
        //01	    43	    00            AA	C8
        public Request_43Frame()
        {
            preBuild(FrameContext.slaveAddr, 0x43, null);
            build();
        }
    }
}
