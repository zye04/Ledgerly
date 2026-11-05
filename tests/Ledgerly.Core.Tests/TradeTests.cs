namespace Ledgerly.Core.Tests;

public class TradeTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1234)]
    public void Valid_trade_is_created_with_its_values(decimal price)
    {
        var time = new DateTimeOffset(2026, 11, 1, 14, 30, 0, TimeSpan.Zero);
        var money = new Money(price, "USD");
        var validTrade = new Trade(time, TradeType.Buy, 10, money);
        Assert.Equal(time, validTrade.Timestamp);
        Assert.Equal(TradeType.Buy, validTrade.Type);
        Assert.Equal(10, validTrade.Quantity);
        Assert.Equal(money, validTrade.Price);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1)]
    [InlineData(5)]
    public void Different_timezones_are_valid_and_equally_universal(double offset)
    {
        var time = new DateTimeOffset(2026, 11, 1, 14, 30, 0, TimeSpan.FromHours(offset));
        var money = new Money(10, "USD");
        var trade = new Trade(time, TradeType.Buy, 10, money);
        Assert.Equal(time, trade.Timestamp);
        Assert.Equal(TimeSpan.Zero, trade.Timestamp.Offset);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-99999)]
    public void Zero_or_negative_quantity_throws(decimal quantity)
    {
        var money = new Money(10, "USD");
        var time = new DateTimeOffset(2026, 11, 1, 14, 30, 0, TimeSpan.Zero);
        var quantityThrows = Assert.Throws<ArgumentException>(() => new Trade(time, TradeType.Buy, quantity, money));
        Assert.Equal("quantity", quantityThrows.ParamName);
    }

    [Fact]
    public void Null_price_throws()
    {
        var time = new DateTimeOffset(2026, 11, 1, 14, 30, 0, TimeSpan.Zero);
        var priceThrows = Assert.Throws<ArgumentException>(() => new Trade(time, TradeType.Buy, 10, null!));
        Assert.Equal("price", priceThrows.ParamName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-999999)]
    public void Negative_price_throws(decimal price)
    {
        var time = new DateTimeOffset(2026, 11, 1, 14, 30, 0, TimeSpan.Zero);
        var money = new Money(price, "EUR");
        var priceThrows = Assert.Throws<ArgumentException>(() => new Trade(time, TradeType.Buy, 10, money));
        Assert.Equal("price", priceThrows.ParamName);
    }
}
