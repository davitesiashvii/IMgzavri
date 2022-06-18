using System;
using System.Linq;
using System.Linq.Expressions;

using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace IMgzavri.FileStore.Infrastructure.Repositories
{
    public class FileStorageRepository : IFileStorageRepository
    {
        private readonly IMgzavriFileStorageDbContext _context;

        public FileStorageRepository(IMgzavriFileStorageDbContext context)
        {
            _context = context;
        }

        public async Task<IMgzavri.FileStore.Domain.File> LoadFileByIdAsync(Guid id, CancellationToken ct)
        {
            return await LoadFileAsync(x => x.Id == id, ct);
        }

        public async Task<IMgzavri.FileStore.Domain.File> LoadFileByNameAsync(string name, CancellationToken ct)
        {
            return await LoadFileAsync(x => x.Name == name, ct);
        }

        public async Task<IMgzavri.FileStore.Domain.File> LoadFileAsync(Expression<Func<IMgzavri.FileStore.Domain.File, bool>> expression, CancellationToken ct)
        {
            return await _context.Files.FirstOrDefaultAsync(expression, ct);
        }

        public IQueryable<IMgzavri.FileStore.Domain.File> LoadFiles(Expression<Func<IMgzavri.FileStore.Domain.File, bool>> expression)
        {
            return _context.Files.Where(expression);
        }

        public void SaveFiles(params IMgzavri.FileStore.Domain.File[] files)
        {
            _context.Files.AddRange(files);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
