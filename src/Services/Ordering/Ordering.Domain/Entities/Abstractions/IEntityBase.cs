namespace Ordering.Domain.Entities.Abstractions;

public interface IEntityBase<T> : IAuditable
{
    public T Id { get; set; }
}
