namespace ClinicSoft.Application.Common.Responses;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NotFound = new("NotFound", "Recurso não encontrado");
    public static readonly Error InvalidInput = new("InvalidInput", "Dados de entrada inválidos");
    public static readonly Error ValidationFailed = new("ValidationFailed", "Falha na validação dos dados");
    public static readonly Error OperationFailed = new("OperationFailed", "A operação não foi concluída");
    public static readonly Error Unauthorized = new("Unauthorized", "Acesso não autorizado");
    public static readonly Error InvalidCredentials = new("InvalidCredentials", "Credenciais inválidas");
    public static readonly Error ServerError = new("ServerError", "Erro interno do servidor");

    // Auth
    public static readonly Error UsernameAlreadyExists = new("UsernameAlreadyExists", "Este nome de utilizador já existe");
    public static readonly Error EmailAlreadyExists = new("EmailAlreadyExists", "Este email já está registado");
    public static readonly Error UserNotFound = new("UserNotFound", "Utilizador não encontrado");
    public static readonly Error InvalidToken = new("InvalidToken", "Token inválido ou expirado");
    public static readonly Error AccountInactive = new("AccountInactive", "Conta inactiva");

    // Patient
    public static readonly Error PatientNotFound = new("PatientNotFound", "Utente não encontrado");
    public static readonly Error PatientEmailExists = new("PatientEmailExists", "Já existe um utente com este email");
    public static readonly Error PatientNifExists = new("PatientNifExists", "Já existe um utente com este NIF");

    // Doctor
    public static readonly Error DoctorNotFound = new("DoctorNotFound", "Médico não encontrado");
    public static readonly Error DoctorEmailExists = new("DoctorEmailExists", "Já existe um médico com este email");
    public static readonly Error DoctorCedulaExists = new("DoctorCedulaExists", "Já existe um médico com esta cédula profissional");

    // Appointment
    public static readonly Error AppointmentNotFound = new("AppointmentNotFound", "Consulta não encontrada");
    public static readonly Error AppointmentConflict = new("AppointmentConflict", "O médico já tem uma consulta neste horário");
    public static readonly Error InvalidAppointmentStatus = new("InvalidAppointmentStatus", "Transição de estado inválida");

    // MedicalRecord
    public static readonly Error MedicalRecordNotFound = new("MedicalRecordNotFound", "Registo clínico não encontrado");
    public static readonly Error MedicalRecordAlreadyExists = new("MedicalRecordAlreadyExists", "Já existe um registo clínico para esta consulta");

    // Exam
    public static readonly Error ExamNotFound = new("ExamNotFound", "Exame não encontrado");
    public static readonly Error ExamAlreadyCompleted = new("ExamAlreadyCompleted", "O exame já tem resultado disponível");

    // Medication
    public static readonly Error MedicationNotFound = new("MedicationNotFound", "Medicamento não encontrado");
    public static readonly Error InsufficientStock = new("InsufficientStock", "Stock insuficiente");

    // Bill
    public static readonly Error BillNotFound = new("BillNotFound", "Fatura não encontrada");
    public static readonly Error BillAlreadyPaid = new("BillAlreadyPaid", "A fatura já se encontra paga");
}
