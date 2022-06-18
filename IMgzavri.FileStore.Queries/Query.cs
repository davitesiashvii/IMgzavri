
using IMgzavri.Shared.Domain.Models;
using SimpleSoft.Mediator;
using System;
namespace IMgzavri.FileStore.Queries
{
    public class Query : IQuery<Result>
    {
        public Guid Id { get; }
        public DateTimeOffset CreatedOn { get; }
        public string CreatedBy { get; }
    }
}