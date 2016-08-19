using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Windows.Controls;

namespace ConfigFrame.UITask
{

    public class BasicTask
    {
        public delegate void UpdateView();
        public delegate void Process();
        private UpdateView updateView;
        private Control control;
        private Process process;
        public Object taskType;

        public BasicTask(Object taskType, Control control, UpdateView updateView, Process process)
        {
            this.taskType = taskType;
            this.control = control;
            this.updateView = updateView;
            this.process = process;
        }

        public BasicTask(Control control, UpdateView updateView, Process process):
            this(null, control, updateView, process)
        {

        }

        public void doWork()
        {
            process();
            control.Dispatcher.Invoke(updateView);
        }
    }
}
