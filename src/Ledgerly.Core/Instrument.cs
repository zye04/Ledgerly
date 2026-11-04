namespace Ledgerly.Core;

/// <summary>
/// Something that can be bought and sold, such as NVIDIA, SXR8 or Bitcoin.
/// It is the same for every investor and exists whether or not anyone holds it.
/// </summary>
public class Instrument
{
    /// <summary>
    /// Ledgerly's own identifier, generated on creation. The ISIN cannot serve as identity because crypto has none.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// The international securities code, e.g. "US67066G1040" for NVIDIA. Always present for stocks and ETFs, always null for crypto.
    /// </summary>
    public string? Isin { get; }

    public string Name { get; }

    public InstrumentType Type { get; }

    /// <summary>
    /// The currency the instrument is priced in (ISO 4217 code, e.g. "USD"), which may differ from the account's currency.
    /// </summary>
    public string Currency { get; }

    /// <summary>
    /// The exchange where the instrument is listed and traded. Always present for stocks and ETFs,
    /// always null for crypto, which is not listed on a single venue.
    /// </summary>
    public Exchange? Exchange { get; }

    /// <summary>
    /// Creates an instrument, rejecting any combination of values that cannot exist in reality.
    /// </summary>
    /// <param name="name">The display name. Must not be empty.</param>
    /// <param name="isin">Required for stocks and ETFs; must be null for crypto.</param>
    /// <param name="type">Whether this is a stock, an ETF or crypto.</param>
    /// <param name="currency">A 3-letter currency code, e.g. "EUR".</param>
    /// <param name="exchange">Required for stocks and ETFs; must be null for crypto.</param>
    /// <exception cref="ArgumentException">
    /// The name is empty, the currency is not a 3-letter code, a stock or ETF has no ISIN or no exchange,
    /// or crypto has either.
    /// </exception>
    public Instrument(string name, string? isin, InstrumentType type, string currency, Exchange? exchange)
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

        if (type != InstrumentType.Crypto && exchange == null)
        {
            throw new ArgumentException("ETFs and Stocks must have an assigned Exchange.", nameof(exchange));
        }

        if (type == InstrumentType.Crypto && exchange != null)
        {
            throw new ArgumentException("Crypto does not have an Exchange.", nameof(exchange));
        }

        Id = Guid.NewGuid();
        Isin = isin;
        Name = name;
        Type = type;
        Currency = currency;
        Exchange = exchange;
    }
}
