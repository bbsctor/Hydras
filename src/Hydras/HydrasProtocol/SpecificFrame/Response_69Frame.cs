using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_69Frame : CommonResponseFrame
    {
        public BasicElement format;
        public BasicElement useDelimiter;

        public Response_69Frame()
        {
            format = new BasicElement();
            format.Name = "format";
            format.Size = 1;

            useDelimiter = new BasicElement();
            useDelimiter.Name = "useDelimiter";
            useDelimiter.Size = 1;

            base.data.RelateElements = new BasicElement[]{
                format,
                useDelimiter
                };
        }
    }
}

