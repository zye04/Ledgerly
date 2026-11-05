namespace Ledgerly.Core.Tests;

public class PositionTests
{
    [Fact]
    public void Valid_Position_is_created_with_its_values()
    {
        var money = new Money(0, "USD");
        var instrument = new Instrument("Test", null, InstrumentType.Crypto, "USD", null);
        var position = new Position(instrument, 0, money);
        Assert.Equal(instrument, position.Instrument);
        Assert.Equal(0, position.Quantity);
        Assert.Equal(money, position.AveragePrice);
    }

    [Fact]
    public void If_position_instrument_null_throws()
    {
        var averagePrice = new Money(123, "USD");
        var instrumentThrows = Assert.Throws<ArgumentException>(() => new Position(null!, 1234, averagePrice));
        Assert.Equal("instrument", instrumentThrows.ParamName);
    }

    [Fact]
    public void If_average_price_is_null_throws()
    {
        Money averagePriceNull = null!;
        var instrument = new Instrument("Test", null, InstrumentType.Crypto, "USD", null);
        var averagePriceThrows = Assert.Throws<ArgumentException>(() => new Position(instrument, 1234, averagePriceNull));
        Assert.Equal("averagePrice", averagePriceThrows.ParamName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-9999)]
    public void Negative_quantity_throws(decimal quantity)
    {
        var instrument = new Instrument("Test", null, InstrumentType.Crypto, "USD", null);
        var averagePrice = new Money(1234, "USD");
        var quantityThrows = Assert.Throws<ArgumentOutOfRangeException>(() => new Position(instrument, quantity, averagePrice));
        Assert.Equal("quantity", quantityThrows.ParamName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-9999)]
    public void Negative_average_price_throws(decimal averagePrice)
    {
        var money = new Money(averagePrice, "USD");
        var instrument = new Instrument("Test", null, InstrumentType.Crypto, "USD", null);
        var averagePriceThrows = Assert.Throws<ArgumentException>(() => new Position(instrument, 1234, money));
        Assert.Equal("averagePrice", averagePriceThrows.ParamName);
    }

    [Fact]
    public void If_position_currencies_are_not_equal_throws()
    {
        var money = new Money(123, "USD");
        var instrument = new Instrument("Test", null, InstrumentType.Crypto, "EUR", null);
        var currencyThrows = Assert.Throws<ArgumentException>(() => new Position(instrument, 1234, money));
        Assert.Equal("averagePrice", currencyThrows.ParamName);
    }
}
