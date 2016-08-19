﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.UITask
{
    public interface IBlockUIManager
    {
        bool inputValidating();
        void updateUI();
        void updateDataModel();
    }
}
