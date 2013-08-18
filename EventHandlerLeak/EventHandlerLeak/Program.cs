using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace EventHandlerLeak
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter the example number to run and press enter. Press enter with no other input to exit.");
                Console.WriteLine("Example 1: Hooking with an instance-scope EventHandler");
                Console.WriteLine("Example 2: Hooking with an anonymous delegate (no parent reference)");
                Console.WriteLine("Example 3: Hooking with an anonymous delegate (with parent reference)");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                Console.Clear();

                int example;
                if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out example))
                {
                    Console.WriteLine("That wasn't an integer! Press enter.");
                    Console.ReadLine();
                    continue;
                }

                switch (example)
                {
                    case 1:
                        Example1();
                        break;
                    case 2:
                        Example2();
                        break;
                    case 3:
                        Example3();
                        break;
                    default:
                        Console.WriteLine("Example " + example + " doesn't exist! Press enter.");
                        Console.ReadLine();
                        continue;
                }

                Console.WriteLine("Press enter to try another example.");
                Console.ReadLine();
            }
        }

        private static void Example1()
        {
            // first we allocate some objects. the second object we create 
            // depends on the first. the second object is going to hook onto
            // the event exposed by the first object.
            Console.WriteLine("Creating instances...");
            var objectWithEvent = new ObjectWithEvent();
            var objectThatHooksEvent = new ObjectThatHooksEvent(objectWithEvent);

            // we'll set the instance to null and call the GC, but since we 
            // have an event still hooked up to the first object, no objects 
            // will get finalized yet.
            Console.WriteLine();
            Console.WriteLine("Setting the object that hooks the event to null and calling garbage collector...");
            objectThatHooksEvent = null;
            GC.Collect();
            Console.WriteLine("Nothing magical?");

            // let's try unhooking the event now and calling the gc. we should
            // finally be able to get rid of our second object
            Console.WriteLine();
            Console.WriteLine("Press enter and I'll unhook the events for you and call the garbage collector again.");
            Console.ReadLine();
            objectWithEvent.UnhookAll();
            GC.Collect();

            // now that we've ditched the second object, we can safely get rid
            // of the first one!
            Console.WriteLine("Neat-o, eh? Press enter and I'll set the object with the event to null and call the garbage collector one last time.");
            Console.ReadLine();
            objectWithEvent = null;
            GC.Collect();

            Console.WriteLine("And presto! Both are gone.");
        }

        private static void Example2()
        {
            // first we allocate some objects. the second object we create 
            // depends on the first. the second object is going to hook onto
            // the event exposed by the first object.
            Console.WriteLine("Creating instances...");
            var objectWithEvent = new ObjectWithEvent();
            var objectThatHooksEvent = new HookWithAnonymousDelegate(objectWithEvent);

            // we'll set the instance to null and call the GC, and since we 
            // our event handler we've set up doesn't have any dependencies on
            // the object we've hooked with, it will get cleaned up.
            Console.WriteLine();
            Console.WriteLine("Setting the object that hooks the event to null and calling garbage collector...");
            objectThatHooksEvent = null;
            GC.Collect();

            // the first object should be just as easy to get rid of!
            Console.WriteLine("Look at that! Press enter and I'll set the object with the event to null and call the garbage collector one last time.");
            Console.ReadLine();
            objectWithEvent = null;
            GC.Collect();

            Console.WriteLine("And presto! Both are gone.");
        }

        private static void Example3()
        {
            // first we allocate some objects. the second object we create 
            // depends on the first. the second object is going to hook onto
            // the event exposed by the first object.
            Console.WriteLine("Creating instances...");
            var objectWithEvent = new ObjectWithEvent();
            var objectThatHooksEvent = new HookWithAnonymousDelegate2(objectWithEvent);

            // we'll set the instance to null and call the GC, but since we 
            // have an event still hooked up to the first object, no objects 
            // will get finalized yet.
            Console.WriteLine();
            Console.WriteLine("Setting the object that hooks the event to null and calling garbage collector...");
            objectThatHooksEvent = null;
            GC.Collect();
            Console.WriteLine("Nothing magical?");

            // let's try unhooking the event now and calling the gc. we should
            // finally be able to get rid of our second object
            Console.WriteLine();
            Console.WriteLine("Press enter and I'll unhook the events for you and call the garbage collector again.");
            Console.ReadLine();
            objectWithEvent.UnhookAll();
            GC.Collect();

            // now that we've ditched the second object, we can safely get rid
            // of the first one!
            Console.WriteLine("Neat-o, eh? Press enter and I'll set the object with the event to null and call the garbage collector one last time.");
            Console.ReadLine();
            objectWithEvent = null;
            GC.Collect();

            Console.WriteLine("And presto! Both are gone.");
        }

        private class ObjectWithEvent
        {
            ~ObjectWithEvent()
            {
                Console.WriteLine(this + " is being finalized.");
            }

            public event EventHandler<EventArgs> Event;

            public void UnhookAll()
            {
                Event = null;
            }
        }

        private class ObjectThatHooksEvent
        {
            public ObjectThatHooksEvent(ObjectWithEvent objectWithEvent)
            {
                objectWithEvent.Event += ObjectWithEvent_Event;
            }

            ~ObjectThatHooksEvent()
            {
                Console.WriteLine(this + " is being finalized.");
            }

            private void ObjectWithEvent_Event(object sender, EventArgs e)
            {
                // some fancy event
            }
        }

        private class HookWithAnonymousDelegate
        {
            public HookWithAnonymousDelegate(ObjectWithEvent objectWithEvent)
            {
                objectWithEvent.Event += (sender, args) =>
                {
                    // handle your event
                    // (this one is special because it doesn't use anything related to the instance)
                    Console.WriteLine("Event being called!");
                };
            }

            ~HookWithAnonymousDelegate()
            {
                Console.WriteLine(this + " is being finalized.");
            }
        }

        private class HookWithAnonymousDelegate2
        {
            public HookWithAnonymousDelegate2(ObjectWithEvent objectWithEvent)
            {
                objectWithEvent.Event += (sender, args) =>
                {
                    // handle your event and use something that's part of this instance
                    SomeInnocentLittleMethod();
                };
            }

            ~HookWithAnonymousDelegate2()
            {
                Console.WriteLine(this + " is being finalized.");
            }

            private void SomeInnocentLittleMethod()
            {
                Console.WriteLine("... Not so innocent after all!");
            }
        }
    }
}
