using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.Util;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class SondeInfoModelConverter : IModelConverter
    {
        private static SondeInfoModelConverter instance = null;

        public static SondeInfoModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new SondeInfoModelConverter();
            }
            return instance;
        }

        private SondeInfoModelConverter()
        {

        }

        private static SondeInfoDataModel genDataModel(SondeInfoViewModel vModel)
        {
            SondeInfoDataModel model = new SondeInfoDataModel();
            model.portModel = vModel.PortModel;
            model.manufacturer = StringByteConverter.stringToByteArray(vModel.Manufacturer);
            model.serialNum = StringByteConverter.stringToByteArray(vModel.SerialNum);
            model.model = StringByteConverter.stringToByteArray(vModel.Model);
            model.softwareVersion = StringByteConverter.stringToByteArray(vModel.SoftwareVersion);
            model.modbusVersion = StringByteConverter.stringToByteArray(vModel.ModbusVersion);
            model.manufactureDate = DateTimeByteConverter.getSecondsByte(vModel.ManufactureDate);

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            SondeInfoViewModel LVModel = vModel as SondeInfoViewModel;
            SondeInfoDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static SondeInfoViewModel genViewModel(SondeInfoDataModel model)
        {
            SondeInfoViewModel vModel = new SondeInfoViewModel();
            vModel.PortModel = model.portModel;
            vModel.Manufacturer = StringByteConverter.byteArrayToString(model.manufacturer);
            vModel.SerialNum = StringByteConverter.byteArrayToString(model.serialNum);
            vModel.Model = StringByteConverter.byteArrayToString(model.model);
            vModel.SoftwareVersion = StringByteConverter.byteArrayToString(model.softwareVersion);
            vModel.ModbusVersion = StringByteConverter.byteArrayToString(model.modbusVersion);
            //System.Array.Reverse(model.manufactureDate);
            vModel.ManufactureDate = DateTimeByteConverter.getDateTime(model.manufactureDate);

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            SondeInfoDataModel model = dModel as SondeInfoDataModel;
            SondeInfoViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}
