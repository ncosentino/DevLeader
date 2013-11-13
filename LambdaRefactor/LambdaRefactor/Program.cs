using System;
using System.Collections.Generic;
using System.Text;

using LambdaRefactor.Processing;

using PreRefactorFactory = LambdaRefactor.Processing.PreRefactor.ProcessorFactory;
using PostRefactorFactory = LambdaRefactor.Processing.PostRefactor.ProcessorFactory;

namespace LambdaRefactor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // this entire example is based on a real-world example. i've 
            // opted to try and introduce some complexity to the system by 
            // creating some artificial dependency on an object reference. 
            // throughout the code, you'll see that many of the classes depend
            // on having this set, although we never really do anything with 
            // it. this is to simulate something closer to production code
            // where you may actually have additional dependencies passed down
            // your call/class hierarchy that you need to worry about.
            Console.WriteLine("Creating our super important dependency...");
            var superImportantReference = new object();

            // let's create an instance of our pre-refactor factory and an 
            // instance of our factory after the lambda refactor.
            Console.WriteLine("Creating our pre/post refactor factory instances...");
            var preRefactorFactory = new PreRefactorFactory();
            var postRefactorFactory = new PostRefactorFactory();

            // run all of the tests! (i opted to not include any real test 
            // framework, but the concepts still apply)
            Console.WriteLine("Running 'GreaterThan' processor tests...");
            TestGreaterThan(preRefactorFactory, superImportantReference);
            TestGreaterThan(postRefactorFactory, superImportantReference);

            Console.WriteLine("Running 'StringEquals' processor tests...");
            TestStringEqual(preRefactorFactory, superImportantReference);
            TestStringEqual(postRefactorFactory, superImportantReference);

            /* 
             * Exercise for you: implement the other processors in the pre-
             * refactor code and in the post refactor code. which method 
             * involved more work?
             */

            //TestLessThan(preRefactorFactory, superImportantReference);
            //TestLessThan(postRefactorFactory, superImportantReference);

            /*
             * Code some of the remaining test functions to see if your 
             * implementations match up!
             */

            Console.WriteLine("Press 'enter' to exit.");
            Console.ReadLine();
        }

        private static void TestGreaterThan(IProcessorFactory factory, object superImportantReference)
        {
            const int FILTER_VALUE = 10;
            var processor = factory.Create(
                ProcessorType.GreaterThan, 
                superImportantReference, 
                FILTER_VALUE);

            if (processor.TryProcess(FILTER_VALUE - 5))
            {
                throw new InvalidOperationException("This should not have been greater than.");
            }

            if (!processor.TryProcess(FILTER_VALUE + 5))
            {
                throw new InvalidOperationException("This should have been greater than.");
            }

            if (processor.TryProcess("This isn't a number."))
            {
                throw new InvalidOperationException("This should have failed because the input is invalid.");
            }

            if (processor.TryProcess(new object()))
            {
                throw new InvalidOperationException("This should have failed because the input is invalid.");
            }
        }

        private static void TestLessThan(IProcessorFactory factory, object superImportantReference)
        {
            const int FILTER_VALUE = 10;
            var processor = factory.Create(
                ProcessorType.LessThan,
                superImportantReference,
                FILTER_VALUE);

            if (!processor.TryProcess(FILTER_VALUE - 5))
            {
                throw new InvalidOperationException("This should have been less than.");
            }

            if (processor.TryProcess(FILTER_VALUE + 5))
            {
                throw new InvalidOperationException("This should not have been less than.");
            }

            if (processor.TryProcess("This isn't a number."))
            {
                throw new InvalidOperationException("This should have failed because the input is invalid.");
            }

            if (processor.TryProcess(new object()))
            {
                throw new InvalidOperationException("This should have failed because the input is invalid.");
            }
        }

        private static void TestStringEqual(IProcessorFactory factory, object superImportantReference)
        {
            const string FILTER_VALUE = "Hello, World!";
            var processor = factory.Create(
                ProcessorType.StringEqual,
                superImportantReference,
                FILTER_VALUE);

            if (!processor.TryProcess(FILTER_VALUE))
            {
                throw new InvalidOperationException("This should have matched.");
            }

            if (processor.TryProcess("Goodbye, Cruel World!"))
            {
                throw new InvalidOperationException("This should not have matched.");
            }

            if (processor.TryProcess(10))
            {
                throw new InvalidOperationException("This should not have matched.");
            }

            if (processor.TryProcess(new object()))
            {
                throw new InvalidOperationException("This should not have matched.");
            }
        }
    }
}
