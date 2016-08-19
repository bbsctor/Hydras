using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class CalibrationDetailInfoViewModel : BasicModel, IViewModel
    {

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
        private string paraName;

        public string ParaName
        {
            get { return paraName; }
            set { paraName = value; }
        }
        private string paraNameForShort;

        public string ParaNameForShort
        {
            get { return paraNameForShort; }
            set { paraNameForShort = value; }
        }
        private string paraUnit;

        public string ParaUnit
        {
            get { return paraUnit; }
            set { paraUnit = value; }
        }
        private string countFormat1;

        public string CountFormat1
        {
            get { return countFormat1; }
            set { countFormat1 = value; }
        }
        private string paraValue;

        public string ParaValue
        {
            get { return paraValue; }
            set { paraValue = value; }
        }
        private string countFormat2;

        public string CountFormat2
        {
            get { return countFormat2; }
            set { countFormat2 = value; }
        }
        private string scope;

        public string Scope
        {
            get { return scope; }
            set { scope = value; }
        }
        private string prompt;

        public string Prompt
        {
            get { return prompt; }
            set { prompt = value; }
        }
    }
}