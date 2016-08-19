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
    public class LogFileParameterInfoModelConverter : IModelConverter
    {
        private static LogFileParameterInfoModelConverter instance = null;

        public static LogFileParameterInfoModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new LogFileParameterInfoModelConverter();
            }
            return instance;
        }

        private LogFileParameterInfoModelConverter()
        {

        }

        private static LogFileParameterInfoDataModel genDataModel(LogFileParameterInfoViewModel vModel)
        {
            LogFileParameterInfoDataModel model = new LogFileParameterInfoDataModel();
            model.calFormat1 = StringByteConverter.stringToByteArray(vModel.CalFormat1);
            model.calFormat2 = StringByteConverter.stringToByteArray(vModel.CalFormat2);
            model.calUnit = StringByteConverter.stringToByteArray(vModel.CalUnit);
            model.sn = vModel.Sn;
            //model.formatAndScope = StringByteConverter.stringToByteArray(vModel.FormatAndScope);
            byte[] temp1 = StringByteConverter.stringToByteArray(vModel.Format);
            byte[] temp2 = vModel.ScopeLow;
            temp1 = temp1.Concat(new byte[] { 0x00 }).ToArray().Concat(temp2).ToArray();
            temp2 = vModel.ScopeHigh;
            temp1 = temp1.Concat(new byte[] { 0x00 }).ToArray().Concat(temp2).ToArray();

            model.formatAndScope = temp1;

            model.paraCode = vModel.ParaCode;
            model.paraName = StringByteConverter.stringToByteArray(vModel.ParaName);
            model.paraNameForShort = StringByteConverter.stringToByteArray(vModel.ParaNameForShort);

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            LogFileParameterInfoViewModel LVModel = vModel as LogFileParameterInfoViewModel;
            LogFileParameterInfoDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static LogFileParameterInfoViewModel genViewModel(LogFileParameterInfoDataModel model)
        {
            LogFileParameterInfoViewModel vModel = new LogFileParameterInfoViewModel();
            vModel.CalFormat1 = StringByteConverter.byteArrayToString(model.calFormat1);
            vModel.CalFormat2 = StringByteConverter.byteArrayToString(model.calFormat2);
            vModel.CalUnit = StringByteConverter.byteArrayToString(model.calUnit);
            vModel.Sn = model.sn;
            //vModel.FormatAndScope = StringByteConverter.byteArrayToString(model.formatAndScope);
            vModel.Format = StringByteConverter.byteArrayToString(model.formatAndScope);
            if (model.formatAndScope[2] == 0x00)
            {
                vModel.ScopeLow = new byte[4];
                vModel.ScopeHigh = new byte[4];
                System.Array.Copy(model.formatAndScope, 3, vModel.ScopeLow, 0, 4);
                System.Array.Copy(model.formatAndScope, 8, vModel.ScopeHigh, 0, 4);
            }
            else
            {
                vModel.ScopeLow = new byte[4];
                vModel.ScopeHigh = new byte[4];
                System.Array.Copy(model.formatAndScope, 4, vModel.ScopeLow, 0, 4);
                System.Array.Copy(model.formatAndScope, 9, vModel.ScopeHigh, 0, 4);
            }
            
            vModel.ParaCode = model.paraCode;
            vModel.ParaName = StringByteConverter.byteArrayToString(model.paraName);
            vModel.ParaNameForShort = StringByteConverter.byteArrayToString(model.paraNameForShort);

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            LogFileParameterInfoDataModel model = dModel as LogFileParameterInfoDataModel;
            LogFileParameterInfoViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}