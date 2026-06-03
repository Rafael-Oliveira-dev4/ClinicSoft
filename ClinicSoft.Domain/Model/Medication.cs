using ClinicSoft.Domain.Common;
using ClinicSoft.Domain.Events;
using ClinicSoft.Domain.Interfaces;
using ClinicSoft.Domain.Model.Enums;
using ClinicSoft.Domain.Model.ValueObjects;

namespace ClinicSoft.Domain.Model;

public class Medication : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ActiveSubstance { get; set; }
    public MedicationCategory Category { get; set; }
    public required string Unit { get; set; }
    public int CurrentStock { get; set; }
    public int MinimumStock { get; set; }
    public Money? UnitPrice { get; set; }
    public bool RequiresPrescription { get; set; } = true;

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public bool IsLowStock => CurrentStock <= MinimumStock;

    public void AdjustStock(int quantity)
    {
        CurrentStock += quantity;
        if (IsLowStock)
            _domainEvents.Add(new LowMedicationStockEvent(Id, Name, CurrentStock, MinimumStock));
        UpdateTimestamp();
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
