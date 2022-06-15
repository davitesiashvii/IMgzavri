using IMgzavri.Infrastructure.Db;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using SimpleSoft.Mediator;


namespace IMgzavri.Commands
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand, Result> where TCommand : Command
    {
        protected readonly IMgzavriDbContext context;
        //protected ClientServiceProvider ClientServiceProvider;
        protected IAuthorizedUserService Auth;

        protected CommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth)
        {
            this.context = context;
            //ClientServiceProvider = clientServiceProvider;
            Auth = auth;
        }

        public abstract Task<Result> HandleAsync(TCommand cmd, CancellationToken ct);
    }
}
