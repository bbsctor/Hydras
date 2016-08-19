using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class OnlineParameterValueController : HydrasBasicController
    {
        public override void execute(string action)
        {
            setSlaveAddr();
            base.execute(action);
        }

        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                ParameterMetaModel cModel = mModel as ParameterMetaModel;
                if (cModel != null)
                {
                    OnlineParameterValueDataModel sondePara = (OnlineParameterValueDataModel)cModel.Data;
                    switch (cModel.type)
                    {
                        case ParameterMetaModel.TYPE.PARAMETER_VALUE:
                            ((OnlineParameterValueDataModel)model).update(sondePara);
                            break;
                    }
                }
            }
        }
    }
}

