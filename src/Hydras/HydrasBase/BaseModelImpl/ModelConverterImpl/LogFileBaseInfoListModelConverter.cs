using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class LogFileBaseInfoListModelConverter : IModelConverter
    {
        private static LogFileBaseInfoListModelConverter instance = null;

        public static LogFileBaseInfoListModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new LogFileBaseInfoListModelConverter();
            }
            return instance;
        }

        private LogFileBaseInfoListModelConverter()
        {

        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            LogFileBaseInfoListViewModel LVModel = vModel as LogFileBaseInfoListViewModel;
            LogFileBaseInfoListDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            LogFileBaseInfoListViewModel vModel = null;
            LogFileBaseInfoListDataModel model = dModel as LogFileBaseInfoListDataModel;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        private static LogFileBaseInfoListDataModel genDataModel(LogFileBaseInfoListViewModel viewList)
        {
            LogFileBaseInfoListDataModel modelList = new LogFileBaseInfoListDataModel();
            return modelList;
        }

        private static LogFileBaseInfoListViewModel genViewModel(LogFileBaseInfoListDataModel sondeInfoList)
        {
            LogFileBaseInfoListViewModel viewList = new LogFileBaseInfoListViewModel();
            for (int i = 0; i < sondeInfoList.Count; i++)
            {
                viewList.Add((LogFileBaseInfoViewModel)(LogFileBaseInfoModelConverter.
                    getInstance().genViewModel(sondeInfoList[i])));
            }
            return viewList;
        }
    }
}