using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;
using ProtocolFrame.ElementDef.BasicImpl.Util;

namespace ProtocolFrame.ElementDef.BasicImpl
{
    public class CRCElement:ComplexElement
    {
        public override bool build()
        {
            base.build();
            List<byte> buff = new List<byte>();
            for (int i = 0; i < RelateElements.Length; i++)
            {
                if (RelateElements[i].Data != null)
                {
                    buff.AddRange(RelateElements[i].Data);
                }
            }

            ushort crc = ModBusCRCHelper.getInstance().getCrc(buff.ToArray());
            byte[] temp = BitConverter.GetBytes(crc);
            //System.Array.Reverse(temp, 0, 2);
            Data = temp;
            
            return true;
        }

        public override bool parse()
        {
            return true;
        }
    }
}
