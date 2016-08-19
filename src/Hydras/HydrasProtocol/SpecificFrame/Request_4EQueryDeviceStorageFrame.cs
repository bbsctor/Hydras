using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_4EQueryDeviceStorageFrame : CommonRequestFrame
    {

        //帧头                            帧尾
        //地址码	功能码	数据域长度    CRC低	CRC高
        //01	    4E	    1             
        public Request_4EQueryDeviceStorageFrame()
        {
            preBuild(FrameContext.slaveAddr, 0x4E, new byte[]{0x00});
            build();
        }
    }
}
