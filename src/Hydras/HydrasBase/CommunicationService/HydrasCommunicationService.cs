using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationDataWrapper;
using HydrasBase.Util;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.CommunicationService
{
    public class HydrasCommunicationService:StandardCommunicationService
    {
        public override void process(IRequest req, IResponse resp)
        {
            resp.Request = req;
            lock (this)
            {
                connect();
                byte[] temp = req.genBytes();
                byte[] recv = serial.sendAndReceive(temp);
                resp.parse(recv);
                if (temp != null && temp[0] == 0x00)
                {
                    if (SlaveAddrContext.slaveDict.ContainsKey((SerialPortModel)ConnectParameterModel) == false)
                    {
                        SlaveAddrContext.addSlaveAddr((SerialPortModel)ConnectParameterModel, recv[0]);
                    }
                }
            }
        }
    }
}
