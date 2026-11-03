namespace Ledgerly.Core;

/// <summary>
/// An amount of money in a specific currency (ISO 4217 code, e.g. "EUR").
/// Immutable: operations return new values.
/// </summary>
public record Money(decimal Amount, string Currency)
{
    /// <summary>
    /// Returns the sum of this amount and <paramref name="other"/>, in the shared currency.
    /// </summary>
    /// <param name="other">The amount to add. Must be in the same currency.</param>
    /// <returns>A new <see cref="Money"/>; neither operand is changed.</returns>
    /// <exception cref="ArgumentException">The currencies differ.</exception>
    public Money Add(Money other)
    {
        if (other.Currency != Currency)
        {
            throw new ArgumentException(message: "Currencies differ.", paramName: nameof(other));
        }

        return new Money(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// Returns a new amount scaled by <paramref name="factor"/>, in the same currency.
    /// </summary>
    /// <param name="factor">The multiplier, for example a quantity of shares. Must not be negative.</param>
    /// <returns>A new <see cref="Money"/>; this instance is unchanged.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="factor"/> is negative.</exception>
    public Money Multiply(decimal factor)
    {
        if (factor < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), factor, "Factor cannot be negative.");
        }

        return new Money(Amount * factor, Currency);
    }
}
