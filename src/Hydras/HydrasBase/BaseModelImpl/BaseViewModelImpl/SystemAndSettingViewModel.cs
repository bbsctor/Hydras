using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class SystemAndSettingViewModel : BasicModel, IViewModel
    {

        private byte paraCode1;

        public byte ParaCode1
        {
            get { return paraCode1; }
            set { paraCode1 = value; }
        }

        private string paraName;

        public string ParaName
        {
            get { return paraName; }
            set { paraName = value; }
        }
        //private string paraNameAndSettingType;

        //public string ParaNameAndSettingType
        //{
        //    get { return paraNameAndSettingType; }
        //    set { paraNameAndSettingType = value; }
        //}

        private string paraNameForShort;

        public string ParaNameForShort
        {
            get { return paraNameForShort; }
            set { paraNameForShort = value; }
        }

        private string settingType;

        public string SettingType
        {
            get { return settingType; }
            set { settingType = value; }
        }

        private string paraFormat;

        public string ParaFormat
        {
            get { return paraFormat; }
            set { paraFormat = value; }
        }
        private byte[] settingContent;

        public byte[] SettingContent
        {
            get { return settingContent; }
            set { settingContent = value; }
        }
        private string paraFormatAndScope;

        public string ParaFormatAndScope
        {
            get { return paraFormatAndScope; }
            set { paraFormatAndScope = value; }
        }
        private string prompt;

        public string Prompt
        {
            get { return prompt; }
            set { prompt = value; }
        }
    }
}