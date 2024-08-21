namespace Customize.Domain.DataObject.Query
{
    public class DateRangeQueryParam(DateTime start, DateTime end)
    {
        public DateTime Start { get; set; } = start;
        public DateTime End { get; set; } = end;
    }
}