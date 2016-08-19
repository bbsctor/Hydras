using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class LogFileSettingFieldViewModel : BasicModel, IViewModel
    {
        private byte sn;

        public byte Sn
        {
            get { return sn; }
            set { sn = value; }
        }
        private string settingName;

        public string SettingName
        {
            get { return settingName; }
            set { settingName = value; }
        }
        private string settingNameForShort;

        public string SettingNameForShort
        {
            get { return settingNameForShort; }
            set { settingNameForShort = value; }
        }
        private string settingContent;

        public string SettingContent
        {
            get { return settingContent; }
            set { settingContent = value; }
        }
        private string countFormat1;

        public string CountFormat1
        {
            get { return countFormat1; }
            set { countFormat1 = value; }
        }

        private Object settingValue;

        public Object SettingValue
        {
            get { return settingValue; }
            set { settingValue = value; }
        }

        private string countFormatAndScope;

        public string CountFormatAndScope
        {
            get { return countFormatAndScope; }
            set { countFormatAndScope = value; }
        }
        private string prompt;

        public string Prompt
        {
            get { return prompt; }
            set { prompt = value; }
        }
    }
}