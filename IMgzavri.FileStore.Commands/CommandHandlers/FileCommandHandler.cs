using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.Shared.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace IMgzavri.FileStore.Commands.CommandHandlers
{
    public abstract class FileCommandHandler<TCommand> : CommandHandler<TCommand> where TCommand : Command
    {
        protected FileCommandHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor) : base(repository, globalSettings, fileProcessor)
        {
        }

        protected Result Rollback(params string[] savedFilePaths)
        {
            FileProcessor.DeleteFilesIfExist(savedFilePaths.ToArray());

            throw new FileStorageException("Files can't be saved! Operation is aborted", ExceptionLevel.Fatal);
        }

        protected bool IsFormatSupported(string extension) => SupportedFormats.Contains(extension);
    }
}
