using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;

namespace ConfigFrame.AppInterface
{
    public interface IController
    {
        void execute(string action);
        void handleResult(IMetaModel dModel);

        Type ModelType
        {
            get;
            set;
        }

        IDataModel Model
        {
            get;
            set;
        }

        IModelProtocolService ModelProtocolService
        {
            get;
            set;
        }
        string Target
        {
            get;
            set;
        }
    }
}
