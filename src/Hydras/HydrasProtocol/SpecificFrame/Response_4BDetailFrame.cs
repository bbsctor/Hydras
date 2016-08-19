using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_4BDetailFrame : CommonResponseFrame
    {
        public BasicElement paraCode1;
        public BasicElement fixed1;
        public BasicElement paraCode2;
        public BasicElement paraName;
        public BasicElement paraNameForShort;
        public BasicElement paraUnit;
        public BasicElement countFormat1;
        public BasicElement paraValue;
        public BasicElement countFormat2;
        public BasicElement scope;
        public BasicElement prompt;

        public Response_4BDetailFrame()
        {
            paraCode1 = new BasicElement();
            paraCode1.Name = "paraCode1";
            paraCode1.Size = 1;

            fixed1 = new BasicElement();
            fixed1.Name = "fixed1";
            fixed1.Size = 1;

            paraCode2 = new BasicElement();
            paraCode2.Name = "paraCode2";
            paraCode2.Size = 1;

            paraName = new BasicElement();
            paraName.Name = "paraName";
            paraName.Size = 33;

            paraNameForShort = new BasicElement();
            paraNameForShort.Name = "paraNameForShort";
            paraNameForShort.Size = 9;

            paraUnit = new BasicElement();
            paraUnit.Name = "paraUnit";
            paraUnit.Size = 9;

            countFormat1 = new BasicElement();
            countFormat1.Name = "countFormat1";
            countFormat1.Size = 9;

            paraValue = new BasicElement();
            paraValue.Name = "paraValue";
            paraValue.Size = 16;

            countFormat2 = new BasicElement();
            countFormat2.Name = "countFormat2";
            countFormat2.Size = 6;

            scope = new BasicElement();
            scope.Name = "scope";
            scope.Size = 37;

            prompt = new BasicElement();
            prompt.Name = "prompt";
            prompt.Size = 129;

            base.data.RelateElements = new BasicElement[]{
                paraCode1,
                fixed1,
                paraCode2,
                paraName,
                paraNameForShort,
                paraUnit,
                countFormat1,
                paraValue,
                countFormat2,
                scope,
                prompt
            };
        }
    }
}
