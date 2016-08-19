using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class SondeDetailDataModel : BasicModel, IDataModel
    {
        public List<IModel> modelList;

        public CalibrationBaseInfoListDataModel calibrationBaseInfoListDataModel;
        public CalibrationDetailInfoListDataModel calibrationDetailInfoListDataModel;
        public DateFormatDataModel dateFormatDataModel;
        public DeviceStorageDataModel deviceStorageDataModel;
        public LogFileBaseInfoListDataModel logFileBaseInfoListDataModel;
        public LogFileParameterInfoListDataModel logFileParameterInfoListDataModel;
        public LogFileParameterValueListDataModel logFileParameterValueListDataModel;
        public LogFileSettingFieldListDataModel logFileSettingFieldListDataModel;
        public OnlineParameterValueDataModel onlineParameterValueDataModel;
        public ParameterInfoListDataModel parameterInfoListDataModel;
        public ParameterSetupBaseInfoListDataModel parameterSetupBaseInfoListDataModel;
        public ParameterSetupDetailInfoListDataModel parameterSetupDetailInfoListDataModel;
        public SDIParameterDataModel sdiParameterDataModel;
        public SecurityLevelDataModel securityLevelDataModel;
        public SystemAndSettingListDataModel systemAndSettingListDataModel;

        public SondeDetailDataModel()
        {
            modelList = new List<IModel>();
            calibrationBaseInfoListDataModel = new CalibrationBaseInfoListDataModel();
            calibrationDetailInfoListDataModel = new CalibrationDetailInfoListDataModel();
            dateFormatDataModel = new DateFormatDataModel();
            deviceStorageDataModel = new DeviceStorageDataModel();
            logFileBaseInfoListDataModel = new LogFileBaseInfoListDataModel();
            logFileParameterInfoListDataModel = new LogFileParameterInfoListDataModel();
            logFileParameterValueListDataModel = new LogFileParameterValueListDataModel();
            logFileSettingFieldListDataModel = new LogFileSettingFieldListDataModel();
            onlineParameterValueDataModel = new OnlineParameterValueDataModel();
            parameterInfoListDataModel = new ParameterInfoListDataModel();
            parameterSetupBaseInfoListDataModel = new ParameterSetupBaseInfoListDataModel();
            parameterSetupDetailInfoListDataModel = new ParameterSetupDetailInfoListDataModel();
            sdiParameterDataModel = new SDIParameterDataModel();
            securityLevelDataModel = new SecurityLevelDataModel();
            systemAndSettingListDataModel = new SystemAndSettingListDataModel();

            modelList.Add(calibrationBaseInfoListDataModel);
            modelList.Add(calibrationDetailInfoListDataModel);
            modelList.Add(dateFormatDataModel);
            modelList.Add(deviceStorageDataModel);
            modelList.Add(logFileBaseInfoListDataModel);
            modelList.Add(logFileParameterInfoListDataModel);
            modelList.Add(logFileParameterValueListDataModel);
            modelList.Add(logFileSettingFieldListDataModel);
            modelList.Add(onlineParameterValueDataModel);
            modelList.Add(parameterInfoListDataModel);
            modelList.Add(parameterSetupBaseInfoListDataModel);
            modelList.Add(parameterSetupDetailInfoListDataModel);
            modelList.Add(sdiParameterDataModel);
            modelList.Add(securityLevelDataModel);
            modelList.Add(systemAndSettingListDataModel);
        }

        public IModel getModelByType(Type type)
        {
            for (int i = 0; i < modelList.Count; i++)
            {
                if (modelList[i].GetType() == type)
                {
                    return modelList[i];
                }
            }
            return null;
        }
    }
}
