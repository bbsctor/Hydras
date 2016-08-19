using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class SondeInfoListModelConverter : IModelConverter
    {
        private static SondeInfoListModelConverter instance = null;

        public static SondeInfoListModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new SondeInfoListModelConverter();
            }
            return instance;
        }

        private SondeInfoListModelConverter()
        {

        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            SondeInfoListViewModel LVModel = vModel as SondeInfoListViewModel;
            SondeInfoListDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            SondeInfoListViewModel vModel = null;
            SondeInfoListDataModel model = dModel as SondeInfoListDataModel;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        private static SondeInfoListDataModel genDataModel(SondeInfoListViewModel viewList)
        {
            SondeInfoListDataModel modelList = new SondeInfoListDataModel();
            for (int i = 0; i < viewList.Count; i++)
            {
                modelList.Add((SondeInfoDataModel)SondeInfoModelConverter.getInstance().genDataModel(viewList[i]));
            }
            return modelList;
        }

        private static SondeInfoListViewModel genViewModel(SondeInfoListDataModel sondeInfoList)
        {
            SondeInfoListViewModel viewList = new SondeInfoListViewModel();
            for (int i = 0; i < sondeInfoList.Count; i++)
            {
                viewList.Add((SondeInfoViewModel)(SondeInfoModelConverter.getInstance().genViewModel(sondeInfoList[i])));
            }
            return viewList;
        }
    }
}
