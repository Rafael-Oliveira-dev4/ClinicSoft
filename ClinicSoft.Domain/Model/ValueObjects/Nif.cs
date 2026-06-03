namespace ClinicSoft.Domain.Model.ValueObjects;

public class Nif
{
    public string Number { get; private set; }

    public Nif(string number)
    {
        var clean = number?.Replace(" ", "").Replace("-", "") ?? "";
        if (clean.Length != 9 || !clean.All(char.IsDigit))
            throw new ArgumentException("NIF inválido. Deve ter 9 dígitos.", nameof(number));
        if (!IsValidNif(clean))
            throw new ArgumentException("NIF inválido (dígito de controlo).", nameof(number));
        Number = clean;
    }

    private static bool IsValidNif(string nif)
    {
        var validFirstDigits = new[] { '1', '2', '3', '5', '6', '8', '9' };
        if (!validFirstDigits.Contains(nif[0])) return false;

        int sum = 0;
        for (int i = 0; i < 8; i++)
            sum += (nif[i] - '0') * (9 - i);

        int remainder = sum % 11;
        int checkDigit = remainder < 2 ? 0 : 11 - remainder;
        return checkDigit == (nif[8] - '0');
    }

    public override string ToString() => Number;

    private Nif() { Number = string.Empty; }
}
