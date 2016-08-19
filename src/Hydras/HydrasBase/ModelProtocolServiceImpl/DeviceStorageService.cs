using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationDataWrapper;
using HydrasBase.CommunicationDataWrapper;
using HydrasBase.ModelProtocolServiceImpl.FrameParser;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl
{
    public class DeviceStorageService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            switch (action)
            {
                case "getDeviceStorage":
                    return genGetDeviceStorage();
            }
            return null;
        }

        public IRequest[] genGetDeviceStorage()
        {
            Request req = new Request();
            req.requestFrame = new Request_4EQueryDeviceStorageFrame();
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {
            Response resp = (Response)response;
            DeviceStorageDataModel model = DeviceStorageParser.parse((Response_4EQueryDeviceStorageFrame)resp.responseFrame);
            DeviceStorageMetaModel mModel = new DeviceStorageMetaModel();
            mModel.type = DeviceStorageMetaModel.TYPE.DEVICE_STORAGE;
            mModel.Data = model;
            return mModel;
        }
    }
}
