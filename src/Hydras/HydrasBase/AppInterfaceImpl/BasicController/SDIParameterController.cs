using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class SDIParameterController : HydrasBasicController
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
                SDIParameterMetaModel cModel = mModel as SDIParameterMetaModel;
                if (cModel != null)
                {
                    switch (cModel.type)
                    {
                        case SDIParameterMetaModel.TYPE.PARA_ORDER:
                            SDIParameterDataModel sdiPara = (SDIParameterDataModel)cModel.Data;
                            ((SDIParameterDataModel)model).update(sdiPara);
                            break;
                    }
                }
            }
        }
    }
}
