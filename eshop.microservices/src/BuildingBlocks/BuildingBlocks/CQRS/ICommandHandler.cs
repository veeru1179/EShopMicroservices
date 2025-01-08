using MediatR;

namespace BuildingBlocks.CQRS
{
 public interface ICommandHandler<in TCommnad> 
        : ICommandHandler<TCommnad, Unit> 
        where TCommnad:ICommand<Unit>
    { }
    public interface ICommandHandler<in TCommnad,TResponse>
        :IRequestHandler<TCommnad, TResponse>
        where TCommnad:ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
