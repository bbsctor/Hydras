using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.CommunicationService;

namespace ConfigFrame.AppInterface
{
    public interface IStaticController:IController
    {
        ICommunicationService CommService
        {
            get;
            set;
        }
    }
}
