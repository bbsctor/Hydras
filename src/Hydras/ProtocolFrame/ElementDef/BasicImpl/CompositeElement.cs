using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl.ExceptionImpl;

namespace ProtocolFrame.ElementDef.BasicImpl
{
    public class CompositeElement:ComplexElement
    {
        public override bool build()
        {
            if (base.build() == true && RelateElements != null)
            {
                List<byte> temp = new List<byte>();
                for (int i = 0; i < RelateElements.Length; i++)
                {
                    if (RelateElements[i].Size > 0)
                    {
                        temp.AddRange(RelateElements[i].Data);
                    }

                }
                this.Data = temp.ToArray();
                this.Size = this.Data.Length;
                IsBuilt = true;
            }
            
            return true;
        }

        public virtual bool parse()
        {
            if (Data != null)
            {
                try
                {
                    if (RelateElements != null && RelateElements.Length > 0)
                    {
                        int currentIndex = 0;
                        for (int i = 0; i < RelateElements.Length; i++)
                        {
                            if (RelateElements[i].Size > 0)
                            {
                                RelateElements[i].Data = new byte[RelateElements[i].Size];
                                System.Array.Copy(this.Data, currentIndex, RelateElements[i].Data, 0, RelateElements[i].Size);
                                currentIndex += RelateElements[i].Size;
                            }
                            else
                            {
                                return false;
                            }
                        }

                        for (int i = 0; i < RelateElements.Length; i++)
                        {
                            if (RelateElements[i] is CompositeElement)
                            {
                                if (((CompositeElement)RelateElements[i]).parse() == false)
                                {
                                    return false;
                                }
                            }
                            else if (RelateElements[i] is ComplexElement)
                            {
                                if (((ComplexElement)RelateElements[i]).parse() == false)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new FrameParseException();
                }
                IsParsed = true;
                return true;
            }
            this.IsParsed = false;
            return false;
        }
    }
}
