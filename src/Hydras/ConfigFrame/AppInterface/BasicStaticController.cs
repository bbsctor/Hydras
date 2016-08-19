using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService;

namespace ConfigFrame.AppInterface
{
    public class BasicStaticController : BasicController, IStaticController
    {
        protected List<BasicStaticController> subController;

        protected ICommunicationService cService;

        public ICommunicationService CommService
        {
            get { return cService; }
            set 
            { 
                this.cService = value;
                for (int i = 0; i < subController.Count; i++)
                {
                    subController[i].CommService = value;
                }
            }
        }

        private IInteractionValidator interactionValidator;

        public IInteractionValidator InteractionValidator
        {
            get { return interactionValidator; }
            set { interactionValidator = value; }
        }

        public BasicStaticController()
        {
            subController = new List<BasicStaticController>();
        }

        public void add(BasicStaticController controller)
        {
            subController.Add(controller);
        }

        public void beforeExecute(string action)
        {
            OnRaiseExecuteInfoEvent(new ExecuteInfoEventArgs("start " + action));
        }

        public void afterExecute(string action)
        {
            OnRaiseExecuteInfoEvent(new ExecuteInfoEventArgs("finish " + action));
        }

        public override void execute(string action)
        {
            beforeExecute(action);
            resetDataModel();
            base.execute(action);
            IRequest[] reqs = ModelProtocolService.
                        genRequestByActionName(Target, Model);
            IResponse resp;
            for (int i = 0; i < reqs.Length; i++)
            {
                resp = cService.processRequest(reqs[i]);
                if (interactionValidator.validate(reqs[i], resp) == true)
                {
                    IMetaModel model = ModelProtocolService.handleResponse(resp);
                    handleResult(model);
                }
                
            }
            afterExecute(action);
        }

        public override void sendOnly(string action)
        {
            IRequest[] reqs = ModelProtocolService.
                        genRequestByActionName(Target, Model);
            cService.sendOnly(reqs);
        }
    }
}
