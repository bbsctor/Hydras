using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using ConfigFrame.ExceptionHandle;

namespace ConfigFrame.UITask
{
    public class BasicTaskGroup : List<BasicTask>, ConfigFrame.UITask.IBasicTaskGroup
    {
        public enum Mode { SINGLE_THREAD, MULTI_THREAD };
        private Mode currentMode;
        public Mode CurrentMode
        {
            get { return currentMode; }
            set { this.currentMode = value; }
        }

        private BasicTask.UpdateView updateView;

        protected BasicTask.UpdateView UpdateView
        {
            get { return updateView; }
            set { updateView = value; }
        }
        private Control control;

        private int interval;

        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        protected Control Control
        {
            get { return control; }
            set { control = value; }
        }
        protected Object taskType;
        private Thread taskThread;
        private bool doLoop;
        private int times;
        private AutoResetEvent autoEvent1;
        private AutoResetEvent autoEvent2;
        private bool isDeepSleep;
        public ExceptionHandler exceptionHandler;

        public BasicTaskGroup(Control control, BasicTask.UpdateView updateView)
        {
            this.control = control;
            this.updateView = updateView;
            times = 0;
            doLoop = true;
            autoEvent1 = new AutoResetEvent(false);
            autoEvent2 = new AutoResetEvent(false);
            isDeepSleep = false;
            interval = 1000;
            currentMode = Mode.MULTI_THREAD;
        }

        public void finish()
        {
            if (taskThread != null)
            {
                taskThread.Abort();
            }
        }

        public void execute(Object taskType, int times)
        {
            if (currentMode == Mode.MULTI_THREAD)
            {
                stopLoop();
                lock (this)
                {
                    this.taskType = taskType;
                    this.times = times;
                }
                startLoop();
            }
            else if (currentMode == Mode.SINGLE_THREAD)
            {
                lock (this)
                {
                    this.taskType = taskType;
                    this.times = times;
                }
                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < this.Count; j++)
                    {
                        if (this[j].taskType.Equals(this.taskType))
                        {
                            this[j].doWork();
                        }
                    }
                }
            }
        }

        public void startLoop()
        {
            if (taskThread == null)
            {
                taskThread = new Thread(runTimes);
                taskThread.Name = "taskThread" + taskThread.ManagedThreadId;
                taskThread.Start();
            }
            else if (isDeepSleep == true)
            {
                autoEvent1.Set();
            }
        }

        public void stopLoop()
        {
            if (taskThread != null && times < 0)
            {
                isDeepSleep = true;
                times = 0;
            }
        }

        public void runTimes()
        {
            while (doLoop == true)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].taskType.Equals(this.taskType))
                    {
                        try
                        {
                            this[i].doWork();
                        }
                        catch (Exception e)
                        {
                            if (exceptionHandler != null)
                            {
                                exceptionHandler.handle(e);
                            }
                        }
                    }
                }
                if (times > 0)
                {
                    times--;
                }
                if (times == 0)
                {
                    isDeepSleep = true;
                    autoEvent1.WaitOne();
                    continue;
                }
                autoEvent2.WaitOne(interval);
            }
        }
    }
}
