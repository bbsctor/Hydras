using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;
using ProtocolFrame.ElementDef.BasicImpl.Util;

namespace ProtocolFrame.ElementDef.BasicImpl
{
    public class DataLengthElement:ComplexElement
    {
        public override bool build()
        {
            ushort length = 0;
            for (int i = 0; i < RelateElements.Length; i++)
            {
                length += (ushort)RelateElements[i].Size;
            }

            if (Size == 2)
            {
                byte[] temp = BitConverter.GetBytes(length);
                System.Array.Reverse(temp, 0, 2);
                Data = temp;
            }
            else if (Size == 1)
            {
                Data = new byte[] { (byte)length };
            }

            return true;
        }

        public override bool parse()
        {
            return true;
        }
    }
}
