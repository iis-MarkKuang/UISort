using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace UISort
{
    public class Waiter
    {
        private readonly System.Timers.Timer timer;
        private readonly EventWaitHandle waitHandle;

        public Waiter(TimeSpan? interval = null)
        {
            waitHandle = new AutoResetEvent(false);
            timer = new System.Timers.Timer();
            timer.Elapsed += (sender, args) => waitHandle.Set();
            SetInterval(interval);
        }

        public TimeSpan Interval
        {
            set { timer.Interval = value.TotalMilliseconds; }
        }

        public void Wait(TimeSpan? newInterval = null)
        {
            SetInterval(newInterval);
            timer.Start();
            waitHandle.WaitOne();
            timer.Close();
            waitHandle.Reset();
        }

        private void SetInterval(TimeSpan? newInterval)
        {
            if (newInterval.HasValue)
            {
                Interval = newInterval.Value;
            }
        }
    }
}
