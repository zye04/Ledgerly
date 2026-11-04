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

        Assert.Throws<ArgumentException>(() => eur.Add(usd));
    }

    [Fact]
    public void Multiply_by_negative_throws()
    {
        var factor = -1m;
        var money = new Money(10m, "EUR");

        Assert.Throws<ArgumentOutOfRangeException>(() => money.Multiply(factor));
    }

    [Fact]
    public void Multiply_by_fractional_scales_the_amount()
    {
        var factor = 0.5m;
        var result = new Money(10m, "EUR").Multiply(factor);

        Assert.Equal(new Money(5m, "EUR"), result);
    }

    [Fact]
    public void Multiply_by_non_fractional_scales_the_amount()
    {
        var factor = 4m;
        var result = new Money(10m, "EUR").Multiply(factor);

        Assert.Equal(new Money(40m, "EUR"), result);
    }

    [Fact]
    public void Multiply_by_zero_dissolves_the_amount()
    {
        var factor = 0m;
        var result = new Money(10m, "EUR").Multiply(factor);

        Assert.Equal(new Money(0m, "EUR"), result);

    }
}
