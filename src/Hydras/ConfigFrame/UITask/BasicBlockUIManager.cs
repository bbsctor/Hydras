using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Drawing;

namespace ConfigFrame.UITask
{
    public class BasicBlockUIManager:IBlockUIManager
    {
        protected Control control;

        public BasicBlockUIManager(Control control)
        {
            this.control = control;
        }

        public virtual bool inputValidating()
        {
            return true;
        }

        public virtual void updateUI()
        {

        }
        public virtual void updateDataModel()
        {

        }

        public virtual void cleanUI()
        {

        }
    }
}
