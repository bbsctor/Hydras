using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConfigFrame.AppInterface;
using ConfigFrame.BaseModel;
using ConfigFrame.BaseService;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.CommunicationService;
using ConfigFrame.ExceptionHandle;

namespace ConfigFrame.AppInterface
{
    public class LocalRunningServer
    {
        public bool running;
        public enum State { BUSY, IDLE };
        private IDynamicController currentController;
        private IDynamicController oldController;
        private AutoResetEvent autoResetEvent1 = new AutoResetEvent(false);
        private AutoResetEvent autoResetEvent2 = new AutoResetEvent(false);
        private AutoResetEvent autoResetEvent3 = new AutoResetEvent(false);
        private AutoResetEvent autoResetEvent4 = new AutoResetEvent(false);
        private Object stateLock = new object();
        private bool isIdleWorkOn;
        private bool goStop;
        private bool isSleep;
        private int switchInterval;
        private int busyInterval;
        private int idleInterval;

        public int IdleInterval
        {
            get { return idleInterval; }
            set { idleInterval = value; }
        }

        public int BusyInterval
        {
            get { return busyInterval; }
            set { busyInterval = value; }
        }

        public int SwitchInterval
        {
            get { return switchInterval; }
            set { switchInterval = value; }
        }

        public bool IsIdleWorkOn
        {
            get { return isIdleWorkOn; }
            set { isIdleWorkOn = value; }
        }

        public IDynamicController CurrentController
        {
            get { return currentController; }
            set { currentController = value; }
        }
        private State state;
        private ICommunicationService commService;
        private Thread thread;

        public ICommunicationService CommService
        {
            get {return commService;}
            set { this.commService = value; }
        }
        public ExceptionHandler exceptionHandler;
        public State getState()
        {
            lock (stateLock)
            {
                return this.state;
            }
        }

        public void setState(State state)
        {
            lock (stateLock)
            {
                this.state = state;
            }
        }

        public LocalRunningServer()
        {
            switchInterval = 3000;
            busyInterval = 0;
        }

        public void doWork(IDynamicController controller)
        {
            if (running == true)
            {
                lock (this)
                {
                    notify();
                    goStop = true;
                    autoResetEvent1.WaitOne();

                    oldController = this.currentController;
                    this.currentController = controller;
                    setState(State.BUSY);

                    autoResetEvent2.Set();
                    autoResetEvent3.WaitOne();
                }
            }
        }

        public void start(IDynamicController controller)
        {
            this.currentController = controller;
            running = true;
            isIdleWorkOn = true;
            setState(State.IDLE);
            thread = new Thread(runningLoop);
            thread.Start();
            Console.WriteLine("start server!");
        }

        public void finish()
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }

        public void notify()
        {
            if (thread.ThreadState == ThreadState.WaitSleepJoin)
            {
                autoResetEvent4.Set();
            }
        }

        private void runningLoop()
        {
            try
            {
                IRequest[] reqs = null;
                while (running == true)
                {
                    if (getState() == State.IDLE && isIdleWorkOn == true)
                    {
                        Console.WriteLine("====================server state : idle===================");

                        reqs = currentController.ModelProtocolService.genIdleRequest();
                        IResponse resp;
                        for (int i = 0; i < reqs.Length; i++)
                        {
                            resp = commService.processRequest(reqs[i]);
                            if (resp != null)
                            {
                                IMetaModel model = currentController.ModelProtocolService.handleResponse(resp);
                                currentController.handleResult(model);
                            }
                            if (idleInterval > 0)
                            {
                                Thread.Sleep(idleInterval);
                            }
                        }
                    }
                    else if (getState() == State.BUSY)
                    {
                        Console.WriteLine("====================server state : busy===================");
                        reqs = currentController.ModelProtocolService.
                            genRequestByActionName(currentController.Target, currentController.Model);
                        IResponse resp;
                        for (int i = 0; i < reqs.Length; i++)
                        {
                            resp = commService.processRequest(reqs[i]);
                            if (resp != null)
                            {
                                IMetaModel model = currentController.ModelProtocolService.handleResponse(resp);
                                currentController.handleResult(model);
                            }
                            if (busyInterval > 0)
                            {
                                Thread.Sleep(busyInterval);
                            }
                        }

                        goStop = false;
                        this.currentController = oldController;
                        setState(State.IDLE);
                        autoResetEvent3.Set();
                    }
                    if (goStop == true)
                    {
                        autoResetEvent1.Set();
                        autoResetEvent2.WaitOne();
                    }
                    else
                    {
                        bool b = autoResetEvent4.WaitOne(switchInterval);
                    }
                }
            }
            catch (Exception e)
            {
                exceptionHandler.handle(e);
            }
        }

        

        public void stop()
        {
            if (thread != null && (thread.ThreadState == ThreadState.WaitSleepJoin ||
                thread.ThreadState == ThreadState.Running))
            {
                notify();
                goStop = true;
                autoResetEvent1.WaitOne();
                finish();
            }
        }
    }
}
