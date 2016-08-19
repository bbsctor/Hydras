using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace HydrasBase.BaseModelImpl.BaseMetaModelImpl
{
    public class DeviceStorageMetaModel : MetaModel
    {
        public enum TYPE { DEVICE_STORAGE };
        public TYPE type;
    }
}