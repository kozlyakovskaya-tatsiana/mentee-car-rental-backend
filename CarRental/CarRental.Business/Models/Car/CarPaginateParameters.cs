using CarRental.Common.Options;

namespace CarRental.Business.Models.Car
{
    public class CarPaginateParameters
    {
        private readonly int _maxPageSize = BasePaginationOptions.MaxAvailablePageSize;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = BasePaginationOptions.DefaultPageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }
    }
}