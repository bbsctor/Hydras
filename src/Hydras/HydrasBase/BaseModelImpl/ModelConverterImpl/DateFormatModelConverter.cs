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
    public class DateFormatModelConverter : IModelConverter
    {
        private static DateFormatModelConverter instance = null;

        public static DateFormatModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new DateFormatModelConverter();
            }
            return instance;
        }

        private DateFormatModelConverter()
        {

        }

        private static DateFormatDataModel genDataModel(DateFormatViewModel vModel)
        {
            DateFormatDataModel model = new DateFormatDataModel();
            switch (vModel.DateFormat)
            {
                case "MMDDYY":
                    model.format = 0x00;
                    break;
                case "DDMMYY":
                    model.format = 0x01;
                    break;
                case "YYMMDD":
                    model.format = 0x02;
                    break;
            }
            model.useDelimiter = vModel.UseDelimiter;

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            DateFormatViewModel LVModel = vModel as DateFormatViewModel;
            DateFormatDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static DateFormatViewModel genViewModel(DateFormatDataModel model)
        {
            DateFormatViewModel vModel = new DateFormatViewModel();
            switch (model.format)
            {
                case 0x00:
                    vModel.DateFormat = "MMDDYY";
                    break;
                case 0x01:
                    vModel.DateFormat = "DDMMYY";
                    break;
                case 0x02:
                    vModel.DateFormat = "YYMMDD";
                    break;
            }
            vModel.UseDelimiter = model.useDelimiter;
            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            DateFormatDataModel model = dModel as DateFormatDataModel;
            DateFormatViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}
