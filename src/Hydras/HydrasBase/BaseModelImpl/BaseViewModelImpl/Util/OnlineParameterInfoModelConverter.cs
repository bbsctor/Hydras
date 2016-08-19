using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseViewModelImpl.Common;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl.Util
{
    public class ParameterInfoModelConverter
    {
        public static LogFileParameterInfoViewModel toLogFileParameterInfoModel(ParameterInfoViewModel ovModel)
        {
            LogFileParameterInfoViewModel lvModel = new LogFileParameterInfoViewModel();
            lvModel.CalFormat1 = ovModel.CalFormat1;
            lvModel.CalFormat2 = ovModel.CalFormat2;
            lvModel.CalUnit = ovModel.CalUnit;
            //lvModel.FormatAndScope = ovModel.FormatAndScope;
            lvModel.Format = ovModel.Format;
            lvModel.ScopeLow = ovModel.ScopeLow;
            lvModel.ScopeHigh = ovModel.ScopeHigh;
            lvModel.Sn = ovModel.Sn;
            lvModel.ParaCode = ovModel.ParaCode;
            lvModel.ParaName = ovModel.ParaName;
            lvModel.ParaNameForShort = ovModel.ParaNameForShort;
            lvModel.ParaNum = ovModel.ParaNum;

            return lvModel;
        }
    }
}
