using IMgzavri.FileStore.Client;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Queries.Extension;
using IMgzavri.Queries.Queries.Statement;
using IMgzavri.Queries.ViewModels;
using IMgzavri.Queries.ViewModels.Statment;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Statement
{
    public class GetStatmentQueryHandler : QueryHandler<GetStatmentQuery>
    {
        public GetStatmentQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetStatmentQuery query, CancellationToken ct)
        {
            var userId = Auth.GetCurrentUserId();

            var statment = await context.Statements.FirstOrDefaultAsync(x=>x.CreateUserId == userId && x.Id == query.Id);

            if (statment == null)
                return Result.Error("დაფიქსირდა სისტემური შეცდომა");

            FileStoreLinkResult fmRes = null;
            try
            {
                fmRes = await FileStorage.GetFilePhysicalPath(statment.CreateUser.PhotoId.Value);
            }
            catch { }

            var str = new StatmentVm()
            {
                Id = statment.Id,
                CarId = statment.CarId,
                Description = statment.Description,
                CreateDate = statment.CreatedDate,
                Seat = statment.Seat,
                Price = statment.Price,
                RoutFrom = context.Cities.FirstOrDefault(x=>x.Id == statment.RoutFromId).Name,
                RouteTo = context.Cities.FirstOrDefault(x => x.Id == statment.RouteToId).Name,
                DateFrom = statment.DateFrom,
                DateTo = statment.DateTo,
                IsComplited = statment.IsComplited,
                CreateUserId = userId,
                ImageLink = fmRes == null ? null : fmRes.Link
            };
            var result = new Result();
            result.Response = str;
            return result;
        }
    }
}
