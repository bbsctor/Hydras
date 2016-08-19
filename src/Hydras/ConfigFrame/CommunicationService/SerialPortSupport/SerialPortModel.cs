using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace ConfigFrame.CommunicationService.SerialPortSupport
{
    public class SerialPortModel : BasicModel, IDataModel
    {
        private string baudRate;

        public string BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }
        private string port;

        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        public int dataBits;
        public string parity;
        public int stopBit;
        public bool isConnected;

        public override bool Equals(Object obj)
        {
            // Performs an equality check on two points (integer pairs).
            if (obj == null || GetType() != obj.GetType()) return false;
            SerialPortModel p = (SerialPortModel)obj;
            return (baudRate.Equals(p.baudRate)) && (port.Equals(p.port));
        }
    }
}
