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
    public class UploadFilesCommandHandler : FileCommandHandler<UploadFilesCommand>
    {
        public UploadFilesCommandHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor) : base(repository, globalSettings, fileProcessor)
        {
        }

        public override async Task<Result> HandleAsync(UploadFilesCommand cmd, CancellationToken ct)
        {
            if (cmd.Files.All(x => IsFormatSupported(x.Extension)))
                throw new FileStorageException("Unsupported format detected!", ExceptionLevel.Fatal);

            var savedFilePaths = new List<string>();
            var savingResults = new List<FileSavingResult>();

            foreach (var file in cmd.Files)
            {
                var fileToSave = IMgzavri.FileStore.Domain.File.Create(file.Name, file.Extension, file.ContentType, file.Size, file.CreatorId, file.CorrelationId);

                var savingPath = FileHelper.BuildPath(fileToSave, GlobalSettings.FileSystemBasePath, GlobalSettings.MainFolderName);

                if (!FileHelper.Exists(savingPath))
                {
                    var isSuccess = await FileProcessor.SaveToFileSystemAsync(file.File, savingPath);

                    if (!isSuccess)
                    {
                        if (savedFilePaths.Count > 0)
                            FileProcessor.DeleteFilesIfExist(savedFilePaths.ToArray());

                        throw new FileStorageException($"File: '{file.Name}' can't be saved! Operation is aborted", ExceptionLevel.Fatal);
                    }

                    savedFilePaths.Add(savingPath);
                    savingResults.Add(new FileSavingResult(fileToSave.Id, fileToSave.CorrelationId));

                    Repository.SaveFiles(fileToSave);
                }
            }

            var result = await Repository.SaveChangesAsync();

            if (result == 0) 
                return Rollback(savedFilePaths.ToArray());

            var resultToReturn = Result.Success();

            resultToReturn.Parameters.Add(FileStorageConstants.UploadedFilesResultName, savingResults);

            return resultToReturn;
        }
    }
}
