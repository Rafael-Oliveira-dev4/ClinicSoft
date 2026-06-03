namespace ClinicSoft.Domain.Model.ValueObjects;

public class Name
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Name(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2)
            throw new ArgumentException("Primeiro nome inválido.", nameof(firstName));

        FirstName = firstName.Trim();
        LastName = string.IsNullOrWhiteSpace(lastName) ? string.Empty : lastName.Trim();
    }

    public string FullName => string.IsNullOrWhiteSpace(LastName)
        ? FirstName
        : $"{FirstName} {LastName}";

    public override string ToString() => FullName;

    public bool Equals(Name? other)
    {
        if (other is null) return false;
        return FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase) &&
               LastName.Equals(other.LastName, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj) => Equals(obj as Name);

    public override int GetHashCode() => HashCode.Combine(FirstName.ToLower(), LastName.ToLower());

    private Name() { FirstName = LastName = string.Empty; }
}
