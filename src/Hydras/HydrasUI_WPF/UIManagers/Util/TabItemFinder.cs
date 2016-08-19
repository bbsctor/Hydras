using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace HydrasUI_WPF.UIManagers.Util
{
    public class TabItemFinder
    {
        public static TabItem findTabItemByHeader(TabControl tabControl, string header)
        {
            TabItem temp = null;
            for (int i = 0; i < tabControl.Items.Count; i++)
            {
                if (((TabItem)tabControl.Items[i]).Header.Equals(header))
                {
                    temp = (TabItem)tabControl.Items[i];
                    break;
                }
            }
            return temp;
        }
    }
}
