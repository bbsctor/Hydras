using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl.Common
{
    public class BaseSondeParameterInfoViewModel : BasicModel, IViewModel
    {
        private byte sn;

        public byte Sn
        {
            get { return sn; }
            set { sn = value; }
        }
        //setting paraNum
        private byte paraNum;

        public byte ParaNum
        {
            get { return paraNum; }
            set { paraNum = value; }
        }

        //setting parameter
        private byte paraCode;

        public byte ParaCode
        {
            get { return paraCode; }
            set { paraCode = value; }
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
        private string calUnit;

        public string CalUnit
        {
            get { return calUnit; }
            set { calUnit = value; }
        }
        //private string formatAndScope;

        //public string FormatAndScope
        //{
        //    get { return formatAndScope; }
        //    set { formatAndScope = value; }
        //}

        private string format;

        public string Format
        {
            get { return format; }
            set { format = value; }
        }
        private byte[] scopeLow;

        public byte[] ScopeLow
        {
            get { return scopeLow; }
            set { scopeLow = value; }
        }
        private byte[] scopeHigh;

        public byte[] ScopeHigh
        {
            get { return scopeHigh; }
            set { scopeHigh = value; }
        }


        private string calFormat1;

        public string CalFormat1
        {
            get { return calFormat1; }
            set { calFormat1 = value; }
        }
        private string calFormat2;

        public string CalFormat2
        {
            get { return calFormat2; }
            set { calFormat2 = value; }
        }

        public override bool Equals(Object obj)
        {
            // Performs an equality check on two points (integer pairs).
            if (obj == null || GetType() != obj.GetType()) return false;
            BaseSondeParameterInfoViewModel p = (BaseSondeParameterInfoViewModel)obj;
            return (paraCode.Equals(p.paraCode));
        }
    }
}
