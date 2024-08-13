namespace Customize.Domain.DataObject.Query
{
    public class CustomerQueryParam
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Limit { get; set; } = 30;
    }
}
