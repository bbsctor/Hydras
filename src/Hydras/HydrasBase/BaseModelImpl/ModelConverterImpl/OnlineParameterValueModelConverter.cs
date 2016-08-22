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
    public class OnlineParameterValueModelConverter : IModelConverter
    {
        private static OnlineParameterValueModelConverter instance = null;

        public static OnlineParameterValueModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new OnlineParameterValueModelConverter();
            }
            return instance;
        }

        private OnlineParameterValueModelConverter()
        {

        }

        private static OnlineParameterValueDataModel genDataModel(OnlineParameterValueViewModel vModel)
        {
            OnlineParameterValueDataModel model = new OnlineParameterValueDataModel();
            

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            OnlineParameterValueViewModel LVModel = vModel as OnlineParameterValueViewModel;
            OnlineParameterValueDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static OnlineParameterValueViewModel genViewModel(OnlineParameterValueDataModel model)
        {
            OnlineParameterValueViewModel vModel = new OnlineParameterValueViewModel();
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
                vModel.Values.Add(temp);
            }

                return vModel;
        }

        private static OnlineParameterValueViewModel genViewModel(OnlineParameterValueDataModel model, ParameterInfoListViewModel formatModel)
        {
            OnlineParameterValueViewModel vModel = new OnlineParameterValueViewModel();
            string temp;
            string format;
            for (int i = 0; i < model.values.Count; i++)
            {
                if (i == 0)
                {
                    temp = DateTimeByteConverter.getDateTime(model.values[i]).ToString();
                }
                else
                {
                    format = formatModel[i].CalFormat1;
                    format = format.Substring(format.Length - 2, 1);
                    int bitLen;
                    int.TryParse(format, out bitLen);
                    float value = BitConverter.ToSingle(model.values[i], 0);
                    temp = Math.Round(value, bitLen).ToString();
                }
                vModel.Values.Add(temp);
            }

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            OnlineParameterValueDataModel model = dModel as OnlineParameterValueDataModel;
            OnlineParameterValueViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel, ParameterInfoListViewModel formatModel)
        {
            OnlineParameterValueDataModel model = dModel as OnlineParameterValueDataModel;
            OnlineParameterValueViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel((OnlineParameterValueDataModel)model, formatModel);
            }
            return vModel;
        }
    }
}

