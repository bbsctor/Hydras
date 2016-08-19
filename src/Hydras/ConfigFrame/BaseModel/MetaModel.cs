using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.BaseModel
{
    public class MetaModel : IMetaModel
    {
        private Object data;
        public Object Data
        {
            get { return data; }
            set { this.data = value; }
        }

        public static readonly MetaModel NULL = new NullMetaModel();

        private class NullMetaModel : MetaModel
        {

        }
    }
}
