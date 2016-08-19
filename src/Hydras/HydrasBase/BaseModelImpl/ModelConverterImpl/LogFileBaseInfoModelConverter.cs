using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.Util;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasProtocol.StringMap;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class LogFileBaseInfoModelConverter : IModelConverter
    {
        private static LogFileBaseInfoModelConverter instance = null;

        public static LogFileBaseInfoModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new LogFileBaseInfoModelConverter();
            }
            return instance;
        }

        private LogFileBaseInfoModelConverter()
        {

        }

        private static LogFileBaseInfoDataModel genDataModel(LogFileBaseInfoViewModel vModel)
        {
            LogFileBaseInfoDataModel model = new LogFileBaseInfoDataModel();
            model.searchNum = vModel.LogNum;
            model.autoLogState = vModel.AutoLogState;
            model.logFileName = StringByteConverter.stringToByteArray(vModel.LogFileName);
            model.logNum = vModel.SearchNum;
            model.startTime = DateTimeByteConverter.getSecondsByte(vModel.StartTime);
            model.parasNum = vModel.ParasNum;
            model.settingFieldsNum = vModel.SettingFieldsNum;

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            LogFileBaseInfoViewModel LVModel = vModel as LogFileBaseInfoViewModel;
            LogFileBaseInfoDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static LogFileBaseInfoViewModel genViewModel(LogFileBaseInfoDataModel model)
        {
            LogFileBaseInfoViewModel vModel = new LogFileBaseInfoViewModel();
            vModel.LogNum = model.logNum;
            vModel.AutoLogState = model.autoLogState;
            vModel.LogFileName = StringByteConverter.byteArrayToString(model.logFileName);
            vModel.SearchNum = model.searchNum;
            vModel.StartTime = DateTimeByteConverter.getDateTime(model.startTime);
            vModel.ParasNum = model.parasNum;
            vModel.SettingFieldsNum = model.settingFieldsNum;
            vModel.Size_bytes = BitConverter.ToUInt32(model.size_bytes, 0);
            vModel.Size_scans = BitConverter.ToUInt32(model.size_scans, 0);

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            LogFileBaseInfoDataModel model = dModel as LogFileBaseInfoDataModel;
            LogFileBaseInfoViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}

