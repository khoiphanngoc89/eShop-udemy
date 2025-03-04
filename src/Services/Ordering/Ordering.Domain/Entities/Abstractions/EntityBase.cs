namespace Ordering.Domain.Entities.Abstractions;

public abstract class EntityBase<T> : IEntityBase<T>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public T Id { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastedModifiedAt { get; set; }
    public string? LastedModifiedBy { get; set; }
}
