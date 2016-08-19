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
    public class SystemAndSettingModelConverter : IModelConverter
    {
        private static SystemAndSettingModelConverter instance = null;

        public static SystemAndSettingModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new SystemAndSettingModelConverter();
            }
            return instance;
        }

        private SystemAndSettingModelConverter()
        {

        }

        private static SystemAndSettingDataModel genDataModel(SystemAndSettingViewModel vModel)
        {
            SystemAndSettingDataModel model = new SystemAndSettingDataModel();
            model.paraCode1 = vModel.ParaCode1;
            model.settingContent = vModel.SettingContent;

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            SystemAndSettingViewModel LVModel = vModel as SystemAndSettingViewModel;
            SystemAndSettingDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static SystemAndSettingViewModel genViewModel(SystemAndSettingDataModel model)
        {
            SystemAndSettingViewModel vModel = new SystemAndSettingViewModel();
            vModel.ParaCode1 = model.paraCode1;
            vModel.ParaFormat = StringByteConverter.byteArrayToString(model.paraFormat);
            vModel.ParaFormatAndScope = StringByteConverter.byteArrayToString(model.paraFormatAndScope);
            vModel.ParaName = StringByteConverter.byteArrayToString(model.paraName);
            //vModel.ParaNameAndSettingType = StringByteConverter.byteArrayToString(model.paraNameAndSettingType);
            vModel.ParaNameForShort = StringByteConverter.byteArrayToString(model.paraNameForShort);
            vModel.SettingType = StringByteConverter.byteArrayToString(model.settingType);
            vModel.Prompt = StringByteConverter.byteArrayToString(model.prompt);
            vModel.SettingContent = model.settingContent;

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            SystemAndSettingDataModel model = dModel as SystemAndSettingDataModel;
            SystemAndSettingViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}
