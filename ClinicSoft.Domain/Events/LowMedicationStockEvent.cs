namespace ClinicSoft.Domain.Events;

public class LowMedicationStockEvent : DomainEvent
{
    public Guid MedicationId { get; }
    public string MedicationName { get; }
    public int CurrentStock { get; }
    public int MinimumStock { get; }

    public LowMedicationStockEvent(Guid medicationId, string name, int current, int minimum)
    {
        MedicationId = medicationId;
        MedicationName = name;
        CurrentStock = current;
        MinimumStock = minimum;
    }
}
