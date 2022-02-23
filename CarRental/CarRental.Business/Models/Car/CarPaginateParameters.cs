using CarRental.Common.Options;
using Microsoft.Extensions.Options;

namespace CarRental.Business.Models.Car
{
    public class CarPaginateParameters
    {
        private readonly int _maxPageSize;
        public int PageNumber { get; set; } = 1;
        private int _pageSize;

        public CarPaginateParameters(IOptions<PaginationOptions> paginationOptions)
        {
            var paginationOptionsValue = paginationOptions.Value;
            _maxPageSize = paginationOptionsValue.MaxAvailablePageSize;
            _pageSize = paginationOptionsValue.DefaultPageSize;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }
    }
}
