using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class LogFileSettingFieldListModelConverter : IModelConverter
    {
        private static LogFileSettingFieldListModelConverter instance = null;

        public static LogFileSettingFieldListModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new LogFileSettingFieldListModelConverter();
            }
            return instance;
        }

        private LogFileSettingFieldListModelConverter()
        {

        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            LogFileSettingFieldListViewModel LVModel = vModel as LogFileSettingFieldListViewModel;
            LogFileSettingFieldListDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            LogFileSettingFieldListViewModel vModel = null;
            LogFileSettingFieldListDataModel model = dModel as LogFileSettingFieldListDataModel;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        private static LogFileSettingFieldListDataModel genDataModel(LogFileSettingFieldListViewModel viewList)
        {
            LogFileSettingFieldListDataModel modelList = new LogFileSettingFieldListDataModel();
            return modelList;
        }

        private static LogFileSettingFieldListViewModel genViewModel(LogFileSettingFieldListDataModel sondeInfoList)
        {
            LogFileSettingFieldListViewModel viewList = new LogFileSettingFieldListViewModel();
            for (int i = 0; i < sondeInfoList.Count; i++)
            {
                viewList.Add((LogFileSettingFieldViewModel)(LogFileSettingFieldModelConverter.
                    getInstance().genViewModel(sondeInfoList[i])));
            }
            return viewList;
        }
    }
}
