using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolFrame.ElementDef.BasicImpl.Util
{
    public class ModBusCRCHelper
    {

        private static ModBusCRCHelper instance = null;

        public static ModBusCRCHelper getInstance()
        {
            if (instance == null)
            {
                instance = new ModBusCRCHelper();
            }
            return instance;
        }

        public ushort ComputeChecksum(byte[] crc_array)
        {
            ushort crc;
            CalculateCRC(crc_array, crc_array.Length, out crc);
            return crc;
        }
        private void CalculateCRC(byte[] pByte, int nNumberOfBytes, out ushort pChecksum)
        {
            int nBit;
            ushort nShiftedBit;
            pChecksum = 0xFFFF;

            for (int nByte = 0; nByte < nNumberOfBytes; nByte++)
            {
                pChecksum ^= pByte[nByte];
                for (nBit = 0; nBit < 8; nBit++)
                {
                    if ((pChecksum & 0x1) == 1)
                    {
                        nShiftedBit = 1;
                    }
                    else
                    {
                        nShiftedBit = 0;
                    }
                    pChecksum >>= 1;
                    if (nShiftedBit != 0)
                    {
                        pChecksum ^= 0xA001;
                    }
                }
            }
        }

        public byte[] ComputeChecksumBytes(byte[] bytes)
        {
            ushort crc = ComputeChecksum(bytes);
            return BitConverter.GetBytes(crc);
        }

        public ushort getCrc(byte[] bytes)
        {
            return ComputeChecksum(bytes);
        }
    }
}
