namespace ClinicSoft.Domain.Model.ValueObjects;

public class NumeroUtenteSns
{
    public string Number { get; private set; }

    public NumeroUtenteSns(string number)
    {
        var clean = number?.Replace(" ", "") ?? "";
        if (clean.Length != 9 || !clean.All(char.IsDigit))
            throw new ArgumentException("Número de Utente SNS inválido. Deve ter 9 dígitos.", nameof(number));
        Number = clean;
    }

    public override string ToString() => Number;

    private NumeroUtenteSns() { Number = string.Empty; }
}
