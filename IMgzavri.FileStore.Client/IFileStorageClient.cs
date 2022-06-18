

using IMgzavri.FileStore.Client.Models;
using IMgzavri.Shared.Domain.Models;
using IMgzavri.Shared.Services;

namespace IMgzavri.FileStore.Client
{
    public interface IFileStorageClient : IHttpClientService
    {
        Task<Result> UploadFilesBytesAsync(List<FileSavingModel> files);

        Task<Result> UploadFileBytesAsync(FileSavingModel file);

        Task<Result> LoadFilePhysicalPathAsync(Guid id);

        Task<Result> LoadFilesPhysicalPathsAsync(List<Guid> ids);

        Task<Result> LoadFileBytesAsync(Guid id);

        Task<Result> DeleteFileAsync(Guid id);

        Task<Result> ExistsAsync(Guid id);
    }
}
