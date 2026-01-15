using Crypty.Models.DataModels;

namespace Crypty.Services.IServices
{
    public interface ICoinDataProviderService
    {
        /// <summary>
        /// Retrieves a list of previews for the top popular coins, limited to the specified number of
        /// results
        /// </summary>
        /// <param name="number">The maximum number of coin previews to return. Must be greater than zero.</param>
        public Task<List<CoinPreview>?> GetTopPopularCoinPreviewsAsync(int number);

        /// <summary>
        /// Retrieves detailed information for a cryptocurrency specified by its unique id
        /// </summary>
        /// <param name="coinId">The unique identifier of the cryptocurrency to retrieve. Cannot be null or empty.</param>
        public Task<CoinDetails?> GetCoinDataByIdAsync(string coinId);

        /// <summary>
        /// Retrieves the historical price data for a specified coin over a given number of days
        /// </summary>
        /// <param name="coinId">The unique identifier of the coin for which to retrieve history. Cannot be null or empty.</param>
        /// <param name="days">The number of days of historical data to retrieve. Must be greater than zero.</param>
        public Task<List<HistoryPoint>?> GetCoinHistory(string coinId, int days);

        /// <summary>
        /// Checks the ability to send requests to service
        /// </summary>
        public Task<bool> Ping();
    }
}
