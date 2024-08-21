namespace Customize.Domain.DataObject.File
{
    public class PutFile
    {
        public string Key { get; set; }
        public string BucketName  { get; set; }
        public string Message { get; set; }

        public PutFile(string key, string bucketName, string message)
        {
            Key = key;
            BucketName = bucketName;
            Message = message;
        }
    }
}
