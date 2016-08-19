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
    public class TabPageUIManager : BasicBlockUIManager
    {
        protected TabItem tabItem;
        protected Dictionary<byte, string> subItemMapper;

        public TabPageUIManager(Control control)
            : base(control)
        {
            tabItem = (TabItem)control;
            subItemMapper = new Dictionary<byte, string>();
        }


        public override void updateUI()
        {
            updateBaseInfo();

        }

        public virtual void updateBaseInfo()
        {

        }
    }
}
