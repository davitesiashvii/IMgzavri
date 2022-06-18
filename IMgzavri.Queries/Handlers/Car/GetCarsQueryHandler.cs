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
    public class GetCarsQueryHandler : QueryHandler<GetCarsQuery>
    {
        public GetCarsQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetCarsQuery query, CancellationToken ct)
        {

            var cars = await context.Cars.Where(x=>x.UserId == query.userId).ToListAsync();
            var resultCars = new List<CarVM>() { };
            var result = new Result();
            if (!cars.Any())
            {
                result.Response = resultCars;
                return result;
            }

            resultCars.AddRange(cars.Select(x => new CarVM
            {
                Id = x.Id,
                Manufacturer = context.CarMarcks.FirstOrDefault(z => z.Id == x.ManufacturerId).Name,
                Model = context.CarMarcks.FirstOrDefault(z => z.Id == x.ModelId).Name,
                CreatedDate = x.CreateDate,
                mainImageLink = this.GetImagelink(x.UserId),
                Images = this.GetImagelinks(x.UserId)
            }));
            result.Response = resultCars;
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
                fmRes = FileStorage.GetFilesPhysicalPaths(context.CarImages.Where(x => x.Id == carId).Select(z=>z.ImageId).ToList()).Result;
            }
            catch { return null; }

            return fmRes.Select(x=>x.Link).ToList();
        }




    }
}
