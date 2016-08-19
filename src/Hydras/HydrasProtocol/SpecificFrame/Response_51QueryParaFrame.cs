using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_51QueryParaFrame : CommonResponseFrame
    {

        public BasicElement[] values;

        public Response_51QueryParaFrame()
        {
            values = new BasicElement[7];
            for (int i = 0; i < 7; i++)
            {
                values[i].Name = "values" + i;
                values[i].Size = 5;
            }

            base.data.RelateElements = values;
        }
    }
}
