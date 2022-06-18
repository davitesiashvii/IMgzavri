using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Helpers;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.FileStore.Queries.Models;
using IMgzavri.FileStore.Queries.Queries;
using IMgzavri.Shared.Constants;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace IMgzavri.FileStore.Queries.QueryHandlers
{
    public class GetFilesPhysicalPathsQueryHandler : FileQueryHandler<GetFilesPhysicalPathsQuery>
    {
        public GetFilesPhysicalPathsQueryHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor) : base(repository, globalSettings, fileProcessor)
        {
        }

        public override async Task<Result> HandleAsync(GetFilesPhysicalPathsQuery query, CancellationToken ct)
        {
            var files = await Repository.LoadFiles(x => query.FileIds.Contains(x.Id)).ToListAsync(ct);

            var fileStoreLinkResults = files.Select(x => new FileStoreLinkResult(
                x.CorrelationId,
                FileHelper.BuildPathForFileServer(x, GlobalSettings.FileServerRequestPath, GlobalSettings.ApiUrl)));

            var result = Result.Success();

            result.Parameters.Add(FileStorageConstants.GetFilesPhysicalPathsResultName, fileStoreLinkResults);

            return result;
        }
    }
}
