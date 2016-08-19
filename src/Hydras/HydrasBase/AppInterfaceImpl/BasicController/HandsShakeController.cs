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
using HydrasProtocol;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class HandsShakeController : HydrasBasicController
    {
        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();

            if ("handsShake".Equals(action))
            {
                for (int i = 0; i < 2; i++)
                {
                    base.execute("handsShake");
                }    
            }
        }
        public override void resetDataModel()
        {
            SondeInfoListDataModel sondeListModel =
                (SondeInfoListDataModel)ModelRepository.getModelByType(typeof(SondeInfoListDataModel));
            this.model = sondeListModel.getElementByPortModel((SerialPortModel)CommService.ConnectParameterModel);
        }
        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                HandsShakeMetaModel cModel = mModel as HandsShakeMetaModel;
                if (cModel != null)
                {
                    switch (cModel.type)
                    {
                        case HandsShakeMetaModel.TYPE.SONDE_INFO:
                            SondeInfoDataModel sondeInfo = (SondeInfoDataModel)cModel.Data;
                            ((SondeInfoDataModel)model).update(sondeInfo);
                            break;
                    }
                }
            }
        }
    }
}
