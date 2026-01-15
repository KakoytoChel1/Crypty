namespace Crypty.Services.IServices
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Retrieves the value associated with the specified key, or returns the provided default value if the key is
        /// not found
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="key">The key whose associated value is to be retrieved. Cannot be null.</param>
        /// <param name="defaultValue">The value to return if the specified key does not exist. If not provided, the default value for type
        T? Get<T>(string key, T? defaultValue = default);

        /// <summary>
        /// Stores the specified value under the given key
        /// </summary>
        /// <typeparam name="T">The type of the value to store.</typeparam>
        /// <param name="key">The key under which the value will be stored. Cannot be null or empty.</param>
        /// <param name="value">The value to store. May be null for reference types.</param>
        Task Set<T>(string key, T value);
    }
}
