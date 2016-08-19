using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.ModelProtocolServiceImpl;
using HydrasBase.Util;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class HydrasBasicController:BasicStaticController
    {
        public virtual void setSlaveAddr()
        {
            HydrasProtocol.FrameContext.slaveAddr = SlaveAddrContext.
                getSlaveAddr((SerialPortModel)this.CommService.ConnectParameterModel);
        }

        public override void resetDataModel()
        {
            SondeInfoListDataModel sondeListModel = 
                (SondeInfoListDataModel)ModelRepository.getModelByType(typeof(SondeInfoListDataModel));
            SondeInfoDataModel sondeModel = 
                sondeListModel.getElementByPortModel((SerialPortModel)CommService.ConnectParameterModel);
            this.model = (IDataModel)sondeModel.sondeDetailDataModel.getModelByType(modelType);
            //BasicService.currentSlaveAddr = BasicService.getSlaveAddr((SerialPortModel)this.CommService.ConnectParameterModel);
        }
    }
}
