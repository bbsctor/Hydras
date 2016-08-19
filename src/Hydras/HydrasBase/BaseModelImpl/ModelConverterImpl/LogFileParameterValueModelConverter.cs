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
    public class LogFileParameterValueModelConverter : IModelConverter
    {
        private static LogFileParameterValueModelConverter instance = null;

        public static LogFileParameterValueModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new LogFileParameterValueModelConverter();
            }
            return instance;
        }

        private LogFileParameterValueModelConverter()
        {

        }

        private static LogFileParameterValueDataModel genDataModel(LogFileParameterValueViewModel vModel)
        {
            LogFileParameterValueDataModel model = new LogFileParameterValueDataModel();
            

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            LogFileParameterValueViewModel LVModel = vModel as LogFileParameterValueViewModel;
            LogFileParameterValueDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static LogFileParameterValueViewModel genViewModel(LogFileParameterValueDataModel model)
        {
            LogFileParameterValueViewModel vModel = new LogFileParameterValueViewModel();
            string temp;
            for (int i = 0; i < model.values.Count; i++)
            {
                if (i == 0)
                {
                    temp = DateTimeByteConverter.getDateTime(model.values[i]).ToString();
                }
                else
                {
                    temp = BitConverter.ToSingle(model.values[i], 0).ToString();
                }
                vModel.values.Add(temp);
            }

                return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            LogFileParameterValueDataModel model = dModel as LogFileParameterValueDataModel;
            LogFileParameterValueViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}
