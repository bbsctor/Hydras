using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.CommunicationDataWrapper
{
    public interface IResponse
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

        IRequest Request
        {
            get;
            set;
        }

        bool validate();
        void parse(byte[] inData);
    }
}
