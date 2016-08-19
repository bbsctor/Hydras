using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationDataWrapper;

namespace ConfigFrame.CommunicationService
{
    public interface ICommunicationService
    {
        IDataModel ConnectParameterModel
        {
            get;
            set;
        }
        void connect();
        void sendOnly(IRequest[] req);
        void process(IRequest req, IResponse resp);
        void disconnect();
        IResponse[] processRequestBatch(IRequest[] reqs);
        IResponse processRequest(IRequest req);
        int Interval
        {
            set;
        }
        int TimeOut
        {
            set;
        }
    }
}
