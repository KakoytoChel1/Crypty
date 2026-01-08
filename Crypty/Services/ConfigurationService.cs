using Crypty.Services.IServices;

namespace Crypty.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public ConfigurationService()
        {

        }

        public Task InitAsync()
        {
            throw new NotImplementedException();
        }

        public T? Get<T>(string key, T? defaultValue = default)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}
