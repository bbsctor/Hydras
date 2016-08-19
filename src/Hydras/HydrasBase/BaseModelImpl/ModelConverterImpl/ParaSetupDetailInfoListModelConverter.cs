using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class ParaSetupDetailInfoListModelConverter : IModelConverter
    {
        private static ParaSetupDetailInfoListModelConverter instance = null;

        public static ParaSetupDetailInfoListModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new ParaSetupDetailInfoListModelConverter();
            }
            return instance;
        }

        private ParaSetupDetailInfoListModelConverter()
        {

        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            ParameterSetupDetailInfoListViewModel LVModel = vModel as ParameterSetupDetailInfoListViewModel;
            ParameterSetupDetailInfoListDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            ParameterSetupDetailInfoListViewModel vModel = null;
            ParameterSetupDetailInfoListDataModel model = dModel as ParameterSetupDetailInfoListDataModel;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        private static ParameterSetupDetailInfoListDataModel genDataModel(ParameterSetupDetailInfoListViewModel viewList)
        {
            ParameterSetupDetailInfoListDataModel modelList = new ParameterSetupDetailInfoListDataModel();
            return modelList;
        }

        private static ParameterSetupDetailInfoListViewModel genViewModel(ParameterSetupDetailInfoListDataModel sondeInfoList)
        {
            ParameterSetupDetailInfoListViewModel viewList = new ParameterSetupDetailInfoListViewModel();
            for (int i = 0; i < sondeInfoList.Count; i++)
            {
                viewList.Add((ParameterSetupDetailInfoViewModel)(ParaSetupDetailInfoModelConverter.getInstance().genViewModel(sondeInfoList[i])));
            }
            return viewList;
        }
    }
}

