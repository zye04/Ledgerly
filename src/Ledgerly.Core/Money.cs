namespace Ledgerly.Core;

public record Money(decimal Amount, string Currency)
{
    public Money Add(Money other)
    {
        if (other.Currency != Currency)
        {
            throw new InvalidOperationException("Currencies differ and so they cannot be added.");
        }

        return new Money(Amount + other.Amount, Currency);
    }
}
