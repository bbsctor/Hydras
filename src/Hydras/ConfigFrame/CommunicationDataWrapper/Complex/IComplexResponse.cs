using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.CommunicationDataWrapper.Complex
{
    public interface IComplexResponse:IResponse
    {
        bool IsNull
        {
            get;
            set;
        }

        void parseData();
    }
}
