using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class ParameterInfoListModelConverter : IModelConverter
    {
        private static ParameterInfoListModelConverter instance = null;

        public static ParameterInfoListModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new ParameterInfoListModelConverter();
            }
            return instance;
        }

        private ParameterInfoListModelConverter()
        {

        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            ParameterInfoListViewModel LVModel = vModel as ParameterInfoListViewModel;
            ParameterInfoListDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            ParameterInfoListViewModel vModel = null;
            ParameterInfoListDataModel model = dModel as ParameterInfoListDataModel;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        private static ParameterInfoListDataModel genDataModel(ParameterInfoListViewModel viewList)
        {
            ParameterInfoListDataModel modelList = new ParameterInfoListDataModel();
            return modelList;
        }

        private static ParameterInfoListViewModel genViewModel(ParameterInfoListDataModel sondeInfoList)
        {
            ParameterInfoListViewModel viewList = new ParameterInfoListViewModel();
            for (int i = 0; i < sondeInfoList.Count; i++)
            {
                viewList.Add((ParameterInfoViewModel)(ParameterInfoModelConverter.getInstance().genViewModel(sondeInfoList[i])));
            }
            return viewList;
        }
    }
}
