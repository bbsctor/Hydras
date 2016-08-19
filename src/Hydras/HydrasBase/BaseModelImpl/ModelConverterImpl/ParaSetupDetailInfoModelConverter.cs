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
    public class ParaSetupDetailInfoModelConverter : IModelConverter
    {
        private static ParaSetupDetailInfoModelConverter instance = null;

        public static ParaSetupDetailInfoModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new ParaSetupDetailInfoModelConverter();
            }
            return instance;
        }

        private ParaSetupDetailInfoModelConverter()
        {

        }

        private static ParameterSetupDetailInfoDataModel genDataModel(ParameterSetupDetailInfoViewModel vModel)
        {
            ParameterSetupDetailInfoDataModel model = new ParameterSetupDetailInfoDataModel();

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            ParameterSetupDetailInfoViewModel LVModel = vModel as ParameterSetupDetailInfoViewModel;
            ParameterSetupDetailInfoDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static ParameterSetupDetailInfoViewModel genViewModel(ParameterSetupDetailInfoDataModel model)
        {
            ParameterSetupDetailInfoViewModel vModel = new ParameterSetupDetailInfoViewModel();
            vModel.CalFormat1 = StringByteConverter.byteArrayToString(model.calFormat1);
            vModel.CalFormatAndScope = StringByteConverter.byteArrayToString(model.calFormatAndScope);
            vModel.ParaCode1 = model.paraCode1;
            vModel.ParaCode2 = model.paraCode2;
            vModel.SettingValue = model.settingValue;
            vModel.Prompt = StringByteConverter.byteArrayToString(model.prompt);
            vModel.SettingName = StringByteConverter.byteArrayToString(model.settingName1)
                //+ StringByteConverter.byteArrayToString(model.settingName2)
                + StringByteConverter.byteArrayToString(model.settingContent); 

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            ParameterSetupDetailInfoDataModel model = dModel as ParameterSetupDetailInfoDataModel;
            ParameterSetupDetailInfoViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}
