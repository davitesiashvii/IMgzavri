using IMgzavri.Commands.Commands.Statment;
using IMgzavri.FileStore.Client;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Handlers.Statment
{
    public class EditStatmrentCommandHandler : CommandHandler<EditStatmrentCommand>
    {
        public EditStatmrentCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(EditStatmrentCommand cmd, CancellationToken ct)
        {
            var statment = await context.Statements.FirstOrDefaultAsync(x=>x.Id == cmd.Id && x.CreateUserId == cmd.CreatorUserId);
            if (statment == null)
                return Result.Error("დაფიქსირდა შეცდომა");
            statment.Description = cmd.Description;
            statment.Seat = cmd.Seat;
            statment.Price = cmd.Price;
            statment.CreatedDate = DateTime.Now;
            statment.RouteToId = cmd.RouteToId;
            statment.RouteToId = cmd.RouteToId;
            statment.DateFrom = cmd.DateFrom;
            statment.DateTo = cmd.DateTo;  
            statment.IsComplited = cmd.IsComplited == null ? false : cmd.IsComplited;

            context.Statements.Update(statment);
            await context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
