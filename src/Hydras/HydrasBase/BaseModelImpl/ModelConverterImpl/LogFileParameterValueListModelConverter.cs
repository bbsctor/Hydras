using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class LogFileParameterValueListModelConverter : IModelConverter
    {
        private static LogFileParameterValueListModelConverter instance = null;

        public static LogFileParameterValueListModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new LogFileParameterValueListModelConverter();
            }
            return instance;
        }

        private LogFileParameterValueListModelConverter()
        {

        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            LogFileParameterValueListViewModel LVModel = vModel as LogFileParameterValueListViewModel;
            LogFileParameterValueListDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            LogFileParameterValueListViewModel vModel = null;
            LogFileParameterValueListDataModel model = dModel as LogFileParameterValueListDataModel;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        private static LogFileParameterValueListDataModel genDataModel(LogFileParameterValueListViewModel viewList)
        {
            LogFileParameterValueListDataModel modelList = new LogFileParameterValueListDataModel();
            for (int i = 0; i < viewList.Count; i++)
            {
                modelList.Add((LogFileParameterValueDataModel)(LogFileParameterValueModelConverter.
                    getInstance().genDataModel(viewList[i])));
            }
            return modelList;
        }

        private static LogFileParameterValueListViewModel genViewModel(LogFileParameterValueListDataModel sondeInfoList)
        {
            LogFileParameterValueListViewModel viewList = new LogFileParameterValueListViewModel();
            for (int i = 0; i < sondeInfoList.Count; i++)
            {
                viewList.Add((LogFileParameterValueViewModel)(LogFileParameterValueModelConverter.
                    getInstance().genViewModel(sondeInfoList[i])));
            }
            return viewList;
        }
    }
}


