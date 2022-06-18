using IMgzavri.FileStore.Commands.Commands;
using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.Shared.Domain.Models;
using Microsoft.Extensions.Options;


namespace IMgzavri.FileStore.Commands.CommandHandlers
{
    public class DeleteFileCommandHandler : CommandHandler<DeleteFileCommand>
    {
        public DeleteFileCommandHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor) : base(repository, globalSettings, fileProcessor)
        {
        }

        public override async Task<Result> HandleAsync(DeleteFileCommand cmd, CancellationToken ct)
        {
            var file = await Repository.LoadFileByIdAsync(cmd.FileId, ct);

            if (file == null)
            {
                return new Result("File not found", ResultStatus.NotFound);
            }

            //TODO needs delete user Id
            file.Delete(Guid.NewGuid());

            await Repository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
