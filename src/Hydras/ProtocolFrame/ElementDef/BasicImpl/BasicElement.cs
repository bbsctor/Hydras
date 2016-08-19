using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolFrame.ElementDef.BasicImpl
{
    public class BasicElement:IElement
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        private byte[] data;
        public byte[] Data
        {
            get { return data; }
            set { this.data = value; }
        }

        private int size;
        public int Size
        {
            get { return size; }
            set { this.size = value; }
        }
    }
}
