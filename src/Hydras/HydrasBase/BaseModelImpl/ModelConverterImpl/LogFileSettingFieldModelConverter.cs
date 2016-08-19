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
    public class LogFileSettingFieldModelConverter : IModelConverter
    {
        private static LogFileSettingFieldModelConverter instance = null;

        public static LogFileSettingFieldModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new LogFileSettingFieldModelConverter();
            }
            return instance;
        }

        private LogFileSettingFieldModelConverter()
        {

        }

        private static LogFileSettingFieldDataModel genDataModel(LogFileSettingFieldViewModel vModel)
        {
            LogFileSettingFieldDataModel model = new LogFileSettingFieldDataModel();
            model.countFormat1 = StringByteConverter.stringToByteArray(vModel.CountFormat1);
            model.countFormatAndScope = StringByteConverter.stringToByteArray(vModel.CountFormatAndScope);
            model.prompt = StringByteConverter.stringToByteArray(vModel.Prompt);
            model.settingContent = StringByteConverter.stringToByteArray(vModel.SettingContent);
            model.settingName = StringByteConverter.stringToByteArray(vModel.SettingName);
            model.settingNameForShort = StringByteConverter.stringToByteArray(vModel.SettingNameForShort);
            model.sn = vModel.Sn;
            model.settingValue = (byte[])vModel.SettingValue;
            //model.settingValue = genDataSettingValue(vModel.Sn, vModel.SettingValue);

            return model;
        }

        private static byte[] genDataSettingValue(byte code, Object value)
        {
            byte[] result = null;
            switch (code)
            {
                case 0x01:
                case 0x02:
                case 0x03:
                case 0x04:
                case 0x05:
                    result = DateTimeByteConverter.getSecondsByte((DateTime)value);
                    break;
                case 0x06:
                    result = (byte[])value;
                    break;
            }
            return result;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            LogFileSettingFieldViewModel LVModel = vModel as LogFileSettingFieldViewModel;
            LogFileSettingFieldDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static LogFileSettingFieldViewModel genViewModel(LogFileSettingFieldDataModel model)
        {
            LogFileSettingFieldViewModel vModel = new LogFileSettingFieldViewModel();
            vModel.CountFormat1 = StringByteConverter.byteArrayToString(model.countFormat1);
            vModel.CountFormatAndScope = StringByteConverter.byteArrayToString(model.countFormatAndScope);
            vModel.Prompt = StringByteConverter.byteArrayToString(model.prompt);
            vModel.SettingContent = StringByteConverter.byteArrayToString(model.settingContent);
            vModel.SettingName = StringByteConverter.byteArrayToString(model.settingName);
            vModel.SettingNameForShort = StringByteConverter.byteArrayToString(model.settingNameForShort);
            vModel.Sn = model.sn;
            //vModel.SettingValue = model.settingValue;
            vModel.SettingValue = genViewSettingValue(model.sn, model.settingValue);

            return vModel;
        }

        private static Object genViewSettingValue(byte code, byte[] data)
        {
            Object result = null;
            switch (code)
            {
                case 0x01:
                case 0x02:
                case 0x03:
                case 0x04:
                case 0x05:
                     byte[] temp = new byte[4];
                     System.Array.Copy(data, temp, 4);
                     result = DateTimeByteConverter.getDateTime(temp);
                     break;
                case 0x06:
                     result = data;
                     break;
            }
            return result;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            LogFileSettingFieldDataModel model = dModel as LogFileSettingFieldDataModel;
            LogFileSettingFieldViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}