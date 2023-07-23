namespace InternalsVisibleTo
{
    public sealed class OurPublicClass_Step3
    {
        private readonly IDependency _dependency;

        public OurPublicClass_Step3(IDependency dependency)
        {
            _dependency = dependency;
        }

        public int OurPublicMethod(int input)
        {
            var result = _dependency.DependencyMethod(input, 5);
            return result;
        }
    }

    public interface IDependency
    {
        int DependencyMethod(
            int num1,
            int num2);
    }

    internal sealed class TheClassWeAreKeepingInternal 
        : IDependency
    {
        public int DependencyMethod(
            int num1,
            int num2)
        {
            var result = num1 * num2;
            return result;
        }
    }
}