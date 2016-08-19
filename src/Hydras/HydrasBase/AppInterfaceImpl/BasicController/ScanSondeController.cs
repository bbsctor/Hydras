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
using HydrasProtocol.ExceptionDefine;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class ScanSondeController : HandsShakeController
    {
        private LogFileController logFileController;

        private SerialPortModel currentPort;

        public SerialPortModel CurrentPort
        {
            get { return currentPort; }
            set { currentPort = value; }
        }

        public override void execute(string action)
        {
            resetDataModel();

            if ("scanSonde".Equals(action))
            {
                try
                    {
                        base.execute("handsShake");
                    }
                catch (AbnormalResponseException are)
                {
                    execute(action);
                }
            }
        }

        private void scanLogFile()
        {
            logFileController.execute("readAllLogFileBaseInfo");
        }

        public override void resetDataModel()
        {
            this.model = (SondeInfoListDataModel)ModelRepository.getModelByType(typeof(SondeInfoListDataModel));
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
                            ((SondeInfoDataModel)(mModel.Data)).portModel = currentPort;
                            SondeInfoDataModel sondeInfo = (SondeInfoDataModel)cModel.Data;
                            ((SondeInfoListDataModel)model).updateElement(sondeInfo);
                            break;
                    }
                }
            }
        }
    }
}