namespace ClinicSoft.Domain.Interfaces;

public interface IAuditable
{
    DateTime? CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    string? CreatedBy { get; set; }
    string? UpdatedBy { get; set; }
    bool IsDeleted { get; set; }

    void MarkAsDeleted(string? deletedBy = null);
    void UpdateTimestamp(string? updatedBy = null);
}
