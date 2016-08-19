using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class DateFormatViewModel : BasicModel, IViewModel
    {
        private string dateFormat;

        public string DateFormat
        {
            get { return dateFormat; }
            set { dateFormat = value; }
        }
        private byte useDelimiter;

        public byte UseDelimiter
        {
            get { return useDelimiter; }
            set { useDelimiter = value; }
        }
    }
}