namespace Customize.Contracts.Base
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public Dictionary<string, string> Errors { get; set; } = [];
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
