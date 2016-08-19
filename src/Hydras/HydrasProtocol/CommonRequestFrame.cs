using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol
{
    public class CommonRequestFrame:CommonFrame
    {
        public CommonRequestFrame()
        {

        }

        public CommonRequestFrame(byte addr, byte func, byte[] sendData)
        {
            data.Data = null;
            data.Size = 0;
            data.RelateElements = null;
            preBuild(addr, func, sendData);
            build();
        }
        public void preBuild(byte address, byte func, byte[] sendData)
        {

            addr.Data = new byte[] { address };
            funcNo.Data = new byte[] { func };
            //funcNo.Size = 1;

            if (sendData != null)
            {
                data.Data = sendData;
                data.Size = data.Data.Length;
            }
            else
            {
                data.Data = null;
                data.Size = 0;
            }

            dataLength.build();
            crc.build();

            this.RelateElements = new BasicElement[]{
                addr, funcNo, dataLength, data, crc
            };
        }

        public byte[] lengthCopmlete(byte[] data, int wantedLength)
        {
            byte[] temp = new byte[wantedLength];
            System.Array.Copy(data, temp, data.Length);
            return temp;
        }
    }
}
