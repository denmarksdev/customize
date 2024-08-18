using Customize.Domain.DataObject.Query;

namespace Customize.Contracts.Customer
{
    public class ListCustomerRequest(DateRangeQueryParam dateRange)
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Limit { get; set; } = 30;
        public DateRangeQueryParam DateRange { get; set; } = dateRange;
        public string? PaginationToken { get; set; }
    }
}
