using Xunit;

namespace InternalsVisibleTo.UnitTests
{
    public sealed class TheClassWeAreKeepingInternalTests
    {
        [Fact]
        private void DependencyMethod_2And3_ResultIs6()
        {
            TheClassWeAreKeepingInternal systemUnderTest = new();
            var result = systemUnderTest.DependencyMethod(2, 3);
            Assert.Equal(6, result);
        }
    }
}