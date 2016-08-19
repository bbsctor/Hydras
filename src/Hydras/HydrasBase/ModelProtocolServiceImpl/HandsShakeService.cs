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
using HydrasProtocol;
using HydrasProtocol.SpecificFrame;

namespace HydrasBase.ModelProtocolServiceImpl
{
    public class HandsShakeService : BasicService
    {
        public override IRequest[] genRequestByActionName(string action, IDataModel dModel)
        {
            switch (action)
            {
                case "handsShake":
                    return genHandsShake();
            }
            return null;
        }

        public IRequest[] genHandsShake()
        {
            Request req = new Request();
            req.requestFrame = new Request_40Frame();
            return new RequestWrapper(req).getRequestArray();
        }

        public override IMetaModel handleResponse(IResponse response)
        {
            Response resp = (Response)response;
            HandsShakeMetaModel mModel = null;
            if (resp.responseFrame is Response_40Frame)
            {
                SondeInfoDataModel model = SondeInfoParser.parse((Response_40Frame)resp.responseFrame);
                mModel = new HandsShakeMetaModel();
                if (model != null)
                {
                    mModel.type = HandsShakeMetaModel.TYPE.SONDE_INFO;
                    mModel.Data = model;
                    FrameContext.slaveAddr = resp.responseFrame.Data[0];
                    //BasicService.currentSlaveAddr = resp.responseFrame.Data[0];
                }
                else
                {
                    mModel.type = HandsShakeMetaModel.TYPE.INIT_INFO;
                    mModel.Data = null;
                }
            }
            
            return mModel;
        }
    }
}
