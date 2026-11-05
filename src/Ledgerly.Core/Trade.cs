namespace Ledgerly.Core;

/// <summary>
/// One executed buy or sell that changed a position's quantity. A trade that has happened never changes,
/// so it is an immutable record. Ledgerly uses trades to rebuild how much was held at any past moment.
/// </summary>
public record Trade
{
    /// <summary>
    /// When the trade was executed, always stored in UTC. The moment is kept exactly; the original time zone is not.
    /// </summary>
    public DateTimeOffset Timestamp { get; }

    public TradeType Type { get; }

    /// <summary>
    /// How much was bought or sold. Always positive; <see cref="Type"/> says the direction.
    /// </summary>
    public decimal Quantity { get; }

    /// <summary>
    /// The price paid or received per unit. Can be 0, never negative.
    /// </summary>
    public Money Price { get; }

    /// <summary>
    /// Creates a trade, rejecting values that cannot exist in reality.
    /// </summary>
    /// <param name="timestamp">When the trade was executed, in any time zone; it is converted to UTC.</param>
    /// <param name="type">Whether it was a buy or a sell.</param>
    /// <param name="quantity">How much was traded. Must be greater than 0.</param>
    /// <param name="price">The price per unit. Must not be null or negative.</param>
    /// <exception cref="ArgumentException">
    /// The quantity is 0 or negative, or the price is null or negative.
    /// </exception>
    public Trade(DateTimeOffset timestamp, TradeType type, decimal quantity, Money price)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("A trade quantity cannot be zero nor negative.", nameof(quantity));
        }

        if (price == null)
        {
            throw new ArgumentException("A trade must have a price value.", nameof(price));
        }

        if (price.Amount < 0)
        {
            throw new ArgumentException("Price value can not be negative.", nameof(price));
        }

        Timestamp = timestamp.ToUniversalTime();
        Type = type;
        Quantity = quantity;
        Price = price;
    }
}
