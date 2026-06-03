namespace ClinicSoft.Domain.Model.ValueObjects;

public class CedulaProfissional
{
    public string Number { get; private set; }

    public CedulaProfissional(string number)
    {
        var clean = number?.Replace(" ", "") ?? "";
        if (string.IsNullOrWhiteSpace(clean))
            throw new ArgumentException("Cédula Profissional inválida.", nameof(number));
        Number = clean;
    }

    public override string ToString() => Number;

    private CedulaProfissional() { Number = string.Empty; }
}
