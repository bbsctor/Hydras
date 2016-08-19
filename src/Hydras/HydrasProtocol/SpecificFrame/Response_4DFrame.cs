using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_4DFrame : CommonResponseFrame
    {
        public BasicElement constByte1;
        public BasicElement constByte2;

        public BasicElement[] values;

        private int valuesLength;

        public Response_4DFrame()
        {
            constByte1 = new BasicElement();
            constByte1.Size = 1;
            constByte1.Name = "constByte1";

            constByte2 = new BasicElement();
            constByte2.Size = 1;
            constByte2.Name = "constByte2";
        }

        protected override void preParse()
        {
            base.preParse();
            valuesLength = (data.Size - 2) / 5;
            values = new BasicElement[valuesLength];
            for (int i = 0; i < valuesLength; i++)
            {
                values[i] = new BasicElement();
                values[i].Size = 5;
                values[i].Name = "values " + i;
            }

            List<BasicElement> list = new List<BasicElement>();
            list.AddRange(new BasicElement[]{constByte1, constByte2});
            list.AddRange(values);
            base.data.RelateElements = list.ToArray();
        }
    }
}
