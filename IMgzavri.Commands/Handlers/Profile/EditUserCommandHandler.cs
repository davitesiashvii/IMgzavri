using IMgzavri.Commands.Commands.Profile;
using IMgzavri.Commands.Extensions;
using IMgzavri.Commands.Models.ResponceModels;
using IMgzavri.Domain.Models;
using IMgzavri.FileStore.Client;
using IMgzavri.FileStore.Client.Models;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Handlers.Profile
{
    public class EditUserCommandHandler : CommandHandler<EditUserCommand>
    {
        public EditUserCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(EditUserCommand cmd, CancellationToken ct)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=>x.Id == cmd.userId);

            if (user == null)
                return Result.Error("ოპერაციის შესრტულების დროს მოხდა შეცდომა");

            FileUploadResult res = null;         

            try
            {
                var fileSavingModel = new FileSavingModel(cmd.Photo.Name, cmd.Photo.Extension, cmd.Photo.ContentType, cmd.Photo.Size, Convert.FromBase64String(cmd.Photo.File), cmd.userId, cmd.userId);

                res = await FileStorage.UploadFile(fileSavingModel);
            }
            catch { }

            user.IdNumber = cmd.IdNumber;
            user.NumberLicense = cmd.NumberLicense;
            user.VerifyUser = true;
            user.PhotoId = res == null ? null : res.FileId;

            context.Users.Update(user);
            return Result.Success();
        }
    }
}
