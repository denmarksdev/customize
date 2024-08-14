namespace Customize.Domain.DataObject.Query
{
    public class QueryResult<T>
    {
        public List<T> Items { get; set; } = [];
        public bool HasMore => LastKey != null && LastKey != "{}";
        public string? LastKey { get; set; }
    }
}