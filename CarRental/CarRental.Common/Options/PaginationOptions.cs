namespace CarRental.Common.Options
{
    public class PaginationOptions
    {
        public const string SectionName = "Pagination";
        public int MaxAvailablePageSize { get; set; }
        public int DefaultPageSize { get; set; }
    }
}