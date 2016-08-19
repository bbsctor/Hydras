using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasBase.AppInterfaceImpl.BasicController;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class LogFileSettingFieldController : HydrasBasicController
    {
        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();

            if (action.Contains("readLogFileSettingField") == true)
            {
                byte para;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para);
                readLogFileSettingField(para);
            }
            else if (action.Contains("updateLogFileSettingField"))
            {
                int para1;
                int.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                updateLogFileSettingField(para1);
            }
        }

        private void updateLogFileSettingField(int para1)
        {
            for (int i = 0; i < 6; i++)
            {
                //para1 logNum
                //para2 settingNameForShort
                //para3 settingContent
                //para4 settingValue
                LogFileSettingFieldDataModel dModel = ((LogFileSettingFieldListDataModel)(model)).
                    getElementBySn((byte)(i + 1));
                string para2 = BitConverter.ToString(dModel.settingNameForShort);
                string para3 = BitConverter.ToString(dModel.settingContent);
                string para4 = BitConverter.ToString(dModel.settingValue);
                string action = base.genAction("updateLogFileSettingField",
                    new string[] { para1.ToString(), para2, para3, para4});
                base.execute(action);
            }
        }

        private void readLogFileSettingField(byte para)
        {
            
            for (int i = 0; i < 6; i++)
            {
                string action = genAction("readLogFileSettingField", 
                    new string[] {BitConverter.ToString(new byte[]{para}), (i + 1).ToString() });
                base.execute(action);
            }
            
        }

        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                LogFileMetaModel cModel = mModel as LogFileMetaModel;
                if (cModel != null)
                {
                    LogFileSettingFieldDataModel sondePara = (LogFileSettingFieldDataModel)cModel.Data;
                    switch (cModel.type)
                    {
                        case LogFileMetaModel.TYPE.SETTING_FIELD:
                            ((LogFileSettingFieldListDataModel)model).updateElement(sondePara);
                            break;
                    }
                }
            }
        }
    }
}

