using MediatR;

namespace Protocol.CrossCutting.CQRS
{
    public interface ICommand : ICommand<Unit> { }
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}
