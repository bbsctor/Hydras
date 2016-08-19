using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_46Frame : CommonResponseFrame
    {
        public enum TYPE { PARAMETER_NUM, PERAMETER_DETAIL};
        private TYPE type;

        public TYPE Type
        {
            get { return type; }
            set { type = value; }
        }

        public BasicElement paraCode1;
        //parameter num define
        public BasicElement paraNum;
        //parameter define
        public BasicElement paraName;
        //public BasicElement paraNameAndSettingType;
        public BasicElement paraNameForShort;
        public BasicElement settingType;
        public BasicElement paraFormat;
        public BasicElement settingContent;
        public BasicElement paraFormatAndScope;
        public BasicElement prompt;

        public Response_46Frame()
        {
            paraCode1 = new BasicElement();
            paraCode1.Name = "paraCode1";
            paraCode1.Size = 1;

            //parameter num define
            paraNum = new BasicElement();
            paraNum.Name = "paraNum";
            paraNum.Size = 1;

            //parameter define
            paraName = new BasicElement();
            paraName.Name = "paraName";
            paraName.Size = 33;

            //paraNameAndSettingType = new BasicElement();
            //paraNameAndSettingType.Name = "paraNameAndSettingType";
            //paraNameAndSettingType.Size = 18;

            paraNameForShort = new BasicElement();
            paraNameForShort.Name = "paraNameForShort";
            paraNameForShort.Size = 9;

            settingType = new BasicElement();
            settingType.Name = "settingType";
            settingType.Size = 9;

            paraFormat = new BasicElement();
            paraFormat.Name = "paraFormat";
            paraFormat.Size = 9;

            settingContent = new BasicElement();
            settingContent.Name = "settingContent";
            settingContent.Size = 16;

            paraFormatAndScope = new BasicElement();
            paraFormatAndScope.Name = "paraFormatAndScope";
            paraFormatAndScope.Size = 43;

            prompt = new BasicElement();
            prompt.Name = "prompt";
            prompt.Size = 131;

            //base.data.RelateElements = new BasicElement[]{
            //    paraCode1,
            //    paraName,
            //    paraNameAndSettingType,
            //    paraFormat,
            //    settingContent,
            //    paraFormatAndScope,
            //    prompt
            //};
        }

        public override bool parse()
        {
            if (this.Data[2] == 0x02)
            {
                this.type = TYPE.PARAMETER_NUM;
                base.data.RelateElements = new BasicElement[]{
                paraCode1,
                paraNum
                };
            }
            else
            {
                this.type = TYPE.PERAMETER_DETAIL;
                base.data.RelateElements = new BasicElement[]{
                paraCode1,
                paraName,
                //paraNameAndSettingType,
                paraNameForShort,
                settingType,
                paraFormat,
                settingContent,
                paraFormatAndScope,
                prompt
                };
            }
            return base.parse();
        }
    }
}
