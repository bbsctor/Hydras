using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_41Frame : CommonResponseFrame
    {
        //帧头                          帧的数据域      帧尾
        //地址码	功能码	数据域长度               CRC低	CRC高
        //01	    41	    01          33           10	49

        public Response_41Frame()
        {
            base.data.Size = 1;
            base.data.RelateElements = null;
        }

        public override bool parse()
        {
            if (this.Data.Length == 4)
            {
                this.type = TYPE.INIT;
                base.data.RelateElements = null;
                return true;
            }
            else
            {
                this.type = TYPE.NORMAL;
            }
            return base.parse();
        }
    }
}

