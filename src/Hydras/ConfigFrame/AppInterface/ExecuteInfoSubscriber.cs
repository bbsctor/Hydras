using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.AppInterface
{
    public class ExecuteInfoSubscriber
    {
        public virtual void HandleExecuteInfoEvent(object sender, ExecuteInfoEventArgs e)
        {
            Console.WriteLine(" received this message: {0}", e.Message);
        }
    }
}
