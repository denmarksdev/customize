namespace Customize.Domain.DataObject
{
    public class Result
    {
        public const string UnmanagedError = "Erro não gerênciado";

        public bool Success { get; set; }
        public Dictionary<string, string> Errors { get; set; } = [];
        public Exception? Exception { get; set; }
    }

    public class Result<T> : Result
    {
        public T? Data  { get; set; }
    }
}
