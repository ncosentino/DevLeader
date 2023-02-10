using CommandLine;

namespace SocialMediaAssistant
{
    public sealed class CommandLineOptionsProvider : IOptionsProvider
    {
        private readonly string[] _cliArgs;
        private readonly Parser _cliParser;

        public CommandLineOptionsProvider(
            string[] cliArgs,
            Parser cliParser)
        {
            _cliArgs = cliArgs;
            _cliParser = cliParser;
        }

        public T GetOptions<T>()
        {
            var result = _cliParser.ParseArguments<T>(_cliArgs);
            if (result.Errors.Any())
            {
                throw new InvalidOperationException(string.Join("\r\n", result.Errors));
            }

            return result.Value;
        }
    }
}