using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.CommunicationDataWrapper
{
    public interface IRequest
    {
        byte[] Data
        {
            get;
            set;
        }
        bool IsNull
        {
            get;
            set;
        }
        IResponse genResponse();
        byte[] genBytes();
    }
}
