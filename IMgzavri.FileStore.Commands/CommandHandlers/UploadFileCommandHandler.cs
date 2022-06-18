

using IMgzavri.FileStore.Commands.Commands;
using IMgzavri.FileStore.Commands.Models;
using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Helpers;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.Shared.Constants;
using IMgzavri.Shared.Domain.Models;
using Microsoft.Extensions.Options;

namespace IMgzavri.FileStore.Commands.CommandHandlers
{
    public class UploadFileCommandHandler : FileCommandHandler<UploadFileCommand>
    {
        public UploadFileCommandHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor) : base(repository, globalSettings, fileProcessor)
        {
        }

        public override async Task<Result> HandleAsync(UploadFileCommand cmd, CancellationToken ct)
        {
            if (IsFormatSupported(cmd.File.Extension))
                throw new FileStorageException("Unsupported format detected!", ExceptionLevel.Fatal);

            var savedFilePaths = new List<string>();
            var savedFileIds = new List<Guid>();

            var fileToSave = IMgzavri.FileStore.Domain.File.Create(cmd.File.Name, cmd.File.Extension, cmd.File.ContentType, cmd.File.Size, cmd.File.CreatorId, cmd.File.CorrelationId);

            var savingPath = FileHelper.BuildPath(fileToSave, GlobalSettings.FileSystemBasePath, GlobalSettings.MainFolderName);

            if (!FileHelper.Exists(savingPath))
            {
                var isSuccess = await FileProcessor.SaveToFileSystemAsync(cmd.File.File, savingPath);

                if (!isSuccess)
                {
                    if (savedFilePaths.Count > 0)
                        FileProcessor.DeleteFilesIfExist(savedFilePaths.ToArray());

                    throw new FileStorageException($"File: '{cmd.File.Name}' can't be saved! Operation is aborted", ExceptionLevel.Fatal);
                }

                savedFilePaths.Add(savingPath);
                savedFileIds.Add(fileToSave.Id);

                Repository.SaveFiles(fileToSave);
            }

            var result = await Repository.SaveChangesAsync();

            if (result == 0)
                return Rollback(savedFilePaths.ToArray());

            var resultToReturn = Result.Success();

            var fileSavingResult = new FileSavingResult(savedFileIds[0], fileToSave.CorrelationId);

            resultToReturn.Parameters.Add(FileStorageConstants.UploadedFileResultName, fileSavingResult);

            return resultToReturn;
        }
    }
}
