using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ncosentino.ThreadPatternExample
{
    internal class SingleThreadRunner : IThreadRunner
    {
        #region Fields

        private readonly object _threadLock;
        private readonly AutoResetEvent _trigger;
        private Thread _theOneThread;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="SingleThreadRunner"/> class from being created.
        /// </summary>
        private SingleThreadRunner()
        {
            _threadLock = new object();
            _trigger = new AutoResetEvent(false);
        }

        #endregion

        #region Exposed Members

        public static IThreadRunner Create()
        {
            return new SingleThreadRunner();
        }

        public void Start()
        {
            lock (_threadLock)
            {
                // check if already running
                if (_theOneThread != null)
                {
                    return;
                }

                _theOneThread = new Thread(DoWork);
                _theOneThread.Name = "The One Thread";
                _theOneThread.Start(_trigger);
            }
        }

        public void Stop()
        {
            lock (_threadLock)
            {
                // check if not running
                if (_theOneThread == null)
                {
                    return;
                }

                _theOneThread = null;
                _trigger.Set();
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
                while (currentThread == _theOneThread)
                {
                    // DO ALL SORTS OF AWESOME WORK HERE.
                    Console.WriteLine("Awesome work being done.");

                    // put this thread to sleep, but remember it can be woken 
                    // up from other places in this instance.
                    trigger.WaitOne(1000);
                }
            }
            finally
            {
                lock (_threadLock)
                {
                    // if we were still expected to be running, change the 
                    // state to suggest that we're not
                    if (_theOneThread == currentThread)
                    {
                        _theOneThread = null;
                    }
                }
            }
        }

        #endregion
    }
}
