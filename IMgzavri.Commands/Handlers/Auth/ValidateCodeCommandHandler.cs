using IMgzavri.Commands.Commands.Auth;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IMgzavri.Commands.Handlers.Auth
{
    public class ValidateCodeCommandHandler : CommandHandler<ValidateCodeCommand>
    {
        public ValidateCodeCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth) : base(context, auth)
        {
        }

        public override async Task<Result> HandleAsync(ValidateCodeCommand cmd, CancellationToken ct)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == cmd.Email);

            if (user == null)
                return Result.Error("დაფიქსირდა შეცდომა");
            if (user.RendomCode != cmd.Code)
                return Result.Error("კოდი არასწორია");

            return Result.Success();
        }
    }
}
