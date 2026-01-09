namespace Crypty.Services.IServices
{
    public interface IConfigurationService
    {
        T? Get<T>(string key, T? defaultValue = default);
        Task Set<T>(string key, T value);
    }
}
