using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;

namespace ConfigFrame.Util
{
    public delegate void DisplayDateTime(DateTime dt);
    public class UserClock
    {
        public DisplayDateTime displayMethod;
        private DateTime dateTime;

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }
        private System.Timers.Timer aTimer;
        private Control control;

        public UserClock(DateTime dt)
        {
            dateTime = dt;
            aTimer.Enabled = false;
        }
        public UserClock(DisplayDateTime display)
        {
            aTimer = new System.Timers.Timer(1000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = false;
            displayMethod = display;
        }

        public UserClock(DisplayDateTime display, Control ctrl) :
            this(display)
        {
            control = ctrl;
        }

        public void reset(DateTime dt)
        {
            dateTime = dt;
            aTimer.Enabled = false;
        }

        public void start()
        {
            aTimer.Enabled = true;
        }

        public void stop()
        {
            aTimer.Enabled = false;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            dateTime += new TimeSpan(10000000);
            if (control != null)
            {
                control.Dispatcher.Invoke(displayMethod, dateTime);
            }
            else
            {
                displayMethod(dateTime);
            }

        }

        public DateTime getDateTime()
        {
            return dateTime;
        }
    }
}

