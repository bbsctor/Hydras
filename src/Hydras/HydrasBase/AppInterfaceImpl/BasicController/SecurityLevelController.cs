using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.Util;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseMetaModelImpl;

namespace HydrasBase.AppInterfaceImpl.BasicController
{
    public class SecurityLevelController : HydrasBasicController
    {
        public override void execute(string action)
        {
            setSlaveAddr();
            resetDataModel();

            if ("setAllLevelPassword".Equals(action))
            {
                setAllLevelPassword();
            }
            else
            {
                base.execute(action);
            }
        }

        private void setAllLevelPassword()
        {
            SecurityLevelDataModel dModel = (SecurityLevelDataModel)model;
            string level1Pwd = StringByteConverter.byteArrayToString(dModel.level1Pwd);
            string level2Pwd = StringByteConverter.byteArrayToString(dModel.level2Pwd);
            string level3Pwd = StringByteConverter.byteArrayToString(dModel.level3Pwd);

            setSingleLevelPassword(0x01, level1Pwd);
            setSingleLevelPassword(0x02, level2Pwd);
            setSingleLevelPassword(0x03, level3Pwd);
        }

        private void setSingleLevelPassword(byte level, string pwd)
        {
            string action = genAction("setSingleLevelPassword", new string[]{((int)level).ToString(), pwd});
            base.execute(action);
        }

        public override void handleResult(IMetaModel mModel)
        {
            if (mModel != MetaModel.NULL)
            {
                SecurityLevelMetaModel cModel = mModel as SecurityLevelMetaModel;
                if (cModel != null)
                {
                    switch (cModel.type)
                    {
                        case SecurityLevelMetaModel.TYPE.SECURITY_LEVEL:
                            SecurityLevelDataModel securityLevel = (SecurityLevelDataModel)cModel.Data;
                            ((SecurityLevelDataModel)model).update(securityLevel);
                            break;
                    }
                }
            }
        }
    }
}
