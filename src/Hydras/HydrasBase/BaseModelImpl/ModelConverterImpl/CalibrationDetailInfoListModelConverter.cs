using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class CalibrationDetailInfoListModelConverter : IModelConverter
    {
        private static CalibrationDetailInfoListModelConverter instance = null;

        public static CalibrationDetailInfoListModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new CalibrationDetailInfoListModelConverter();
            }
            return instance;
        }

        private CalibrationDetailInfoListModelConverter()
        {

        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            CalibrationDetailInfoListViewModel LVModel = vModel as CalibrationDetailInfoListViewModel;
            CalibrationDetailInfoListDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            CalibrationDetailInfoListViewModel vModel = null;
            CalibrationDetailInfoListDataModel model = dModel as CalibrationDetailInfoListDataModel;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        private static CalibrationDetailInfoListDataModel genDataModel(CalibrationDetailInfoListViewModel viewList)
        {
            CalibrationDetailInfoListDataModel modelList = new CalibrationDetailInfoListDataModel();
            return modelList;
        }

        private static CalibrationDetailInfoListViewModel genViewModel(CalibrationDetailInfoListDataModel sondeInfoList)
        {
            CalibrationDetailInfoListViewModel viewList = new CalibrationDetailInfoListViewModel();
            for (int i = 0; i < sondeInfoList.Count; i++)
            {
                viewList.Add((CalibrationDetailInfoViewModel)(CalibrationDetailInfoModelConverter.getInstance().genViewModel(sondeInfoList[i])));
            }
            return viewList;
        }
    }
}


