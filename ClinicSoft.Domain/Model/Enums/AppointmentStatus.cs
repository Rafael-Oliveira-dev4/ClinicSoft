namespace ClinicSoft.Domain.Model.Enums;

public enum AppointmentStatus
{
    Scheduled,   // Agendada
    Confirmed,   // Confirmada
    InProgress,  // Em curso
    Completed,   // Concluída
    Cancelled,   // Cancelada
    NoShow       // Não compareceu
}
