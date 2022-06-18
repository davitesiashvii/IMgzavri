using IMgzavri.Commands.Commands.car;
using IMgzavri.Commands.Extensions;
using IMgzavri.Commands.Models.ResponceModels;
using IMgzavri.Domain.Models;
using IMgzavri.FileStore.Client;
using IMgzavri.FileStore.Client.Models;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;


namespace IMgzavri.Commands.Handlers.Car
{
    public class CreateCarCommandHandler : CommandHandler<CreateCarCommand>
    {
        public CreateCarCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(CreateCarCommand cmd, CancellationToken ct)
        {
            var userId = Auth.GetCurrentUserId();

            FileUploadResult res = null;

            try
            {
                var fileSavingModel = new FileSavingModel(cmd.MainImage.Name, cmd.MainImage.Extension, cmd.MainImage.ContentType, cmd.MainImage.Size, Convert.FromBase64String(cmd.MainImage.File), userId, userId);

                res = await FileStorage.UploadFile(fileSavingModel);
            }
            catch { }

            var carImages = new List<CarImage>();
            if (cmd.Images.Any())
            {
                var filesModel = new List<FileSavingModel>() { };
                var res2 = new List<FileUploadResult>();
                try
                {
                    cmd.Images.ForEach(x =>
                    {
                        filesModel.Add(new FileSavingModel(x.Name, x.Extension, x.ContentType, x.Size, Convert.FromBase64String(x.File), userId, userId));
                    });
                    res2 = await FileStorage.UploadFiles(filesModel);
                }
                catch { }
                carImages = res2.Select(x => new CarImage()
                {
                    Id = Guid.NewGuid(),
                }).ToList();
            }

            var car = new IMgzavri.Domain.Models.Car()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreateDate = DateTime.Now,
                ManufacturerId = cmd.ManufacturerId,
                ModelId = cmd.ModelId,
                CarImages = carImages,
            };
            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
