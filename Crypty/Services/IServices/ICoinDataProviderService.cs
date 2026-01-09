using Crypty.Models.DataModels;

namespace Crypty.Services.IServices
{
    public interface ICoinDataProviderService
    {
        public Task<List<CoinPreview>?> GetTopPopularCoinPreviewsAsync(int number);

        public Task<CoinDetails?> GetCoinDataById(string coinId);

        public string GetCoinHistory(string coinId, int days);
    }
}
