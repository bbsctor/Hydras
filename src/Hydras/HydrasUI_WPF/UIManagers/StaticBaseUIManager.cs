using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ConfigFrame.UITask;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasUI_WPF.UIServiceImpl;

namespace HydrasUI_WPF.UIManagers
{
    public class StaticBaseUIManager : BasicBlockUIManager
    {
        protected SondeWindow mainFrame;
        protected Dictionary<byte, string> subItemMapper;

        public StaticBaseUIManager(Control control)
            : base(control)
        {
            mainFrame = (SondeWindow)control;
            subItemMapper = new Dictionary<byte, string>();
        }


        public override void updateUI()
        {

        }
    }
}
