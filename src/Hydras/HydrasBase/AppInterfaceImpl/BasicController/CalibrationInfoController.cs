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
using HydrasProtocol.ExceptionDefine;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class CalibrationInfoController : HydrasBasicController
    {
        public override void execute(string action)
        {
            setSlaveAddr();
            cService.connect();
            resetDataModel();

            //try
            //{
                if ("getBaseInfo".Equals(action))
                {
                    readAllBaseInfo();
                }
                else if ("getDetailInfo".Equals(action))
                {
                    CalibrationBaseInfoListDataModel baseModelList =
                (CalibrationBaseInfoListDataModel)model;
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
                else if (action.Contains("reset"))
                {
                    byte para1;
                    byte.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                    reset(para1);
                }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //    cService.disconnect();
            //    throw;
            //}
            
        }

        private void reset(byte paraCode)
        {
            string para1 = ((int)paraCode).ToString();
            string action = genAction("reset", new string[] { para1});
            base.execute(action);
        }

        private void setParameter(byte paraCode)
        {
            CalibrationBaseInfoDataModel baseModel =
                ((CalibrationBaseInfoListDataModel)model).getElementByBaseNum(paraCode);
            CalibrationDetailInfoListDataModel dListModel = baseModel.detailList;
            CalibrationDetailInfoDataModel dModel;
            SondeParameterMapper mapper = 
                SondeParameterMapperContext.getParameterMapper((SerialPortModel)CommService.ConnectParameterModel);
            for (int i = 0; i < dListModel.Count; i++)
            {
                dModel = dListModel[i];
                string para1 = ((int)baseModel.para1).ToString();
                string para2 = StringByteConverter.byteArrayToString(dModel.paraNameForShort);
                string para3 = StringByteConverter.byteArrayToString(dModel.paraUnit);
                string para4 = BitConverter.ToString(dModel.paraValue);
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
                try
                {
                    base.execute(action);
                }
                catch (AbnormalResponseException are)
                {
                    Console.WriteLine(are.ToString());
                }
            }
        }

        private void readDetailInfoByBaseNum(byte baseNum)
        {
            resetDataModel();
            CalibrationBaseInfoDataModel baseModel = ((CalibrationBaseInfoListDataModel)model).
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
                CalibrationMetaModel cModel = mModel as CalibrationMetaModel;
                if (cModel != null)
                {
                    switch (cModel.type)
                    {
                        case CalibrationMetaModel.TYPE.BASE_INFO:
                            CalibrationBaseInfoDataModel baseInfo = (CalibrationBaseInfoDataModel)mModel.Data;
                            if (baseInfo.para2 > 0)
                            {
                                ((CalibrationBaseInfoListDataModel)model).updateElement(baseInfo);
                            }
                            break;
                        case CalibrationMetaModel.TYPE.DETAIL_INFO:
                            CalibrationDetailInfoDataModel detailInfo = (CalibrationDetailInfoDataModel)mModel.Data;
                            CalibrationDetailInfoListDataModel dList =
                            (CalibrationDetailInfoListDataModel)((CalibrationBaseInfoListDataModel)model)
                            .getElementByBaseNum(detailInfo.paraCode1).detailList;

                            dList.updateElement(detailInfo);
                            break;
                    }
                }
            }
        }
    }
}

