using Ledgerly.Core;

namespace Ledgerly.Core.Tests;

public class MoneyTests
{
    [Fact]
    public void Amounts_with_same_value_and_currency_are_equal()
    {
        var a = new Money(10.50m, "EUR");
        var b = new Money(10.50m, "EUR");

        Assert.Equal(a, b);
    }

    [Fact]
    public void Adding_same_currency_sums_the_amounts()
    {
        var result = new Money(10m, "EUR").Add(new Money(5.25m, "EUR"));

        Assert.Equal(new Money(15.25m, "EUR"), result);
    }

    [Fact]
    public void Adding_different_currencies_throws()
    {
        var eur = new Money(10m, "EUR");
        var usd = new Money(5m, "USD");

        Assert.Throws<InvalidOperationException>(() => eur.Add(usd));
    }
}
