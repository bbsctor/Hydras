using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasBase.BaseModelImpl.BaseDataModelImpl
{
    public class CalibrationBaseInfoDataModel : BasicModel, IDataModel
    {
        public byte para1;
        public byte para2;

        public CalibrationDetailInfoListDataModel detailList;

        public CalibrationBaseInfoDataModel()
        {
            detailList = new CalibrationDetailInfoListDataModel();
        }

        public override void update(IModel model)
        {

            update(model, new string[] { "para2" });
        }

        public void updateDetailModel(CalibrationDetailInfoDataModel model)
        {
            detailList.updateElement(model);
        }
    }
}