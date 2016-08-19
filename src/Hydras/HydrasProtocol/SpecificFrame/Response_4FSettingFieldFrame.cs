using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_4FSettingFieldFrame : CommonResponseFrame
    {
        public BasicElement unknown1;
        public BasicElement unknown2;
        public BasicElement sn;
        public BasicElement settingName;
        public BasicElement settingNameForShort;
        public BasicElement settingContent;
        public BasicElement countFormat1;
        public BasicElement settingValue;
        public BasicElement countFormatAndScope;
        public BasicElement prompt;

        public Response_4FSettingFieldFrame()
        {
            unknown1 = new BasicElement();
            unknown1.Name = "unknown1";
            unknown1.Size = 1;

            unknown2 = new BasicElement();
            unknown2.Name = "unknown2";
            unknown2.Size = 1;

            sn = new BasicElement();
            sn.Name = "sn";
            sn.Size = 1;

            settingName = new BasicElement();
            settingName.Name = "settingName";
            settingName.Size = 33;

            settingNameForShort = new BasicElement();
            settingNameForShort.Name = "settingNameForShort";
            settingNameForShort.Size = 9;

            settingContent = new BasicElement();
            settingContent.Name = "settingContent";
            settingContent.Size = 9;

            countFormat1 = new BasicElement();
            countFormat1.Name = "countFormat1";
            countFormat1.Size = 9;

            settingValue = new BasicElement();
            settingValue.Name = "countFormat1";
            settingValue.Size = 16;

            countFormatAndScope = new BasicElement();
            countFormatAndScope.Name = "countFormatAndScope";
            countFormatAndScope.Size = 43;

            prompt = new BasicElement();
            prompt.Name = "prompt";
            prompt.Size = 129;

            base.data.RelateElements = new BasicElement[]{
                unknown1,
                unknown2,
                sn,
                settingName,
                settingNameForShort,
                settingContent,
                countFormat1,
                settingValue,
                countFormatAndScope,
                prompt
            };
        }
    }
}
