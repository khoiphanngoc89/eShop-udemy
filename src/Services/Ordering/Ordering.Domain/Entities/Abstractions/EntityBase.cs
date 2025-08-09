namespace Ordering.Domain.Entities.Abstractions;

public abstract class EntityBase<T> : IEntityBase<T>
{
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastedModifiedAt { get; set; }
    public string? LastedModifiedBy { get; set; }
}
