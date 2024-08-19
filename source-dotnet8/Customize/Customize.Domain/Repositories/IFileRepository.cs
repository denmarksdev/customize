using Customize.Domain.DataObject;

namespace Customize.Domain.Repositories
{
    public interface IFileRepository
    {
        Task<Result> PutAsync(string key, string bucketName, string message);
    }
}
