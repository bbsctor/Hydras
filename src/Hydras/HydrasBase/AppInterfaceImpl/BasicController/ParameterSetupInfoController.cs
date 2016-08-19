using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;
using ConfigFrame.Util;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasBase.Util;
using HydrasProtocol.StringMap;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class ParameterSetupInfoController : HydrasBasicController
    {

        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();
            try
            {
                if ("getBaseInfo".Equals(action))
                {
                    readAllBaseInfo();
                }
                else if ("getDetailInfo".Equals(action))
                {
                    ParameterSetupBaseInfoListDataModel baseModelList =
                (ParameterSetupBaseInfoListDataModel)model;
                    for (int i = 0; i < baseModelList.Count; i++)
                    {
                        readDetailInfoByBaseNum(baseModelList[i].para1);
                    }
                }
                else if (action.Contains("setParameter"))
                {
                    byte para1;
                    byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                    setParameter(para1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                cService.disconnect();
            }
        }

        private void setParameter(byte paraCode)
        {
            ParameterSetupBaseInfoDataModel baseModel = 
                ((ParameterSetupBaseInfoListDataModel)model).getElementByBaseNum(paraCode);
            ParameterSetupDetailInfoListDataModel dListModel = baseModel.detailList;
            ParameterSetupDetailInfoDataModel dModel;
            SondeParameterMapper mapper =
                SondeParameterMapperContext.getParameterMapper((SerialPortModel)CommService.ConnectParameterModel);
            for (int i = 0; i < dListModel.Count; i++)
            {
                dModel = dListModel[i];
                string para1 = ((int)baseModel.para1).ToString();
                string para2 = StringByteConverter.byteArrayToString(dModel.settingName1);
                string para3 = StringByteConverter.byteArrayToString(dModel.settingContent);
                string para4 = ((int)dModel.settingValue).ToString();
                string action = genAction("setParameter", new string[] { para1, para2, para3, para4 });
                base.execute(action);
            }
            
        }

        private void readAllBaseInfo()
        {
            SondeParameterMapper mapper =
                SondeParameterMapperContext.getParameterMapper((SerialPortModel)CommService.ConnectParameterModel);
            for (int i = 0; i < mapper.elementList.Count; i++)
            {
                string action = ActionStrAssistant.buildActionStr("getBaseInfo", 
                    new ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", mapper.getCodeBySerialNum((byte)(i + 1)).ToString())
                    });
                base.execute(action);
            }
        }

        private void readDetailInfoByBaseNum(byte baseNum)
        {
            resetDataModel();
            ParameterSetupBaseInfoDataModel baseModel = ((ParameterSetupBaseInfoListDataModel)model).
                getElementByBaseNum(baseNum);
            if (baseModel != null)
            {
                int boxNum = (int)baseModel.para2;
                string action;
                for (int i = 0; i < boxNum; i++)
                {
                    action = ActionStrAssistant.buildActionStr("getDetailInfo",
                        new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", baseNum.ToString()),
                        new ActionStrAssistant.ParameterValue("para2", (i + 1).ToString())
                    });
                    base.execute(action);
                }
            }
        }

        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                ParameterSetupMetaModel cModel = mModel as ParameterSetupMetaModel;
                if (cModel != null)
                {
                    switch (cModel.type)
                    {
                        case ParameterSetupMetaModel.TYPE.BASE_INFO:
                            ParameterSetupBaseInfoDataModel baseInfo = (ParameterSetupBaseInfoDataModel)mModel.Data;
                            if (baseInfo.para2 > 0)
                            {
                                ((ParameterSetupBaseInfoListDataModel)model).updateElement(baseInfo);
                            }
                            break;
                        case ParameterSetupMetaModel.TYPE.DETAIL_INFO:
                            ParameterSetupDetailInfoDataModel detailInfo = (ParameterSetupDetailInfoDataModel)mModel.Data;
                            ParameterSetupDetailInfoListDataModel dList =
                            (ParameterSetupDetailInfoListDataModel)((ParameterSetupBaseInfoListDataModel)model)
                            .getElementByBaseNum(detailInfo.paraCode1).detailList;

                            dList.updateElement(detailInfo);
                            break;
                    }
                }
            }
        }
    }
}
