using IMgzavri.Commands.Commands.Statment;
using IMgzavri.Domain.Models;
using IMgzavri.FileStore.Client;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Handlers.Statment
{
    public class CreateStatmentCommandHandler : CommandHandler<CreateStatmentCommand>
    {
        public CreateStatmentCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(CreateStatmentCommand cmd, CancellationToken ct)
        {
            var userId = Auth.GetCurrentUserId();
            var statment = new Statement()
            {
                Id = Guid.NewGuid(),
                CarId = cmd.CarId,
                Description = cmd.Description,
                CreatedDate = DateTime.Now,
                Seat = cmd.Seat,
                Price = cmd.Price,
                RouteToId = cmd.RouteToId,
                RoutFromId = cmd.RoutFromId,
                DateFrom = cmd.DateFrom,
                DateTo = cmd.DateTo,
                IsComplited = false,
                CreateUserId = userId,
            };
            await context.Statements.AddAsync(statment);
            await context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
