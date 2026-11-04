namespace Ledgerly.Core;

public class Instrument
{
    public Guid Id { get; }
    public string? Isin { get; }
    public string Name { get; }
    public InstrumentType Type { get; }
    public string Currency { get; }

    public Instrument(string name, string? isin, InstrumentType type, string currency)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3 || !currency.All(char.IsLetter))
        {
            throw new ArgumentException("Currency must be a 3-letter code.", nameof(currency));
        }

        if (isin != null && type == InstrumentType.Crypto)
        {
            throw new ArgumentException("Crypto currencies do not own Isin codes.", nameof(isin));
        }

        if (string.IsNullOrWhiteSpace(isin) && type != InstrumentType.Crypto)
        {
            throw new ArgumentException("ETFs and Stocks require an Isin code.", nameof(isin));
        }

        Id = Guid.NewGuid();
        Isin = isin;
        Name = name;
        Type = type;
        Currency = currency;
    }
}
