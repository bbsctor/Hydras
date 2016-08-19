using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolFrame.ElementDef
{
    public interface IElement
    {
        string Name
        {
            get;
            set;
        }

        byte[] Data
        {
            get;
            set;
        }

        int Size
        {
            get;
            set;
        }
    }
}
