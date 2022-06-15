using IMgzavri.Commands.Commands.Auth;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using IMgzavri.Shared.ExternalServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Handlers.Auth
{
    public class VertifyEmailAndSendValidateCodeCommandHandler : CommandHandler<VertifyEmailAndSendValidateCodeCommand>
    {
        private readonly IMailService _mailService;
        public VertifyEmailAndSendValidateCodeCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IMailService mailService) : base(context, auth)
        {
            _mailService = mailService;
        }

        public override async Task<Result> HandleAsync(VertifyEmailAndSendValidateCodeCommand cmd, CancellationToken ct)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=>x.Email == cmd.Email);

            if (user == null)
                return Result.Error("მეილი არ არის რეგისტრილებული");

            var code = RendomStringGenerators.RandomCode(4);

            await _mailService.SendEmailAsync(new MailRequest()
            {
                Body = $"<p>ვერტიფიკაციის კოდი - {code}</p>",
                Mail = cmd.Email
            });

            user.RendomCode = int.Parse(code);

            context.Update(user);
            await context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
