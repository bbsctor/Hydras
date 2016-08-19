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
    public class SecurityLevelModelConverter : IModelConverter
    {
        private static SecurityLevelModelConverter instance = null;

        public static SecurityLevelModelConverter getInstance()
        {
            if (instance == null)
            {
                instance = new SecurityLevelModelConverter();
            }
            return instance;
        }

        private SecurityLevelModelConverter()
        {

        }

        private static SecurityLevelDataModel genDataModel(SecurityLevelViewModel vModel)
        {
            SecurityLevelDataModel model = new SecurityLevelDataModel();
            model.level1Pwd = StringByteConverter.stringToByteArray(vModel.level1Pwd);
            model.level2Pwd = StringByteConverter.stringToByteArray(vModel.level2Pwd);
            model.level3Pwd = StringByteConverter.stringToByteArray(vModel.level3Pwd);

            return model;
        }

        public IDataModel genDataModel(IViewModel vModel)
        {
            SecurityLevelViewModel LVModel = vModel as SecurityLevelViewModel;
            SecurityLevelDataModel model = null;
            if (LVModel != null)
            {
                model = genDataModel(LVModel);
            }
            return model;
        }
        private static SecurityLevelViewModel genViewModel(SecurityLevelDataModel model)
        {
            SecurityLevelViewModel vModel = new SecurityLevelViewModel();
            vModel.level1Pwd = StringByteConverter.byteArrayToString(model.level1Pwd);
            vModel.level2Pwd = StringByteConverter.byteArrayToString(model.level2Pwd);
            vModel.level3Pwd = StringByteConverter.byteArrayToString(model.level3Pwd);

            return vModel;
        }

        public IViewModel genViewModel(IDataModel dModel)
        {
            SecurityLevelDataModel model = dModel as SecurityLevelDataModel;
            SecurityLevelViewModel vModel = null;
            if (model != null)
            {
                vModel = genViewModel(model);
            }
            return vModel;
        }
    }
}
