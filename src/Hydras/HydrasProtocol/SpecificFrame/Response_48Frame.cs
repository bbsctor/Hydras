using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_48Frame : CommonResponseFrame
    {
        public enum TYPE { PARAMETER_NUM, PERAMETER_DETAIL };
        private TYPE type;

        public TYPE Type
        {
            get { return type; }
            set { type = value; }
        }

        public BasicElement sn;

        //parameter num define
        public BasicElement paraNum;

        //parameter define
        public BasicElement paraCode;
        public BasicElement constByte1;
        public BasicElement paraName;
        public BasicElement paraNameForShort;
        public BasicElement calUnit;
        public BasicElement formatAndScope;
        //public BasicElement format;
        //public BasicElement scopeLow;
        //public BasicElement scopeHigh;

        public BasicElement calFormat1;
        public BasicElement calFormat2;
        public BasicElement unknown1;

        public Response_48Frame()
        {
            sn = new BasicElement();
            sn.Name = "sn";
            sn.Size = 1;

            //parameter num define
            paraNum = new BasicElement();
            paraNum.Name = "paraNum";
            paraNum.Size = 1;

            //parameter define
            paraCode = new BasicElement();
            paraCode.Name = "paraCode";
            paraCode.Size = 1;

            constByte1 = new BasicElement();
            constByte1.Name = "constByte1";
            constByte1.Size = 1;

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

            unknown1 = new BasicElement();
            unknown1.Name = "unknown1";
            unknown1.Size = 1;

            //base.data.RelateElements = new BasicElement[]{
            //    paraCode1,
            //    paraCode2,
            //    fixed1,
            //    paraName,
            //    paraNameForShort,
            //    calUnit,
            //    formatAndScope,
            //    calFormat1,
            //    calFormat2,
            //    unknown1
            //};
        }
        public override bool parse()
        {
            if (this.Data[2] == 0x02)
            {
                this.type = TYPE.PARAMETER_NUM;
                base.data.RelateElements = new BasicElement[]{
                sn,
                paraNum
                };
            }
            else
            {
                this.type = TYPE.PERAMETER_DETAIL;
                base.data.RelateElements = new BasicElement[]{
                sn,
                paraCode,
                constByte1,
                paraName,
                paraNameForShort,
                calUnit,
                formatAndScope,
                calFormat1,
                calFormat2,
                unknown1
                };
            }
            return base.parse();
        }
    }
}
