using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_4FSelectedParaFrame : CommonResponseFrame
    {
        public BasicElement constByte1;
        public BasicElement constByte2;
        public BasicElement sn;
        public BasicElement paraCode;
        public BasicElement constByte3;
        public BasicElement paraName;
        public BasicElement paraNameForShort;
        public BasicElement calUnit;
        public BasicElement formatAndScope;
        public BasicElement calFormat1;
        public BasicElement calFormat2;

        public Response_4FSelectedParaFrame()
        {
            constByte1 = new BasicElement();
            constByte1.Name = "constByte1";
            constByte1.Size = 1;

            constByte2 = new BasicElement();
            constByte2.Name = "constByte2";
            constByte2.Size = 1;

            sn = new BasicElement();
            sn.Name = "sn";
            sn.Size = 1;

            paraCode = new BasicElement();
            paraCode.Name = "paraCode";
            paraCode.Size = 1;

            constByte3 = new BasicElement();
            constByte3.Name = "constByte3";
            constByte3.Size = 1;

            paraName = new BasicElement();
            paraName.Name = "paraName";
            paraName.Size = 33;

            paraNameForShort = new BasicElement();
            paraNameForShort.Name = "paraNameForShort";
            paraNameForShort.Size = 9;

            calUnit = new BasicElement();
            calUnit.Name = "calUnit";
            calUnit.Size = 9;

            formatAndScope = new BasicElement();
            formatAndScope.Name = "formatAndScope";
            formatAndScope.Size = 32;

            calFormat1 = new BasicElement();
            calFormat1.Name = "calFormat1";
            calFormat1.Size = 9;

            calFormat2 = new BasicElement();
            calFormat2.Name = "calFormat2";
            calFormat2.Size = 9;

            constByte1 = new BasicElement();
            constByte1.Name = "unknown1";
            constByte1.Size = 1;

            base.data.RelateElements = new BasicElement[]{
                constByte1,
                constByte2,
                sn,
                paraCode,
                constByte3,
                paraName,
                paraNameForShort,
                calUnit,
                formatAndScope,
                calFormat1,
                calFormat2
            };
        }
    }
}
