using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class SondeInfoDataModel : BasicModel, IDataModel
    {
        public SerialPortModel portModel;
        public byte[] manufacturer;
        public byte[] manufactureDate;
        public byte[] model;
        public byte[] serialNum;
        public byte[] softwareVersion;
        public byte[] modbusVersion;
        //public LogFileBaseInfoListDataModel logList;
        public SondeDetailDataModel sondeDetailDataModel;

        public SondeInfoDataModel()
        {
            //logList = new LogFileBaseInfoListDataModel();
            sondeDetailDataModel = new SondeDetailDataModel();
        }

        public override void update(IModel model)
        {
            update(model, new string[]{"manufacturer", "manufactureDate", "model", "serialNum",
                "softwareVersion", "modbusVersion"});
        }

        public void updateLoglist(LogFileBaseInfoListDataModel logList)
        {
            //this.logList = logList;
            this.sondeDetailDataModel.logFileBaseInfoListDataModel = logList;
        }
    }
}
