using IMgzavri.FileStore.Client;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Queries.Queries.Car;
using IMgzavri.Queries.ViewModels.Car;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Car
{
    public class GetCarMarcksQueryHandler : QueryHandler<GetCarMarcksQuery>
    {
        public GetCarMarcksQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageClient fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetCarMarcksQuery query, CancellationToken ct)
        {
            var carMarcks = context.CarMarcks.ToList();

            if(!carMarcks.Any())
                return Result.Error("dfd");

            var res = new List<CarMarckVm>();
            carMarcks.ForEach(x =>
            {
                if (x.ManufacturerId == null && x.Type == 1)
                {
                    var models = new List<CarMarckVm>();

                    var marks = carMarcks.Where(z => x.Id == z.ManufacturerId);
                    if (marks.Any()) {
                        models.AddRange(marks.Select(z => new CarMarckVm()
                        {
                            Id = z.Id,
                            Name = z.Name,
                            ManufacturerId = z.ManufacturerId,
                            IsManufacturer = false,
                            Models = null
                        }).ToList());
                    }
                   
                    res.Add(new CarMarckVm()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        IsManufacturer = true,
                        Models = models
                    });
                    
                }
            });

            var result = new Result();

            result.Response = res;

            return result;


        }
    }
}
