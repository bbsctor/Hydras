using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.CommunicationService.SerialPortSupport
{
    public class MultiSerialPortModelGen
    {
        public static List<SerialPortModel> genPortList(string[] port, string[] baudRate)
        {
            List<SerialPortModel> portList = new List<SerialPortModel>();
            SerialPortModel temp = null;
            for (int i = 0; i < port.Length; i++)
            {
                for (int j = 0; j < baudRate.Length; j++)
                {
                    temp = new SerialPortModel();
                    temp.Port = port[i];
                    temp.BaudRate = baudRate[j];
                    portList.Add(temp);
                }
            }
            return portList;
        }
    }
}
