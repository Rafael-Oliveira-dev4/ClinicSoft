namespace ClinicSoft.Domain.Events;

public class PrescriptionIssuedEvent : DomainEvent
{
    public Guid PrescriptionId { get; }
    public Guid PatientId { get; }
    public string MedicationName { get; }

    public PrescriptionIssuedEvent(Guid prescriptionId, Guid patientId, string medicationName)
    {
        PrescriptionId = prescriptionId;
        PatientId = patientId;
        MedicationName = medicationName;
    }
}
