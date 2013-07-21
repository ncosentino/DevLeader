using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ncosentino.ThreadPatternExample
{
    internal class GroupThreadRunner : IThreadRunner
    {
        #region Fields

        private readonly object _threadLock;
        private readonly Dictionary<Thread, AutoResetEvent> _triggers;

        private bool _running;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="SingleThreadRunner"/> class from being created.
        /// </summary>
        private GroupThreadRunner()
        {
            _threadLock = new object();
            _triggers = new Dictionary<Thread, AutoResetEvent>();
        }

        #endregion

        #region Exposed Members

        public static IThreadRunner Create()
        {
            return new GroupThreadRunner();
        }

        public void Start()
        {
            lock (_threadLock)
            {
                // check if any are already running
                if (_triggers.Count > 0)
                {
                    return;
                }

                _running = true;

                const int NUMBER_OF_THREADS = 4;
                for (int i = 0; i < NUMBER_OF_THREADS; ++i)
                {
                    var thread = new Thread(DoWork);
                    thread.Name = "Thread " + i;

                    var trigger = new AutoResetEvent(false);
                    _triggers[thread] = trigger;

                    thread.Start(trigger);
                }
            }
        }

        public void Stop()
        {
            lock (_threadLock)
            {
                // check if not running
                if (_triggers.Count <= 0)
                {
                    return;
                }

                _running = false;
                foreach (var trigger in _triggers.Values)
                {
                    trigger.Set();
                }
            }
        }

        #endregion

        #region Internal Members

        private void DoWork(object parameter)
        {
            var currentThread = Thread.CurrentThread;

            // this was the trigger that we passed in. elesewhere in the 
            // instance, we can use this object to wake up the thread.
            var trigger = (AutoResetEvent)parameter;

            try
            {
                // keep running while we're expected to be running
                while (_running)
                {
                    // DO ALL SORTS OF AWESOME WORK HERE.
                    Console.WriteLine("Awesome work being done by " + currentThread.Name);

                    // put this thread to sleep, but remember it can be woken 
                    // up from other places in this instance.
                    trigger.WaitOne(1000);
                }
            }
            finally
            {
                lock (_threadLock)
                {
                    _triggers.Remove(currentThread);

                    // if we were still expected to be running, change the 
                    // state to suggest that we're not
                    if (_running && _triggers.Count <= 0)
                    {
                        _running = false;
                    }
                }
            }
        }

        #endregion
    }
}
