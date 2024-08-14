namespace Customize.Domain.DataObject.Query
{
    public class CustomerQueryParam(DateRangeQueryParam dateRange)
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Limit { get; set; } = 30;
        public DateRangeQueryParam DateRange { get; set; } = dateRange;
        public string? PaginationToken { get; set; }

        public bool HasId => !string.IsNullOrWhiteSpace(Id);
        public bool HasName => !string.IsNullOrWhiteSpace(Name);
    }
}