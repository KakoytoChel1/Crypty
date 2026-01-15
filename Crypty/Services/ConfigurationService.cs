using Crypty.Services.IServices;
using System.IO;
using System.Text.Json;

namespace Crypty.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private const string _configurationFilePath = "appConfig.json";
        private Dictionary<string, string>? _settings;

        public bool IsInitialized { get; private set; } = false;

        public ConfigurationService()
        {
            if (File.Exists(_configurationFilePath))
            {
                LoadDataFromConfiguration();
            }
            else
            {
                CreateDefaultConfigurationFile();
            }

            IsInitialized = true;
        }

        public T? Get<T>(string key, T? defaultValue = default)
        {
            if (!IsInitialized)
                return defaultValue;

            if (_settings!.TryGetValue(key, out var value))
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        public async Task Set<T>(string key, T value)
        {
            if (!IsInitialized)
                return;

            _settings![key] = value?.ToString() ?? string.Empty;
            await SaveDataIntoConfigurationAsync();
        }

        /// <summary>
        /// Asynchronously saves the current settings to the configuration file in JSON format.
        /// </summary>
        private async Task SaveDataIntoConfigurationAsync()
        {
            var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(_configurationFilePath, json);
        }

        /// <summary>
        /// Saves the current settings to the configuration file in JSON format.
        /// </summary>
        private void SaveDataIntoConfiguration()
        {
            var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_configurationFilePath, json);
        }

        /// <summary>
        /// Loads application settings from the configuration file. If the configuration file is missing or invalid,
        /// default settings are created
        /// </summary>
        private void LoadDataFromConfiguration()
        {
            try
            {
                var json = File.ReadAllText(_configurationFilePath);
                _settings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
            catch
            {
                CreateDefaultConfigurationFile();
            }
            finally
            {
                if (_settings == null)
                {
                    CreateDefaultConfigurationFile();
                }
            }
        }

        /// <summary>
        /// Creates a default configuration file with initial settings for the application.
        /// </summary>
        private void CreateDefaultConfigurationFile()
        {
            _settings = new Dictionary<string, string>
                {
                    {"provider_url", "https://api.coingecko.com/api/v3/"},
                    {"api_key", "!!__YOUR_API_KEY__!!"},
                    {"theme", "dark" }
                };

            SaveDataIntoConfiguration();
        }
    }
}
