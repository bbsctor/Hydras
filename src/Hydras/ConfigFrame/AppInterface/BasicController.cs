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
    public class BasicController
    {
        protected Type modelType;
        public IDataModel model;
        protected IModelProtocolService dService;
        protected string target;

        public Type ModelType
        {
            get { return modelType; }
            set { this.modelType = value; }
        }

        public IDataModel Model
        {
            get { return model; }
            set { this.model = value; }
        }

        public IModelProtocolService ModelProtocolService
        {
            get { return dService; }
            set { this.dService = value; }
        }

        public string Target
        {
            get { return target; }
            set { this.target = value; }
        }

        public ExecuteInfoSubscriber Subscriber
        {
            set { this.RaiseCustomEvent += ((ExecuteInfoSubscriber)value).HandleExecuteInfoEvent; }           
        }

        public event EventHandler<ExecuteInfoEventArgs> RaiseCustomEvent;

        protected virtual void OnRaiseExecuteInfoEvent(ExecuteInfoEventArgs e)
        {
            EventHandler<ExecuteInfoEventArgs> handler = RaiseCustomEvent;

            if (handler != null)
            {
                //e.Message += String.Format(" at {0}", DateTime.Now.ToString());

                handler(this, e);
            }
        }

        public virtual void resetDataModel()
        {
            this.model = (IDataModel)ModelRepository.getModelByType(modelType);
        }

        public void setType(Type type)
        {
            this.modelType = type;
        }

        public virtual void execute(string action)
        {
            resetDataModel();
            this.target = action;
        }

        public virtual void sendOnly(string action)
        {

        }

        public virtual void handleResult(IMetaModel dModel)
        {

        }

        public virtual string genAction(string mainStr, string[] para)
        {
            ActionStrAssistant.ParameterValue[] paraArray = new ActionStrAssistant.ParameterValue[para.Length];
            for (int i = 0; i < para.Length; i++)
            {
                paraArray[i] = new ActionStrAssistant.ParameterValue("para" + (i + 1), para[i]);
            }
            string action = ActionStrAssistant.buildActionStr(mainStr, paraArray);
            return action;
        }
    }
}
