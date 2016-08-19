using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseViewModelImpl
{
    public class SecurityLevelViewModel : BasicModel, IViewModel
    {
        public byte currentLevel;

        public string level1Pwd;
        public string level2Pwd;
        public string level3Pwd;
    }
}