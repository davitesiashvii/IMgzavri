using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Helpers;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.FileStore.Queries.Models;
using IMgzavri.Shared.Domain.Models;
using Microsoft.Extensions.Options;

namespace IMgzavri.FileStore.Queries.QueryHandlers
{
    public abstract class FileQueryHandler<TQuery> : QueryHandler<TQuery> where TQuery : Query
    {
        protected FileQueryHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor) : base(repository, globalSettings, fileProcessor)
        {
        }

        protected CheckResult FileExists(IMgzavri.FileStore.Domain.File file)
        {
            var result = new CheckResult
            {
                Exists = true
            };

            result.File = file;

            if (file == null)
            {
                result.Exists = false;
            }

            var path = FileHelper.BuildPath(file, GlobalSettings.FileSystemBasePath, GlobalSettings.MainFolderName);

            if (!FileHelper.Exists(path))
            {
                result.Exists = false;
            }

            result.Path = path;

            return result;
        }
    }
}
