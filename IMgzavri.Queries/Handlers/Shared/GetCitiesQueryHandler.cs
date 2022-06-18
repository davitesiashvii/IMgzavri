using IMgzavri.FileStore.Client;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Queries.Queries.Shared;
using IMgzavri.Queries.ViewModels.Shared;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Shared
{
    public class GetCitiesQueryHandler : QueryHandler<GetCitiesQuery>
    {
        public GetCitiesQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetCitiesQuery query, CancellationToken ct)
        {
            var cities = context.Cities.Select(c => new CityVm()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            var result = new Result();
            result.Response = cities;
            return result;
        }
    }
}
