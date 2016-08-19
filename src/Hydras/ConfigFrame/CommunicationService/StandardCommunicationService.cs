using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;


namespace ConfigFrame.CommunicationService
{
    public class StandardCommunicationService:ICommunicationService
    {
        protected SerialPortAdapter serial;
        private byte[] testBytes;

        private IDataModel connectParameterModel;

        public IDataModel ConnectParameterModel
        {
            get { return connectParameterModel; }
            set 
            { 
                connectParameterModel = value;
                serial = SerialPortPool.getPortAdapter((SerialPortModel)value);
            }
        }

        public StandardCommunicationService()
        {
            serial = new SerialPortAdapter();
        }

        public int TimeOut
        {
            set { serial.SetTimeOut(value); }
        }

        public int Interval
        {
            set { serial.Interval = value; }
        }

        public virtual void process(IRequest req, IResponse resp)
        {
            resp.Request = req;
            lock (this)
            {
                connect();
                byte[] recv = serial.sendAndReceive(req.genBytes());
                //byte[] recv = SerialPortMock.sendAndReceive(req.genBytes());
                resp.parse(recv);
            }
        }

        public void sendOnly(IRequest[] req)
        {
            for (int i = 0; i < req.Length; i++)
            {
                serial.writeOnly(req[i].genBytes());
            }
        }

        public void connect()
        {
            //SerialPortModel sModel = (SerialPortModel)connectParameterModel;
            //serial.SetPortBaudRate(System.Convert.ToInt32(sModel.BaudRate));
            //serial.SetPortName(sModel.Port);
            //serial = SerialPortPool.getPortAdapter(sModel);
            serial.openSerial();
        }

        public void disconnect()
        {
            serial.closeSerial();
        }

        public IResponse[] processRequestBatch(IRequest[] reqs)
        {
            List<IResponse> result = new List<IResponse>();
            for (int i = 0; i < reqs.Length; i++)
            {
                IResponse resp = processRequest(reqs[i]);
                if (resp != null)
                {
                    result.Add(resp);
                }
                
            }
            return result.ToArray();
        }

        public IResponse processRequest(IRequest req)
        {
            if (req.IsNull == false)
            {
                IResponse resp = req.genResponse();
                resp.Request = req;
                process(req, resp);
                return resp;
            }
            return null;
        }
    }
}
