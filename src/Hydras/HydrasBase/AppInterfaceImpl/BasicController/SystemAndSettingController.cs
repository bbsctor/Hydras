using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.Util;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasProtocol;
using HydrasBase.ModelProtocolServiceImpl;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class SystemAndSettingController : HydrasBasicController
    {
        private int paraNum;

        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();

            if ("getSystemAndSetting".Equals(action))
            {
                readSystemAndSettingNum();
                readAllSystemAndSetting();
            }
            else if ("getDateTime".Equals(action))
            {
                readDateTime();
            }
            else if (action.Contains("setSystemAndSetting") == true)
            {
                byte para;
                byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para);
                setSystemAndSetting(para);
            }
            else if ("setID".Equals(action))
            {
                setID();
            }
            else if ("setDateTime".Equals(action))
            {
                setOthers(0x08);
            }
            else if ("setCirculator".Equals(action))
            {
                setOthers(0x01);
            }
            else if ("setAudio".Equals(action))
            {
                setOthers(0x02);
            }
            else if ("setCommunication".Equals(action))
            {
                setOthers(0x03);
                setOthers(0x04);
                setOthers(0x09);
                setOthers(0x06);
                setOthers(0x07);
            }
            else if ("setLogFiles".Equals(action))
            {
                setOthers(0x0B);
            }
            else if ("setAutoLog".Equals(action))
            {
                setOthers(0x0A);
            }
        }

        private void setID()
        {
            SystemAndSettingDataModel dModel = ((SystemAndSettingListDataModel)(model)).getElementByItemNum(0x05);
            string para1 = StringByteConverter.byteArrayToString(dModel.paraNameForShort);
            string para2 = StringByteConverter.byteArrayToString(dModel.settingType);
            string para3 = StringByteConverter.byteArrayToString(dModel.settingContent);
            string action = genAction("setID", new string[]{para1, para2, para3});
            base.execute(action);
        }

        private void setOthers(byte num)
        {
            SystemAndSettingDataModel dModel = ((SystemAndSettingListDataModel)(model)).getElementByItemNum(num);
            string para1 = StringByteConverter.byteArrayToString(dModel.paraNameForShort);
            string para2 = StringByteConverter.byteArrayToString(dModel.settingType);
            string para3 = BitConverter.ToString(dModel.settingContent);
            string action = genAction("setOthers", new string[] { para1, para2, para3 });
            base.execute(action);
        }

        private void setSystemAndSetting(byte itemNum)
        {
            SystemAndSettingDataModel dModel = (SystemAndSettingDataModel)(((SystemAndSettingListDataModel)model)
                .getElementByItemNum(itemNum));
            string settingContent = StringByteConverter.byteArrayToString(dModel.settingContent);
            string prompt = StringByteConverter.byteArrayToString(dModel.prompt);
            string action = ActionStrAssistant.buildActionStr("setSystemAndSetting",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", settingContent),
                        new ActionStrAssistant.ParameterValue("para2", prompt)
                    });
            base.execute(action);
        }
        private void readSystemAndSettingNum()
        {
            string action = ActionStrAssistant.buildActionStr("getSystemAndSetting",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", 0 + "")
                    });
            base.execute(action);
        }

        private void readAllSystemAndSetting()
        {
            for (int i = 0; i < paraNum; i++)
            {
                string action = ActionStrAssistant.buildActionStr("getSystemAndSetting",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", i + 1 + "")
                    });
                base.execute(action);
            }
        }

        private void readDateTime()
        {
            string action = ActionStrAssistant.buildActionStr("getSystemAndSetting",
                new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                {
                    new ActionStrAssistant.ParameterValue("para1", 8 + "")
                });
            base.execute(action);
        }

        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                SystemAndSettingMetaModel cModel = mModel as SystemAndSettingMetaModel;
                if (cModel != null)
                {
                    SystemAndSettingDataModel sondePara = (SystemAndSettingDataModel)cModel.Data;
                    switch (cModel.type)
                    {
                        case SystemAndSettingMetaModel.TYPE.PARAMETER_NUM:
                            this.paraNum = (int)sondePara.paraNum;
                            break;
                        case SystemAndSettingMetaModel.TYPE.SETTING_PARAMETER:
                            ((SystemAndSettingListDataModel)model).updateElement(sondePara);
                            if (sondePara.paraCode1 == 0x06)
                            {
                                FrameContext.slaveAddr = sondePara.settingContent[0];
                                //BasicService.currentSlaveAddr = sondePara.settingContent[0];
                                //BasicService.addSlaveAddr((SerialPortModel)this.CommService.ConnectParameterModel,
                                //    sondePara.settingContent[0]);
                            }
                            break;
                    }
                }
            }
        }
    }
}
