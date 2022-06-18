using System.Threading;
using System.Threading.Tasks;
using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.Shared.Domain.Models;
using Microsoft.Extensions.Options;
using SimpleSoft.Mediator;

namespace IMgzavri.FileStore.Queries
{
    public abstract class QueryHandler<TQuery> : IQueryHandler<TQuery, Result> where TQuery : Query
    {
        protected readonly IFileStorageRepository Repository;
        protected readonly IFileProcessor FileProcessor;
        protected readonly IRecommendFileStorageSettingsGlobalSettings GlobalSettings;
        protected readonly string[] SupportedFormats;

        protected QueryHandler(IFileStorageRepository repository, IOptions<IRecommendFileStorageSettingsGlobalSettings> globalSettings, IFileProcessor fileProcessor)
        {
            Repository = repository;
            FileProcessor = fileProcessor;
            GlobalSettings = globalSettings.Value;
            SupportedFormats = GlobalSettings.SupportedFormats.Split(',');
        }

        public abstract Task<Result> HandleAsync(TQuery query, CancellationToken ct);
    }
}