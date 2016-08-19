using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_50SettingFieldFrame : CommonRequestFrame
    {            
        public Request_50SettingFieldFrame(byte data0, byte[] data1, byte[] data2, byte[] data3)
        {
            List<byte> temp = new List<byte>();
            temp.Add(0x03);
            temp.Add(data0);

            byte[] temp1 = base.lengthCopmlete(data1, 9);
            byte[] temp2 = base.lengthCopmlete(data2, 9);
            byte[] temp3 = base.lengthCopmlete(data3, 16);
            temp.AddRange(temp1);
            temp.AddRange(temp2);
            temp.AddRange(temp3);
            preBuild(FrameContext.slaveAddr, 0x50, temp.ToArray());
            build();
        }
    }
}

