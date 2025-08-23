using MediatR;

namespace BuildingBlocks.Common.Cqrs;

public interface ICommand : ICommand<Unit>
{
}
public interface ICommand<out TRespond> : IRequest<TRespond>
    where TRespond : notnull
{
}