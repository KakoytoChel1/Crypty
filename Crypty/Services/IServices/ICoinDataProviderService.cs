using Crypty.Models.DataModels;

namespace Crypty.Services.IServices
{
    public interface ICoinDataProviderService
    {
        public Task<List<CoinPreview>?> GetTopPopularCoinPreviewsAsync(int number);

        public Task<CoinDetails?> GetCoinDataByIdAsync(string coinId);

        public Task<List<HistoryPoint>?> GetCoinHistory(string coinId, int days);
    }
}
