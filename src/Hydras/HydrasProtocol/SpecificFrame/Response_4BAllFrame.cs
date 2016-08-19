using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_4BAllFrame : CommonResponseFrame
    {
        public enum TYPE { ABNORMAL, NORMAL };
        private TYPE type;

        public TYPE Type
        {
            get { return type; }
            set { type = value; }
        }

        public BasicElement paraCode1;
        public BasicElement fixed1;
        public BasicElement paraCode2;

        public Response_4BAllFrame()
        {
            paraCode1 = new BasicElement();
            paraCode1.Name = "paraCode1";
            paraCode1.Size = 1;

            fixed1 = new BasicElement();
            fixed1.Name = "fixed1";
            fixed1.Size = 2;

            paraCode2 = new BasicElement();
            paraCode2.Name = "paraCode2";
            paraCode2.Size = 1;

            
        }

        public override bool parse()
        {
            if (this.Data.Length == 5)
            {
                this.type = TYPE.ABNORMAL;
                base.data.RelateElements = null;
                return true;
            }
            else
            {
                this.type = TYPE.NORMAL;
                base.data.RelateElements = new BasicElement[]{
                paraCode1,
                fixed1,
                paraCode2
                };
            }
            return base.parse();
        }
    }
}

