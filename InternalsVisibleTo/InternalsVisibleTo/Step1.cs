namespace InternalsVisibleTo
{
    public sealed class OurPublicClass_Step1
    {
        public int OurPublicMethod(int input)
        {
            // a contrived example, but we take
            // the input, pass it to the private
            // method along with some constant,
            // and get the result
            var result = OurPrivateMethod(input, 5);
            return result;
        }

        private int OurPrivateMethod(
            int num1, 
            int num2)
        {
            // this is obviously a very (poorly)
            // contrived example just to try and
            // demonstrate the point where we
            // might have some private method that
            // we want to explore how to test
            var result = num1 * num2;
            return result;
        }
    }
}