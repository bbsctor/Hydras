using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasBase.BaseModelImpl.ModelConverterImpl
{
    public class SystemAndSettingListModelConverter : IModelConverter
    {
        private static SystemAndSettingListModelConverter instance = null;

        public static SystemAndSettingListModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new SystemAndSettingListModelConverter();
            }
            return instance;
        }

        private SystemAndSettingListModelConverter()
        {

        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            SystemAndSettingListViewModel LVModel = vModel as SystemAndSettingListViewModel;
            SystemAndSettingListDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            SystemAndSettingListViewModel vModel = null;
            SystemAndSettingListDataModel model = dModel as SystemAndSettingListDataModel;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }

        private static SystemAndSettingListDataModel genDataModel(SystemAndSettingListViewModel viewList)
        {
            SystemAndSettingListDataModel modelList = new SystemAndSettingListDataModel();
            for (int i = 0; i < viewList.Count; i++)
            {
                modelList.Add((SystemAndSettingDataModel)SystemAndSettingModelConverter.getInstance()
                    .genDataModel(viewList[i]));
            }
            return modelList;
        }

        private static SystemAndSettingListViewModel genViewModel(SystemAndSettingListDataModel sondeInfoList)
        {
            SystemAndSettingListViewModel viewList = new SystemAndSettingListViewModel();
            for (int i = 0; i < sondeInfoList.Count; i++)
            {
                viewList.Add((SystemAndSettingViewModel)(SystemAndSettingModelConverter
                    .getInstance().genViewModel(sondeInfoList[i])));
            }
            return viewList;
        }
    }
}
