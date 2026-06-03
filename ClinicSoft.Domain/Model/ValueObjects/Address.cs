namespace ClinicSoft.Domain.Model.ValueObjects;

public class Address
{
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Parish { get; private set; }       // Freguesia
    public string Municipality { get; private set; } // Concelho
    public string District { get; private set; }     // Distrito
    public string PostalCode { get; private set; }   // Código Postal (XXXX-XXX)
    public string? Complement { get; private set; }  // Andar, porta, etc.

    public Address(string street, string number, string parish, string municipality,
                   string district, string postalCode, string? complement = null)
    {
        Street = Require(street, nameof(street));
        Number = Require(number, nameof(number));
        Parish = Require(parish, nameof(parish));
        Municipality = Require(municipality, nameof(municipality));
        District = Require(district, nameof(district));
        PostalCode = ValidatePostalCode(postalCode);
        Complement = complement?.Trim();
    }

    private static string Require(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{name} não pode ser vazio.", name);
        return value.Trim();
    }

    private static string ValidatePostalCode(string postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Código postal não pode ser vazio.", nameof(postalCode));

        var clean = postalCode.Replace("-", "").Replace(" ", "");
        if (clean.Length != 7 || !clean.All(char.IsDigit))
            throw new ArgumentException("Código postal inválido. Formato: XXXX-XXX", nameof(postalCode));

        return $"{clean[..4]}-{clean[4..]}";
    }

    public string FullAddress =>
        $"{Street}, {Number}" +
        (string.IsNullOrWhiteSpace(Complement) ? "" : $", {Complement}") +
        $"\n{PostalCode} {Parish}" +
        $"\n{Municipality}, {District}";

    public override string ToString() => FullAddress;

    public bool Equals(Address? other)
    {
        if (other is null) return false;
        return Street.Equals(other.Street, StringComparison.OrdinalIgnoreCase) &&
               Number.Equals(other.Number, StringComparison.OrdinalIgnoreCase) &&
               PostalCode.Equals(other.PostalCode, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj) => Equals(obj as Address);

    public override int GetHashCode() =>
        HashCode.Combine(Street.ToLower(), Number.ToLower(), PostalCode.ToLower());

    private Address()
    {
        Street = Number = Parish = Municipality = District = PostalCode = string.Empty;
    }
}
