using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.Util;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class ParameterSetupDetailInfoViewModel : BasicModel, IViewModel
    {
        public enum TYPE { COMBO, TEXT };
        private TYPE type;

        public TYPE Type
        {
            get { return type; }
            set { type = value; }
        }

        private byte paraCode1;

        public byte ParaCode1
        {
            get { return paraCode1; }
            set { paraCode1 = value; }
        }
        private byte paraCode2;

        public byte ParaCode2
        {
            get { return paraCode2; }
            set { paraCode2 = value; }
        }

        private string settingName;

        public string SettingName
        {
            get { return settingName; }
            set { settingName = value; }
        }
        private string calFormat1;

        public string CalFormat1
        {
            get { return calFormat1; }
            set { calFormat1 = value; }
        }

        private byte settingValue;

        public byte SettingValue
        {
            get { return settingValue; }
            set { settingValue = value; }
        }
        private string calFormatAndScope;

        public string CalFormatAndScope
        {
            get { return calFormatAndScope; }
            set { calFormatAndScope = value; }
        }
        private string prompt;

        public string Prompt
        {
            get { return prompt; }
            set { 
                prompt = value;
                parse(prompt);
            }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private string[] selItems;

        public string[] SelItems
        {
            get { return selItems; }
            set { selItems = value; }
        }

        public void parse(string str)
        {
            int index1 = str.IndexOf('(');
            int index2 = str.IndexOf(')');
            if (index1 >= 0 && index2 > index1)
            {
                text = str.Substring(0, index1);
                string temp = str.Substring(index1 + 1, index2 - index1 - 1);
                if (temp.IndexOf(':') >= 0)
                {
                    //"1:Auto 2:High 3:Mid 4:Low"
                    //"1:2 Point Cal 2:4 Point Cal"
                    //"1:Fresh 2:Salt 3:StdMth 4:None 5:Custom"
                    selItems = StringExtension.Split(temp, new Regex("(\\d:(\\d )?)((\\w+ )+(\\D+)?)"));
                    type = TYPE.COMBO;
                }
                else
                {
                    type = TYPE.TEXT;
                }
            }
            else
            {
                type = TYPE.TEXT;
            }
            
        }
    }
}

