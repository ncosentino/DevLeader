namespace SocialMediaAssistant
{
    public interface IOptionsProvider
    {
        public T GetOptions<T>();
    }
}