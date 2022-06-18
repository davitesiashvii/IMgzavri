using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IMgzavri.FileStore.Domain
{
    public interface IFileStorageRepository
    {
        Task<File> LoadFileByIdAsync(Guid id, CancellationToken ct);
        Task<File> LoadFileByNameAsync(string name, CancellationToken ct);
        Task<File> LoadFileAsync(Expression<Func<File, bool>> expression, CancellationToken ct);
        IQueryable<File> LoadFiles(Expression<Func<File, bool>> expression);
        void SaveFiles(params File[] file);
        Task<int> SaveChangesAsync();
    }
}