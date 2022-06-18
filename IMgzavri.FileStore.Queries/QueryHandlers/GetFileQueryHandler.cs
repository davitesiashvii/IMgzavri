using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.FileStore.Queries.Models;
using IMgzavri.FileStore.Queries.Queries;
using IMgzavri.Shared.Constants;
using IMgzavri.Shared.Domain.Models;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
namespace IMgzavri.FileStore.Queries.QueryHandlers
{
    public class GetFileQueryHandler : FileQueryHandler<GetFileQuery>
    {
        public GetFileQueryHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor) : base(repository, globalSettings, fileProcessor)
        {
        }

        public override async Task<Result> HandleAsync(GetFileQuery query, CancellationToken ct)
        {
            var file = await Repository.LoadFileByIdAsync(query.FileId, ct);

            var checkingResult =  FileExists(file);

            if (!checkingResult.Exists)
                return new Result("File not found", ResultStatus.NotFound);

            var bytesResult = await FileProcessor.ReadFileAsync(checkingResult.Path);

            var fileModel = new FileModel(checkingResult.File, bytesResult);

            var result = Result.Success();
            result.Parameters.Add(FileStorageConstants.GetFileResultName, fileModel);

            return result;
        }
    }
}
