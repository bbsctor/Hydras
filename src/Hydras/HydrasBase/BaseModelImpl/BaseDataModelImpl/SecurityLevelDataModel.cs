using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class SecurityLevelDataModel : BasicModel, IDataModel
    {
        public byte currentLevel;

        public byte[] level1Pwd;
        public byte[] level2Pwd;
        public byte[] level3Pwd;

        public override void update(IModel model)
        {
            this.level1Pwd = ((SecurityLevelDataModel)model).level1Pwd;
            this.level2Pwd = ((SecurityLevelDataModel)model).level2Pwd;
            this.level3Pwd = ((SecurityLevelDataModel)model).level3Pwd;
        }
    }
}