using System;
using System.Collections.Generic;
using System.Text;

namespace singleton_examples
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    internal class ThreadSafeSingleton
    {
        private static readonly object _instanceLock = new object();
        private static ThreadSafeSingleton _instance;

        /// 
        /// Prevents a default instance of the class from being created.
        /// 
        private ThreadSafeSingleton()
        {
        }

        /// 
        /// Gets the singleton instance (thread safe).
        /// 
        internal static ThreadSafeSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ThreadSafeSingleton();
                        }
                    }
                }

                return _instance;
            }
        }
    }


    internal class NotThreadSafeSingleton
    {
        private static NotThreadSafeSingleton _instance;

        /// 
        /// Prevents a default instance from being created.
        /// 
        private NotThreadSafeSingleton()
        {
        }

        /// 
        /// Gets the singleton instance (not thread safe).
        /// 
        internal static NotThreadSafeSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotThreadSafeSingleton();
                }

                return _instance;
            }
        }
    }
}
