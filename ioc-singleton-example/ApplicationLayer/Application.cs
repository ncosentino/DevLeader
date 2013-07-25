using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ApplicationLayer
{
    public static class Application
    {
        private static readonly object _instanceLock = new object();
        private static IApplication _instance;

        public static event EventHandler<QueryTypeEventArgs> QueryType;

        public static IApplication Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = CreateInstance();
                        }
                    }
                }

                return _instance;
            }
        }

        private static IApplication CreateInstance()
        {
            var handler = QueryType;
            if (handler == null)
            {
                throw new InvalidOperationException(
                    "Cannot create an instance because the QueryType event " +
                    "handler was never set.");
            }

            var args = new QueryTypeEventArgs();
            handler.Invoke(null, args);

            if (args.Type == null)
            {
                throw new InvalidOperationException(
                    "Cannot create an instance because the type has not been " +
                    "provided.");
            }

            // NOTE: here's where things get weird. you need to define your own
            // sort of "contract" for what type of constructor you will allow.
            // you might not even use a constructor here... but you need to
            // define what mechanism the provided type must have to provide
            // you with a singleton instance. i'm a fan of providing a type
            // with a private parameterless constructor, so i'll demonstrate
            // with that. your requirements will change what this section of
            // code looks like.
            if (!typeof(IApplication).IsAssignableFrom(args.Type))
            {
                throw new InvalidOperationException(
                    "Cannot create an instance because the provided type does " +
                    "not implement the IApplication interface.");
            }

            const BindingFlags FLAGS = 
                BindingFlags.CreateInstance | 
                BindingFlags.Instance | 
                BindingFlags.NonPublic;
            
            var constructors = args.Type.GetConstructors(FLAGS);
            if (constructors.Length != 1)
            {
                throw new InvalidOperationException(
                    "Cannot create an instance because a single private " +
                    "parameterless constructor was expected.");
            }

            return (IApplication)constructors[0].Invoke(null);
        }
    }
}
