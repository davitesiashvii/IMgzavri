using IMgzavri.Commands.Models.ResponceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.car
{
    public record CreateCarCommand(
        Guid ManufacturerId,
        Guid ModelId,
        SaveFileModel MainImage,
        List<SaveFileModel> Images
        ) : Command;

    public record UpdateCarCommand
        (
        Guid Id,
        Guid ManufacturerId,
        Guid ModelId,
        SaveFileModel MainImage,
        List<SaveFileModel> Images
        ):Command;

    public record DeleteCarCommand(
        Guid Id
        ) : Command;

}
