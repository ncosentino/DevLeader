namespace InternalsVisibleTo
{
    public sealed class OurPublicClass_Step2
    {
        private readonly TheClassWeDontWantToExpose _dependency;

        public OurPublicClass_Step2(
            TheClassWeDontWantToExpose dependency)
        {
            _dependency = dependency;
        }

        public int OurPublicMethod(int input)
        {
            var result = _dependency.OurPrivateMethod(
                input, 
                5);
            return result;
        }
    }

    // obviously this isn't what we want :(
    // it's public now! but the tests
    // can see it!
    public sealed class TheClassWeDontWantToExpose
    {
        public int OurPrivateMethod(
            int num1,
            int num2)
        {
            var result = num1 * num2;
            return result;
        }
    }
}