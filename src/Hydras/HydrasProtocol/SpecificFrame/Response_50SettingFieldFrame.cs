using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_50SettingFieldFrame : CommonResponseFrame
    {

        public BasicElement fix;
        public BasicElement value;
        public BasicElement name;

        public Response_50SettingFieldFrame()
        {
            fix = new BasicElement();
            fix.Name = "fix";
            fix.Size = 2;

            value = new BasicElement();
            value.Name = "value";
            value.Size = 9;

            name = new BasicElement();
            name.Name = "name";
            name.Size = 9;

            base.data.RelateElements = new BasicElement[]{
                fix,
                value,
                name
            };
        }
    }
}
