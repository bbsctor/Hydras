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
    public class DeviceStorageController : HydrasBasicController
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
                DeviceStorageMetaModel cModel = mModel as DeviceStorageMetaModel;
                if (cModel != null)
                {
                    switch (cModel.type)
                    {
                        case DeviceStorageMetaModel.TYPE.DEVICE_STORAGE:
                            DeviceStorageDataModel deviceStorage = (DeviceStorageDataModel)cModel.Data;
                            ((DeviceStorageDataModel)model).update(deviceStorage);
                            break;
                    }
                }
            }
        }
    }
}
