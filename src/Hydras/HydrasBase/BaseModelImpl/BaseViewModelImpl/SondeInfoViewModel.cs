using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class SondeInfoViewModel : BasicModel, IViewModel
    {
        private SerialPortModel portModel;

        public SerialPortModel PortModel
        {
            get { return portModel; }
            set { portModel = value; }
        }

        private string manufacturer;

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        private DateTime manufactureDate;

        public DateTime ManufactureDate
        {
            get { return manufactureDate; }
            set { manufactureDate = value; }
        }

        private string model;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        private string serialNum;

        public string SerialNum
        {
            get { return serialNum; }
            set { serialNum = value; }
        }
        private string softwareVersion;

        public string SoftwareVersion
        {
            get { return softwareVersion; }
            set { softwareVersion = value; }
        }
        private string modbusVersion;

        public string ModbusVersion
        {
            get { return modbusVersion; }
            set { modbusVersion = value; }
        }
    }
}
