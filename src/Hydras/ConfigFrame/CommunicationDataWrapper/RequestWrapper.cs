using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.CommunicationDataWrapper
{
    public class RequestWrapper
    {
        List<IRequest> list;
        public RequestWrapper()
        {
            list = new List<IRequest>();
        }

        public RequestWrapper(IRequest req):
            this()
        {
            list.Add(req);
        }

        public void add(IRequest req)
        {
            list.Add(req);
        }

        public IRequest[] getRequestArray()
        {
            return list.ToArray();
        }
    }
}
