using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_40Frame:CommonRequestFrame
    {
        public Request_40Frame()
        {
            preBuild(0x00, 0x40, null);
            build();
        }
    }
}
