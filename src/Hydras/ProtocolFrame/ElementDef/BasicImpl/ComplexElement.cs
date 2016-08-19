using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolFrame.ElementDef.BasicImpl
{
    public class ComplexElement : BasicElement, IComplexElement
    {
        private bool isBuilt;
        public bool IsBuilt
        {
            get { return isBuilt; }
            set { this.isBuilt = value; }
        }

        private bool isParsed;
        public bool IsParsed
        {
            get { return isParsed; }
            set { this.isParsed = value; }
        }

        private IElement[] relateElements;
        public IElement[] RelateElements
        {
            get { return relateElements; }
            set { relateElements = value; }
        }

        public virtual bool build()
        {
            if (relateElements != null)
            {
                for (int i = 0; i < relateElements.Length; i++)
                {
                    if (relateElements[i] is IComplexElement)
                    {
                        if (((IComplexElement)relateElements[i]).build() == false)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public virtual bool parse()
        {
            return true;
        }
    }
}
