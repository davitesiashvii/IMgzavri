using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.Auth
{
    public record RegisterUserCommand(
        string Email,
        string Name,
        string MobileNumber,
        string IdNumber,
        string Password
        ) : Command;
}
