using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_43Frame : CommonResponseFrame
    {
        public BasicElement level1Pwd;
        public BasicElement level2Pwd;
        public BasicElement level3Pwd;

        public Response_43Frame()
        {
            level1Pwd = new BasicElement();
            level1Pwd.Name = "level1Pwd";
            level1Pwd.Size = 9;

            level2Pwd = new BasicElement();
            level2Pwd.Name = "level2Pwd";
            level2Pwd.Size = 9;

            level3Pwd = new BasicElement();
            level3Pwd.Name = "level3Pwd";
            level3Pwd.Size = 9;

            base.data.RelateElements = new BasicElement[]{
                level1Pwd,
                level2Pwd,
                level3Pwd
            };
        }
    }
}


