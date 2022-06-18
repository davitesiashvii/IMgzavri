using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Helpers;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.FileStore.Queries.Models;
using IMgzavri.FileStore.Queries.Queries;
using IMgzavri.Shared.Constants;
using IMgzavri.Shared.Domain.Models;
using Microsoft.Extensions.Options;

namespace IMgzavri.FileStore.Queries.QueryHandlers
{
    public class GetFilePhysicalPathQueryHandler : FileQueryHandler<GetFilePhysicalPathQuery>
    {
        public GetFilePhysicalPathQueryHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor) : base(repository, globalSettings, fileProcessor)
        {
        }

        public override async Task<Result> HandleAsync(GetFilePhysicalPathQuery query, CancellationToken ct)
        {
            var file = await Repository.LoadFileByIdAsync(query.FileId, ct);

            var checkResult = FileExists(file);

            if (!checkResult.Exists)
                return Result.Error("File not found");

            var result = Result.Success();

            var fileStoreLinkResult = new FileStoreLinkResult(file.CorrelationId, FileHelper.BuildPathForFileServer(file, GlobalSettings.FileServerRequestPath, GlobalSettings.ApiUrl));

            result.Parameters.Add(FileStorageConstants.GetFilePhysicalPathResultName, fileStoreLinkResult);

            return result;
        }
    }
}
