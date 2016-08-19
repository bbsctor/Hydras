using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationDataWrapper;

namespace ConfigFrame.AppInterface
{
    public class BasicDynamicController:BasicController, IDynamicController
    {
        protected LocalRunningServer server;
        protected IDynamicController temp;
        public LocalRunningServer Server
        {
            get { return server; }
            set { this.server = value; }
        }

        public override void execute(string action)
        {
            if (action.Equals("bindingServer"))
            {
                bindingServer();
            }
            else
            {
                base.execute(action);
                server.doWork(this);
            }

        }

        public virtual IRequest[] genIdleRequest()
        {
            return null;
        }

        public virtual void bindingServer()
        {
            server.SwitchInterval = 3000;
            temp = server.CurrentController;
            server.CurrentController = this;
        }

        public virtual void unbindingServer()
        {
            server.CurrentController = temp;
        }
    }
}
