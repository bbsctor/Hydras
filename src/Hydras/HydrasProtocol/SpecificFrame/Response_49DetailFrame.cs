using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_49DetailFrame : CommonResponseFrame
    {
        public BasicElement paraCode1;
        public BasicElement fixed1;
        public BasicElement paraCode2;
        public BasicElement settingName1;
        public BasicElement unknown1;
        public BasicElement settingName2;
        public BasicElement settingContent;
        public BasicElement calFormat1;
        public BasicElement settingValue;
        public BasicElement unknown2;
        public BasicElement calFormatAndScope;
        public BasicElement prompt;

        public Response_49DetailFrame()
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

            settingName1 = new BasicElement();
            settingName1.Name = "settingName1";
            settingName1.Size = 9;

            unknown1 = new BasicElement();
            unknown1.Name = "unknown1";
            unknown1.Size = 24;

            settingName2 = new BasicElement();
            settingName2.Name = "settingName2";
            settingName2.Size = 9;

            settingContent = new BasicElement();
            settingContent.Name = "settingContent";
            settingContent.Size = 9;

            calFormat1 = new BasicElement();
            calFormat1.Name = "calFormat1";
            calFormat1.Size = 9;

            settingValue = new BasicElement();
            settingValue.Name = "settingValue";
            settingValue.Size = 1;

            unknown2 = new BasicElement();
            unknown2.Name = "unknown2";
            unknown2.Size = 15;

            calFormatAndScope = new BasicElement();
            calFormatAndScope.Name = "formatAndScope";
            calFormatAndScope.Size = 43;

            prompt = new BasicElement();
            prompt.Name = "prompt";
            prompt.Size = 129;

            base.data.RelateElements = new BasicElement[]{
                paraCode1,
                fixed1,
                paraCode2,
                settingName1,
                unknown1,
                settingName2,
                settingContent,
                calFormat1,
                settingValue,
                unknown2,
                calFormatAndScope,
                prompt
            };
        }
    }
}
