using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.Util
{
    public class SlaveAddrContext
    {
        public static Dictionary<SerialPortModel, byte> slaveDict;

        static SlaveAddrContext()
        {
            slaveDict = new Dictionary<SerialPortModel, byte>();
        }

        public static void addSlaveAddr(SerialPortModel port, byte addr)
        {
            slaveDict.Add(port, addr);
        }

        public static byte getSlaveAddr(SerialPortModel port)
        {
            byte addr;
            slaveDict.TryGetValue(port, out addr);
            return addr;
        }
    }
}
