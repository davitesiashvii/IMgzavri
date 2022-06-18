using IMgzavri.Commands.Commands.Auth;
using IMgzavri.Commands.Models.ResultModels;
using IMgzavri.Domain.Models;
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

namespace IMgzavri.Commands.Handlers.Auth
{
    public class LoginUserCommandHandler : CommandHandler<LoginUserCommand>
    {

        public LoginUserCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(LoginUserCommand cmd, CancellationToken ct)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == cmd.Email && x.Password == cmd.Password);

            if (user == null)
            {
                return Result.Error("მეილი ან პაროლი არასწორია");
            }
            var authResult = Auth.GenerateToken(user);

            await context.RefreshTokens.AddAsync(authResult.RefreshToken);
            await context.SaveChangesAsync();


            var res = new AuthenticationResult(authResult.Token, authResult.RefreshToken.Token, true);
   

            var result = new Result();
            result.Response = res;
            return result;
        }
    }
}
