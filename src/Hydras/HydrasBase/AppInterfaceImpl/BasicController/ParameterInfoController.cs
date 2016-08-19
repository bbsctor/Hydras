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
using HydrasBase.Util;
using HydrasProtocol.StringMap;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class ParameterInfoController : HydrasBasicController
    {
        private int paraNum = 0;

        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();

            if ("getParameterInfo".Equals(action))
            {
                readParameterNum();
                readAllParameterInfo();
            }
        }

        private void readParameterNum()
        {
            string action = ActionStrAssistant.buildActionStr("getParameterInfo",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", 0 + "")
                    });
            base.execute(action);
        }

        private void readAllParameterInfo()
        {
            for (int i = 0; i < paraNum; i++)
            {
                string action = ActionStrAssistant.buildActionStr("getParameterInfo",
                    new ConfigFrame.BaseService.ActionStrAssistant.ParameterValue[]
                    {
                        new ActionStrAssistant.ParameterValue("para1", i + 1 + "")
                    });
                base.execute(action);
            }
            initSondeParameterMapper();
        }

        private void initSondeParameterMapper()
        {
            SondeParameterMapper mapper =
                SondeParameterMapperContext.getParameterMapper((SerialPortModel)CommService.ConnectParameterModel);     
            ParameterInfoListDataModel listModel =
                ((ParameterInfoListDataModel)model);
            mapper.clear();
            for (int i = 0; i < listModel.Count; i++)
            {
                string temp = StringByteConverter.byteArrayToString(listModel[i].paraName) + "[" +
                    StringByteConverter.byteArrayToString(listModel[i].calUnit) + "]";
                //string temp = StringByteConverter.byteArrayToString(listModel[i].paraName);
                mapper.add(listModel[i].sn, listModel[i].paraCode, temp);
            }
            
        }

        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                ParameterMetaModel cModel = mModel as ParameterMetaModel;
                if (cModel != null)
                {
                    ParameterInfoDataModel sondePara = (ParameterInfoDataModel)cModel.Data;
                    switch (cModel.type)
                    {
                        case ParameterMetaModel.TYPE.PARAMETER_NUM:
                            this.paraNum = (int)sondePara.paraNum;
                            break;
                        case ParameterMetaModel.TYPE.PARAMETER_INFO:
                            ((ParameterInfoListDataModel)model).updateElement(sondePara);
                            break;
                    }
                }
            }
        }
    }
}
