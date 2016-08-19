using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class ParameterSetupBaseInfoDataModel : BasicModel, IDataModel
    {
        public byte para1;
        public byte para2;
        public ParameterSetupDetailInfoListDataModel detailList;

        public ParameterSetupBaseInfoDataModel()
        {
            detailList = new ParameterSetupDetailInfoListDataModel();
        }

        public override void update(IModel model)
        {
            update(model, new string[]{"para2"});
        }

        public void updateDetailModel(ParameterSetupDetailInfoDataModel model)
        {
            detailList.updateElement(model);
        }
    }
}
