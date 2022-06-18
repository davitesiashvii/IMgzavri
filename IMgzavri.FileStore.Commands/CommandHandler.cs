

using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.Shared.Domain.Models;
using Microsoft.Extensions.Options;
using SimpleSoft.Mediator;

namespace IMgzavri.FileStore.Commands
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand, Result> where TCommand : Command
    {
        protected readonly IFileStorageRepository Repository;
        protected readonly IFileProcessor FileProcessor;
        protected readonly IRecommendFileStorageSettingsGlobalSettings GlobalSettings;
        protected readonly string[] SupportedFormats;

        protected CommandHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor)
        {
            Repository = repository;
            FileProcessor = fileProcessor;
            GlobalSettings = globalSettings.Value;
            SupportedFormats = GlobalSettings.SupportedFormats.Split(',');
        }

        public abstract Task<Result> HandleAsync(TCommand cmd, CancellationToken ct);
    }
}