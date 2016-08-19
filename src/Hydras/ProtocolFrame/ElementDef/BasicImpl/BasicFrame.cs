using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;
using ProtocolFrame.ElementDef.BasicImpl.Util;

namespace ProtocolFrame.ElementDef.BasicImpl
{
    public class BasicFrame : CompositeElement
    {
        public override bool build()
        {
            base.build();
            List<byte> result = new List<byte>();
            for (int i = 0; i < RelateElements.Length; i++)
            {
                if (RelateElements[i].Data == null && RelateElements[i].Size > 0)
                {
                    this.Data = null;
                    this.IsBuilt = false;
                    return false;
                }
                else if (RelateElements[i].Data != null && RelateElements[i].Size == RelateElements[i].Data.Length)
                {
                    result.AddRange(RelateElements[i].Data);
                }
            }
            this.Data = result.ToArray();
            if (this.Data != null)
            {
                this.Size = this.Data.Length;
            }
            else
            {
                this.Size = 0;
            }

            this.IsBuilt = true;
            return true;
        }
    }
}
