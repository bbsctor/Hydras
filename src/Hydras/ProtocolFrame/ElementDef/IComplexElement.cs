using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolFrame.ElementDef
{
    public interface IComplexElement:IElement
    {
        bool IsBuilt
        {
            get;
            set;
        }

        bool IsParsed
        {
            get;
            set;
        }
        IElement[] RelateElements
        {
            get;
            set;
        }

        bool build();
        bool parse();
    }
}
