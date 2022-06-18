using System.IO;
using System.Threading.Tasks;

namespace IMgzavri.FileStore.Infrastructure.Services
{
    public interface IFileProcessor
    { 
        Task<bool> SaveToFileSystemAsync(byte[] bytes, string path);

        Task<byte[]> ReadFileAsync(string path);

        Task<MemoryStream> DownloadAsync(string path);

        void DeleteFilesIfExist(params string[] paths);
    }
}
