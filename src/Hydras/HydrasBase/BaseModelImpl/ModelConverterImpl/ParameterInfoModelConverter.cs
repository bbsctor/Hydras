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
    public class ParameterInfoModelConverter : IModelConverter
    {
        private static ParameterInfoModelConverter instance = null;

        public static ParameterInfoModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new ParameterInfoModelConverter();
            }
            return instance;
        }

        private ParameterInfoModelConverter()
        {

        }

        private static ParameterInfoDataModel genDataModel(ParameterInfoViewModel vModel)
        {
            ParameterInfoDataModel model = new ParameterInfoDataModel();

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            ParameterInfoViewModel LVModel = vModel as ParameterInfoViewModel;
            ParameterInfoDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static ParameterInfoViewModel genViewModel(ParameterInfoDataModel model)
        {
            ParameterInfoViewModel vModel = new ParameterInfoViewModel();
            vModel.CalFormat1 = StringByteConverter.byteArrayToString(model.calFormat1);
            vModel.CalFormat2 = StringByteConverter.byteArrayToString(model.calFormat2);
            vModel.CalUnit = StringByteConverter.byteArrayToString(model.calUnit);
            vModel.Sn = model.sn;
            vModel.ParaCode = model.paraCode;
            vModel.ParaName = StringByteConverter.byteArrayToString(model.paraName);
            vModel.ParaNameForShort = StringByteConverter.byteArrayToString(model.paraNameForShort);
            //vModel.FormatAndScope = StringByteConverter.byteArrayToString(model.formatAndScope);

            vModel.Format = StringByteConverter.byteArrayToString(model.formatAndScope);
            if (model.formatAndScope[2] == 0x00)
            {
                vModel.ScopeLow = new byte[4];
                vModel.ScopeHigh = new byte[4];
                int a;
                System.Array.Copy(model.formatAndScope, 3, vModel.ScopeLow, 0, 4);
                a = BitConverter.ToInt32(vModel.ScopeLow, 0);
                System.Array.Copy(model.formatAndScope, 8, vModel.ScopeHigh, 0, 4);
                a = BitConverter.ToInt32(vModel.ScopeHigh, 0);
            }
            else
            {
                vModel.ScopeLow = new byte[4];
                vModel.ScopeHigh = new byte[4];
                System.Array.Copy(model.formatAndScope, 4, vModel.ScopeLow, 0, 4);
                float a = BitConverter.ToSingle(vModel.ScopeLow, 0);
                System.Array.Copy(model.formatAndScope, 9, vModel.ScopeHigh, 0, 4);
                a = BitConverter.ToSingle(vModel.ScopeHigh, 0);
            }

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            ParameterInfoDataModel model = dModel as ParameterInfoDataModel;
            ParameterInfoViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}

