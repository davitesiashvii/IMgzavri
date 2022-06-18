using IMgzavri.FileStore.Client;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Queries.Extension;
using IMgzavri.Queries.Queries.Car;
using IMgzavri.Queries.ViewModels;
using IMgzavri.Queries.ViewModels.Car;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Car
{
    public class GetCarQueryHandle : QueryHandler<GetCarQuery>
    {
        public GetCarQueryHandle(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetCarQuery query, CancellationToken ct)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == query.Id);

            if (car == null)
                return Result.Error("");

            var carVm = new CarVM()
            {
                Id = car.Id,
                Manufacturer = context.CarMarcks.FirstOrDefault(z => z.Id == car.ManufacturerId).Name,
                Model = context.CarMarcks.FirstOrDefault(z => z.Id == car.ModelId).Name,
                CreatedDate = car.CreateDate,
                mainImageLink = this.GetImagelink(car.UserId),
                Images = this.GetImagelinks(car.UserId)
            };

            var result = new Result();
            result.Response = carVm;
            return result;
        }


        private string GetImagelink(Guid userId)
        {
            FileStoreLinkResult fmRes = null;
            try
            {
                fmRes = FileStorage.GetFilePhysicalPath(context.Users.FirstOrDefault(x => x.Id == userId).PhotoId.Value).Result;
            }
            catch { return null; }

            return fmRes.Link;
        }

        private List<string> GetImagelinks(Guid carId)
        {
            var fmRes = new List<FileStoreLinkResult>();
            try
            {
                fmRes = FileStorage.GetFilesPhysicalPaths(context.CarImages.Where(x => x.Id == carId).Select(z => z.ImageId).ToList()).Result;
            }
            catch { return null; }

            return fmRes.Select(x => x.Link).ToList();
        }
    }
}
