using System;
using Moq;
using Xunit;

namespace UnitTestSimpleSystem.Tests
{
    public class ConsoleCalculatorTests
    {
        private readonly MockRepository _mockRepository;
        private readonly ConsoleCalculator _consoleCalculator;
        private readonly Mock<IConsole> _console;

        public ConsoleCalculatorTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _console = _mockRepository.Create<IConsole>();
            _consoleCalculator = new ConsoleCalculator(_console.Object);
        }

        [Fact]
        public void Add_Add1And2_Returns3AndPrintsToConsole()
        {
            // arrange (setup)
            _console
                .Setup(x => x.WriteLine("1+2=3"));

            // act (execute the thing)
            var result = _consoleCalculator.Add(1, 2);

            // assert (check your results)
            Assert.Equal(3, result);
            _mockRepository.VerifyAll();
        }
    }
}
