using System.Reflection;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);

//try
//{
//    Scenario3_RethrowCustom();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.InnerException.StackTrace);
//}

static void Scenario1_RethrowExplicit()
{
    try
    {
        Method1();
    }
    catch (Exception ex)
    {
        throw ex;
    }
}

static void Scenario2_RethrowImplicit()
{
    try
    {
        Method1();
    }
    catch (Exception ex)
    {
        throw;
    }
}

static void Scenario3_RethrowCustom()
{
    try
    {
        Method1();
    }
    catch (Exception ex)
    {
        throw new InvalidOperationException(
            "custom exception", 
            ex);
    }
}

static void Method1()
{
    Method2();
}

static void Method2()
{
    Method3();
}

static void Method3()
{
    Method4();
}

static void Method4()
{
    throw new InvalidOperationException(
        "expected exception");
}

[ShortRunJob]
public class ThrowVsReturnBenchmarks
{
    private static readonly ImplicitRethrower _implicitRethrower =
        new ImplicitRethrower();
    private static readonly ExplicitRethrower _explicitRethrower =
        new ExplicitRethrower();
    private static readonly NonThrower _nonThrower =
        new NonThrower();

    [Params(1000)]
    public int Iterations;

    [Benchmark]
    public void ImplicitRethrower_Benchmark()
    {
        for (var i = 0; i < Iterations; i++)
        {
            _implicitRethrower.EntryPoint();
        }
    }

    [Benchmark]
    public void ExplicitRethrower_Benchmark()
    {
        for (var i = 0; i < Iterations; i++)
        {
            _explicitRethrower.EntryPoint();
        }
    }

    [Benchmark]
    public void NonThrower_Benchmark()
    {
        for (var i = 0; i < Iterations; i++)
        {
            _nonThrower.EntryPoint();
        }
    }

    private class ImplicitRethrower
    {
        public bool EntryPoint()
        {
            try
            {
                Method1();   
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                return false;
            }

            return true;
        }

        private void Method1()
        {
            try
            {
                Method2();
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                throw;
            }
        }

        private void Method2()
        {
            try
            {
                Method3();
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                throw;
            }
        }

        private void Method3()
        {
            try
            {
                Method4();
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                throw;
            }
        }

        private void Method4()
        {
            try
            {
                Method5();
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                throw;
            }
        }

        private void Method5()
        {
            throw new InvalidOperationException(
                "expected exception");
        }
    }

    private class ExplicitRethrower
    {
        public bool EntryPoint()
        {
            try
            {
                Method1();
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                return false;
            }

            return true;
        }

        private void Method1()
        {
            try
            {
                Method2();
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                throw new InvalidOperationException(
                    "method 1 catch",
                    ex);
            }
        }

        private void Method2()
        {
            try
            {
                Method3();
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                throw new InvalidOperationException(
                    "method 2 catch",
                    ex);
            }
        }

        private void Method3()
        {
            try
            {
                Method4();
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                throw new InvalidOperationException(
                    "method 3 catch",
                    ex);
            }
        }

        private void Method4()
        {
            try
            {
                Method5();
            }
            catch (Exception ex)
            {
                // maybe do some other logic here...
                throw new InvalidOperationException(
                    "method 4 catch",
                    ex);
            }
        }

        private void Method5()
        {
            throw new InvalidOperationException(
                "expected exception");
        }
    }

    private class NonThrower
    {
        public bool EntryPoint()
        {
            var result = Method1();

            // do something with the result...

            // return true if no exception
            return result.Exception == null;
        }

        private (Exception? Exception, bool? Value) Method1()
        {
            return Method2();
        }

        private (Exception?, bool?) Method2()
        {
            return Method3();
        }

        private (Exception?, bool?) Method3()
        {
            return Method4();
        }

        private (Exception?, bool?) Method4()
        {
            return Method5();
        }

        private (Exception?, bool?) Method5()
        {
            return (
                new InvalidOperationException(
                    "expected exception"),
                true);
        }
    }
}


[ShortRunJob]
public class ThrowRethrowBenchmarks
{
    private static readonly InvalidOperationException ExpectedException =
        new InvalidOperationException("Expected exception.");

    [Benchmark]
    public void ThrowStaticException_RethrowImplicit()
    {
        try
        {
            try
            {
                throw ExpectedException;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        catch
        {

        }
    }

    [Benchmark]
    public void ThrowStaticException_RethrowExplicit()
    {
        try
        {
            try
            {
                throw ExpectedException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        catch
        {

        }
    }
}

[ShortRunJob]
public class TryCatchBenchmarks
{
    private static readonly InvalidOperationException ExpectedException =
        new InvalidOperationException("Expected exception.");

    [Params(1000)]
    public int Iterations;

    [Benchmark]
    public void ThrowStaticException_CatchAll_NoCapture()
    {
        for (int i = 0; i < Iterations; i++)
        {
            try
            {
                throw ExpectedException;
            }
            catch
            {

            }
        }
    }

    [Benchmark]
    public void ThrowStaticException_CatchAll_WithCapture()
    {
        for (int i = 0; i < Iterations; i++)
        {
            try
            {
                throw ExpectedException;
            }
            catch (Exception ex)
            {

            }
        }
    }

    [Benchmark]
    public void ThrowStaticException_CatchSpecific_WithCapture()
    {
        for (int i = 0; i < Iterations; i++)
        {
            try
            {
                throw ExpectedException;
            }
            catch (InvalidOperationException ex)
            {

            }
        }
    }

    [Benchmark]
    public void ThrowNewException_CatchAll_NoCapture()
    {
        for (int i = 0; i < Iterations; i++)
        {
            try
            {
                throw new InvalidOperationException("Expected exception.");
            }
            catch
            {

            }
        }
    }

    [Benchmark]
    public void ThrowNewException_CatchAll_WithCapture()
    {
        for (int i = 0; i < Iterations; i++)
        {
            try
            {
                throw new InvalidOperationException("Expected exception.");
            }
            catch (Exception ex)
            {

            }
        }
    }

    [Benchmark]
    public void ThrowNewException_CatchSpecific_WithCapture()
    {
        for (int i = 0; i < Iterations; i++)
        {
            try
            {
                throw new InvalidOperationException("Expected exception.");
            }
            catch (InvalidOperationException ex)
            {

            }
        }
    }
}