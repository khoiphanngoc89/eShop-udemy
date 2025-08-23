using MediatR;

namespace BuildingBlocks.Common.Cqrs;

public interface IQuery<out TResponse> : IRequest<TResponse>  
    where TResponse : notnull
{
}