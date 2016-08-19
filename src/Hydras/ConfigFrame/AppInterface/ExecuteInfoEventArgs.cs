using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.AppInterface
{
    public class ExecuteInfoEventArgs : EventArgs
    {
        public ExecuteInfoEventArgs(string s)
        {
            message = s;
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
