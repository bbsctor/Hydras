using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.CommunicationService.SerialPortSupport
{
    public class SerialPortPool
    {
        private static Dictionary<SerialPortModel, SerialPortAdapter> portDict;
        static SerialPortPool()
        {
            portDict = new Dictionary<SerialPortModel, SerialPortAdapter>();
        }

        public static SerialPortAdapter getPortAdapter(SerialPortModel model)
        {
            SerialPortAdapter adapter = null;
            portDict.TryGetValue(model, out adapter);
            if (adapter == null)
            {
                adapter = new SerialPortAdapter();
                adapter.SetPortBaudRate(System.Convert.ToInt32(model.BaudRate));
                adapter.SetPortName(model.Port);
                adapter.SetTimeOut(1500);
                portDict.Add(model, adapter);
            }
            return adapter;
        }
    }
}
