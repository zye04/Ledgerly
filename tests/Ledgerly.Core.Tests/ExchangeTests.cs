namespace Ledgerly.Core.Tests;

public class ExchangeTests
{
    [Theory]
    [InlineData("  ")]
    [InlineData("")]
    [InlineData(null)]
    public void Exchange_with_non_valid_name_throws(string? name)
    {
        var nameThrows = Assert.Throws<ArgumentException>(() => new Exchange(name!));
        Assert.Equal("name", nameThrows.ParamName);
    }

    [Theory]
    [InlineData("Frankfurt Stock Exchange")]
    [InlineData("London Stock Exchange")]
    public void Valid_exchange_is_created_with_its_value(string name)
    {
        var exchange = new Exchange(name);
        Assert.Equal(name, exchange.Name);
    }

    [Fact]
    public void Exchanges_same_name_is_the_same()
    {
        var a = new Exchange("London Stock Exchange");
        var b = new Exchange("London Stock Exchange");
        Assert.Equal(a, b);
    }

    [Fact]
    public void Exchanges_with_different_names_are_different()
    {
        var a = new Exchange("London Stock Exchange");
        var b = new Exchange("Frankfurt Stock Exchange");
        Assert.NotEqual(a, b);
    }
}
