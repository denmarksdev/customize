namespace Customize.Domain.DataObject
{
    public class Result
    {
        public const string UnmanagedError = "Erro não gerênciado";

        public bool Sucess { get; set; }
        public Dictionary<string, string> Errors { get; set; } = [];
        public Exception? Exception { get; set; }
    }
}
