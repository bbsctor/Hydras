using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.ExceptionHandle
{
    public class ExceptionHandler
    {
        public delegate void Handle(Exception e);
        public Handle handle;
        

    }
}
