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

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class DateFormatController : HydrasBasicController
    {
        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();

            if ("setDateFormat".Equals(action))
            {
                setDateFormat();
            }
            else
            {
                base.execute(action);
            }
        }

        private void setDateFormat()
        {
            string para1 = BitConverter.ToString(new byte[]{((DateFormatDataModel)model).format});
            string para2 = BitConverter.ToString(new byte[]{((DateFormatDataModel)model).useDelimiter});
            string action = genAction("setDateFormat", new string[] { para1, para2});
            base.execute(action);
        }

        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                DateFormatMetaModel cModel = mModel as DateFormatMetaModel;
                if (cModel != null)
                {
                    switch (cModel.type)
                    {
                        case DateFormatMetaModel.TYPE.FORMAT_INFO:
                            DateFormatDataModel sondeInfo = (DateFormatDataModel)cModel.Data;
                            ((DateFormatDataModel)model).update(sondeInfo);
                            break;
                    }
                }
            }
        }
    }
}