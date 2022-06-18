using IMgzavri.Shared.Domain.Models;
using SimpleSoft.Mediator;
using System;

namespace IMgzavri.FileStore.Commands
{
    public class Command : ICommand<Result>
    {
        public Guid Id { get; }

        public DateTimeOffset CreatedOn { get; }

        public string CreatedBy { get; }

        public Command()
        {
            //TODO for later
            Id = Guid.NewGuid();
            CreatedOn = DateTimeOffset.UtcNow;
            CreatedBy = "kapitani taxmagido";
        }
    }
}