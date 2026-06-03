namespace ClinicSoft.Domain.Model.Enums;

public enum ExamStatus
{
    Requested,        // Solicitado
    Collected,        // Colhido/Realizado
    Processing,       // Em processamento
    ResultAvailable,  // Resultado disponível
    Cancelled
}
