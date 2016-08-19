using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.CommunicationDataWrapper.Simple
{
    public class SimpleFrame
    {
        private byte[] data;
        public byte[] Data
        {
            get { return data; }
            set { this.data = value; }
        }

        private bool isNull;
        public bool IsNull
        {
            get { return isNull; }
            set { this.isNull = value; }
        }
    }
}
