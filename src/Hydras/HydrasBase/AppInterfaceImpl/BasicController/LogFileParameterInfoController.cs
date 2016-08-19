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
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;
using HydrasBase.BaseModelImpl.BaseDataModelImpl.Common;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class LogFileParameterInfoController : HydrasBasicController
    {

        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();

            if (action.Contains("readLogFileParameterInfo"))
            {
                ((LogFileParameterInfoListDataModel)model).Clear();
                int para1, para2;
                int.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                int.TryParse(ActionStrAssistant.getParameterStrValue(action, "para2"), out para2);
                readLogFileParameterInfo(para1, para2);
            }
            else if (action.Contains("updateLogFileParameterInfo"))
            {
                int para1;
                int.TryParse(ActionStrAssistant.getParameterStrValue(action, "para1"), out para1);
                updateLogFileParameterInfo(para1);
            }
        }

        private void readLogFileParameterInfo(int para1, int para2)
        {
            for (int i = 0; i < para2; i++)
            {
                string action = base.genAction("readLogFileParameterInfo", 
                    new string[]{para1.ToString(), (i + 1).ToString()});
                base.execute(action);
            }
        }

        private void updateLogFileParameterInfo(int para1)
        {
            BaseSondeParameterInfoListDataModel temp;
            temp = (BaseSondeParameterInfoListDataModel)((BaseSondeParameterInfoListDataModel)model).removedList;
            for (int i = 0; i < temp.Count; i++)
            {
                //para1 logNum
                //para2 paraCode
                string action = base.genAction("removeLogFileParameterInfo",
                    new string[] { para1.ToString(), temp[i].paraCode.ToString() });
                base.execute(action);
            }
            temp = (BaseSondeParameterInfoListDataModel)((BaseSondeParameterInfoListDataModel)model).addedList;
            for (int i = 0; i < temp.Count; i++)
            {
                //para1 logNum
                //para2 paraCode
                string action = base.genAction("addLogFileParameterInfo",
                    new string[] { para1.ToString(), temp[i].paraCode.ToString() });
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
                    LogFileParameterInfoDataModel sondePara = (LogFileParameterInfoDataModel)cModel.Data;
                    switch (cModel.type)
                    {
                        case LogFileMetaModel.TYPE.SELECTED_PARAMETER:
                            ((LogFileParameterInfoListDataModel)model).updateElement(sondePara);
                            break;
                    }
                }
            }
        }
    }
}
