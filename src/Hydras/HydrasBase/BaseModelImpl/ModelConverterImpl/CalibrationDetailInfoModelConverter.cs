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
    public class CalibrationDetailInfoModelConverter : IModelConverter
    {
        private static CalibrationDetailInfoModelConverter instance = null;

        public static CalibrationDetailInfoModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new CalibrationDetailInfoModelConverter();
            }
            return instance;
        }

        private CalibrationDetailInfoModelConverter()
        {

        }

        private static CalibrationDetailInfoDataModel genDataModel(CalibrationDetailInfoViewModel vModel)
        {
            CalibrationDetailInfoDataModel model = new CalibrationDetailInfoDataModel();

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            CalibrationDetailInfoViewModel LVModel = vModel as CalibrationDetailInfoViewModel;
            CalibrationDetailInfoDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static CalibrationDetailInfoViewModel genViewModel(CalibrationDetailInfoDataModel model)
        {
            CalibrationDetailInfoViewModel vModel = new CalibrationDetailInfoViewModel();
            vModel.CountFormat1 = StringByteConverter.byteArrayToString(model.countFormat1);
            vModel.CountFormat2 = StringByteConverter.byteArrayToString(model.countFormat2);
            vModel.ParaCode1 = model.paraCode1;
            vModel.ParaCode2 = model.paraCode2;
            vModel.ParaName = StringByteConverter.byteArrayToString(model.paraName);
            vModel.ParaNameForShort = StringByteConverter.byteArrayToString(model.paraNameForShort);
            vModel.ParaUnit = StringByteConverter.byteArrayToString(model.paraUnit);
            //vModel.ParaValue = StringByteConverter.byteArrayToString(model.paraValue);
            vModel.ParaValue = BitConverter.ToSingle(model.paraValue, 0).ToString();
            vModel.Prompt = StringByteConverter.byteArrayToString(model.prompt);
            vModel.Scope = StringByteConverter.byteArrayToString(model.scope);

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            CalibrationDetailInfoDataModel model = dModel as CalibrationDetailInfoDataModel;
            CalibrationDetailInfoViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}

