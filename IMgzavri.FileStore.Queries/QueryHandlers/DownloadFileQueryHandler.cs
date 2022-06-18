using System.Threading;
using System.Threading.Tasks;
using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.FileStore.Queries.Queries;
using IMgzavri.Shared.Domain.Models;
using Microsoft.Extensions.Options;


namespace IMgzavri.FileStore.Queries.QueryHandlers
{
    public class DownloadFileQueryHandler : FileQueryHandler<DownloadFileQuery>
    {
        public DownloadFileQueryHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor) : base(repository, globalSettings, fileProcessor)
        {
        }

        public override async Task<Result> HandleAsync(DownloadFileQuery query, CancellationToken ct)
        {
            var file = await Repository.LoadFileByIdAsync(query.FileId, ct);

            var checkingResult =  FileExists(file);

            if (!checkingResult.Exists)
                return new Result("File not found", ResultStatus.NotFound);

            var fileResult = await FileProcessor.DownloadAsync(checkingResult.Path);

            var result = Result.Success();
            result.Parameters.Add("stream", fileResult);
            result.Parameters.Add("type", checkingResult.File.ContentType);
            result.Parameters.Add("name", checkingResult.File.Name + checkingResult.File.Extension);

            return result;
        }
    }
}
