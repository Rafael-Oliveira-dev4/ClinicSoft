namespace ClinicSoft.Domain.Model.ValueObjects;

public class CartaoCidadao
{
    public string Number { get; private set; }

    public CartaoCidadao(string number)
    {
        var clean = number?.Replace(" ", "").ToUpper() ?? "";
        if (string.IsNullOrWhiteSpace(clean) || clean.Length < 8)
            throw new ArgumentException("Número de Cartão de Cidadão inválido.", nameof(number));
        Number = clean;
    }

    public override string ToString() => Number;

    private CartaoCidadao() { Number = string.Empty; }
}
