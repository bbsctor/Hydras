using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_51DownloadLogFrame : CommonResponseFrame
    {
        public BasicElement constByte1;
        public BasicElement constByte2;
        public BasicElement sn;
        public BasicElement constByte3;
        public BasicElement constByte4;
        public BasicElement constByte5;

        public BasicElement[] values;

        private int valuesLength;

        public Response_51DownloadLogFrame()
        {
            constByte1 = new BasicElement();
            constByte1.Size = 1;
            constByte1.Name = "constByte1";

            constByte2 = new BasicElement();
            constByte2.Size = 1;
            constByte2.Name = "constByte2";

            constByte3 = new BasicElement();
            constByte3.Size = 1;
            constByte3.Name = "constByte3";

            constByte4 = new BasicElement();
            constByte4.Size = 1;
            constByte4.Name = "constByte4";

            constByte5 = new BasicElement();
            constByte5.Size = 1;
            constByte5.Name = "constByte5";

            sn = new BasicElement();
            sn.Size = 1;
            sn.Name = "sn";

        }

        protected override void preParse()
        {
            base.preParse();
            valuesLength = (data.Size - 6) / 5;
            values = new BasicElement[valuesLength];
            for (int i = 0; i < valuesLength; i++)
            {
                values[i] = new BasicElement();
                values[i].Size = 5;
                values[i].Name = "values " + i;
            }

            List<BasicElement> list = new List<BasicElement>();
            list.AddRange(new BasicElement[]{constByte1, constByte2, sn, constByte3, constByte4, constByte5});
            list.AddRange(values);
            base.data.RelateElements = list.ToArray();
        }
    }
}
