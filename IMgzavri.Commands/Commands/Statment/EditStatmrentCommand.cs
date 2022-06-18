using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.Statment
{
    public record EditStatmrentCommand(
        Guid Id,
        Guid CreatorUserId,
        Guid CarId,
        string? Description,
        int Seat,
        Double Price,
        Guid RoutFromId,
        Guid RouteToId,
        DateTime DateFrom,
        DateTime DateTo,
        bool? IsComplited
        ) : Command;
}
