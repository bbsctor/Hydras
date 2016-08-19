using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace ProtocolFrame.ElementDef.BasicImpl
{
    public class FrameNOElement:BasicElement
    {
        private static ushort frameNO = 0;
        private ushort genFrameNO()
        {
            frameNO++;
            return frameNO;
        }

        public ushort getCurrentNO()
        {
            return frameNO;
        }

        public bool build()
        {
            ushort fn = genFrameNO();
            byte[] temp = BitConverter.GetBytes(fn);
            System.Array.Reverse(temp, 0 , 2);
            Data = temp;
            return true;
        }
    }
}
