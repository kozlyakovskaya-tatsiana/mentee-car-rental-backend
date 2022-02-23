namespace CarRental.Common.Options
{
    public static class BasePaginationOptions
    {
        public static int MaxAvailablePageSize { get; set; } = 50;
        public static int DefaultPageSize { get; set; } = 10;
    }
}