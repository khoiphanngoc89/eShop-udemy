namespace Ordering.Domain.Entities.Abstractions;

public interface IAuditable
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastedModifiedAt { get; set; }
    public string? LastedModifiedBy { get; set; }
}
