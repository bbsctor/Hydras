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
    public class DeviceStorageModelConverter : IModelConverter
    {
        private static DeviceStorageModelConverter instance = null;

        public static DeviceStorageModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new DeviceStorageModelConverter();
            }
            return instance;
        }

        private DeviceStorageModelConverter()
        {

        }

        private static DeviceStorageDataModel genDataModel(DeviceStorageViewModel vModel)
        {
            DeviceStorageDataModel model = new DeviceStorageDataModel();

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            DeviceStorageViewModel LVModel = vModel as DeviceStorageViewModel;
            DeviceStorageDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static DeviceStorageViewModel genViewModel(DeviceStorageDataModel model)
        {
            DeviceStorageViewModel vModel = new DeviceStorageViewModel();
            vModel.bytesLeft = BitConverter.ToUInt32(model.bytesLeft, 0).ToString();
            vModel.daysLeft = BitConverter.ToSingle(model.daysLeft, 0).ToString();
            vModel.externalBatteryDaysLeft = BitConverter.ToSingle(model.externalBatteryDaysLeft, 0).ToString();
            vModel.externalBatteryLeft = BitConverter.ToSingle(model.externalBatteryLeft, 0).ToString();
            vModel.internalBatteryDaysLeft = BitConverter.ToSingle(model.internalBatteryDaysLeft, 0).ToString();
            vModel.internalBatteryLeft = BitConverter.ToSingle(model.internalBatteryLeft, 0).ToString();
            vModel.logFilesNum = ((uint)model.logFilesNum).ToString();
            vModel.maxLogFilesNum = ((uint)model.maxLogFilesNum).ToString();
            vModel.memoryAvailable = BitConverter.ToUInt32(model.memoryAvailable, 0).ToString();

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            DeviceStorageDataModel model = dModel as DeviceStorageDataModel;
            DeviceStorageViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}