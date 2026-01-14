using Crypty.ViewModels.Tools;

namespace Crypty.Models.DataModels
{
    public class HistoryPoint : ObservableObject
    {
        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }
    }
}
