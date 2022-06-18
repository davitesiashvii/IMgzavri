using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Queries.Car
{
    public record GetCarMarcksQuery(): Query;

    public record GetCarsQuery(Guid userId): Query;

    public record GetCarQuery(Guid carId): Query;
}
