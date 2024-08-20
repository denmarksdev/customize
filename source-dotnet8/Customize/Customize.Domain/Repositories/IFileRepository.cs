using Customize.Domain.DataObject;
using Customize.Domain.DataObject.File;

namespace Customize.Domain.Repositories
{
    public interface IFileRepository
    {
        Task<Result> PutAsync(PutFile input);
    }
}
