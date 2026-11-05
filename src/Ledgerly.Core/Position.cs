namespace Ledgerly.Core;

/// <summary>
/// The user's holding of one <see cref="Ledgerly.Core.Instrument"/>: how much of it, and at what average price.
/// It stays the same position as its quantity changes, and is kept at quantity 0 once closed so its history survives.
/// </summary>
public class Position
{
    public Instrument Instrument { get; }

    /// <summary>
    /// How much is held. Can be fractional (0.37 shares, 0.0021 BTC); 0 means the position is closed.
    /// </summary>
    public decimal Quantity { get; }

    /// <summary>
    /// The average price paid per unit, in the instrument's currency. Can be 0 for crypto received
    /// from staking rewards or airdrops.
    /// </summary>
    public Money AveragePrice { get; }

    /// <summary>
    /// Creates a position, rejecting values that cannot exist in reality.
    /// </summary>
    /// <param name="instrument">What is held. Must not be null.</param>
    /// <param name="quantity">How much is held. Must not be negative.</param>
    /// <param name="averagePrice">The average price per unit. Must not be null or negative, and must be in the instrument's currency.</param>
    /// <exception cref="ArgumentException">
    /// The instrument or average price is null, the average price is negative, or its currency differs from the instrument's.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="quantity"/> is negative.</exception>
    public Position(Instrument instrument, decimal quantity, Money averagePrice)
    {
        if (instrument == null)
        {
            throw new ArgumentException("A position must have a declared instrument.", nameof(instrument));
        }

        if (averagePrice == null)
        {
            throw new ArgumentException("AveragePrice can not be null.", nameof(averagePrice));
        }

        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity can not be negative.");
        }

        if (averagePrice.Amount < 0)
        {
            throw new ArgumentException("Average price can not be negative.", nameof(averagePrice));
        }

        if (instrument.Currency != averagePrice.Currency)
        {
            throw new ArgumentException("Currencies must be the same.", nameof(averagePrice));
        }

        Instrument = instrument;
        Quantity = quantity;
        AveragePrice = averagePrice;
    }
}
