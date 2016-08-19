using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class LogFileParameterInfoListModelConverter : IModelConverter
    {
        private static LogFileParameterInfoListModelConverter instance = null;

        public static LogFileParameterInfoListModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new LogFileParameterInfoListModelConverter();
            }
            return instance;
        }

        private LogFileParameterInfoListModelConverter()
        {

        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            LogFileParameterInfoListViewModel LVModel = vModel as LogFileParameterInfoListViewModel;
            LogFileParameterInfoListDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            LogFileParameterInfoListViewModel vModel = null;
            LogFileParameterInfoListDataModel model = dModel as LogFileParameterInfoListDataModel;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        private static LogFileParameterInfoListDataModel genDataModel(LogFileParameterInfoListViewModel viewList)
        {
            LogFileParameterInfoListDataModel modelList = new LogFileParameterInfoListDataModel();
            for (int i = 0; i < viewList.Count; i++)
            {
                modelList.Add((LogFileParameterInfoDataModel)(LogFileParameterInfoModelConverter.getInstance().genDataModel(viewList[i])));
            }
            return modelList;
        }

        private static LogFileParameterInfoListViewModel genViewModel(LogFileParameterInfoListDataModel sondeInfoList)
        {
            LogFileParameterInfoListViewModel viewList = new LogFileParameterInfoListViewModel();
            for (int i = 0; i < sondeInfoList.Count; i++)
            {
                viewList.Add((LogFileParameterInfoViewModel)(LogFileParameterInfoModelConverter.getInstance().genViewModel(sondeInfoList[i])));
            }
            return viewList;
        }
    }
}

