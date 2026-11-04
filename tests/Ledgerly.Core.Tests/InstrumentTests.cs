namespace Ledgerly.Core.Tests;

public class InstrumentTests
{
    [Theory]
    [InlineData("SXR8", "abfas123nsdfa", InstrumentType.Etf, "EUR", "London Stock Exchange")]
    [InlineData("BTC", null, InstrumentType.Crypto, "USD", null)]
    [InlineData("NVDA", "hsyfgew123e", InstrumentType.Stock, "EUR", "London Stock Exchange")]
    public void Valid_instrument_is_created_with_its_value(string name, string? isin, InstrumentType type, string currency, string? exchangeName)
    {
        Exchange? exchange = exchangeName is null ? null : new Exchange(exchangeName);
        var instrument = new Instrument(name, isin, type, currency, exchange);

        Assert.Equal(name, instrument.Name);
        Assert.Equal(isin, instrument.Isin);
        Assert.Equal(type, instrument.Type);
        Assert.Equal(currency, instrument.Currency);
        Assert.Equal(exchange, instrument.Exchange);
        Assert.NotEqual(Guid.Empty, instrument.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Instrument_with_empty_name_throws(string name)
    {
        var exchange = new Exchange("London Stock Exchange");
        var nameThrow = Assert.Throws<ArgumentException>(() => new Instrument(name, "test", InstrumentType.Stock, "EUR", exchange));
        Assert.Equal("name", nameThrow.ParamName);
    }

    [Theory]
    [InlineData("EU")]
    [InlineData("1€3")]
    [InlineData("€€€")]
    public void Instrument_with_non_valid_currency_throws(string currency)
    {
        var exchange = new Exchange("London Stock Exchange");
        var currencyThrow = Assert.Throws<ArgumentException>(() => new Instrument("NVDA", "test", InstrumentType.Stock, currency, exchange));
        Assert.Equal("currency", currencyThrow.ParamName);
    }

    [Theory]
    [InlineData(InstrumentType.Stock, null)]
    [InlineData(InstrumentType.Etf, null)]
    [InlineData(InstrumentType.Stock, "")]
    [InlineData(InstrumentType.Etf, " ")]
    public void Stock_or_etf_without_isin_throws(InstrumentType type, string? isin)
    {
        var exchange = new Exchange("London Stock Exchange");
        var isinThrow = Assert.Throws<ArgumentException>(() => new Instrument("NVDA", isin, type, "USD", exchange));
        Assert.Equal("isin", isinThrow.ParamName);
    }

    [Fact]
    public void Crypto_with_isin_throws()
    {
        var isinThrow = Assert.Throws<ArgumentException>(() => new Instrument("BTC", "US67066G1040", InstrumentType.Crypto, "USD", null));
        Assert.Equal("isin", isinThrow.ParamName);
    }

    [Theory]
    [InlineData(InstrumentType.Stock)]
    [InlineData(InstrumentType.Etf)]
    public void Stock_or_etf_without_exchange_throws(InstrumentType type)
    {
        var exchangeThrow = Assert.Throws<ArgumentException>(() => new Instrument("NVDA", "123qwe", type, "USD", null));
        Assert.Equal("exchange", exchangeThrow.ParamName);
    }

    [Fact]
    public void Crypto_with_exchange_throws()
    {
        var exchange = new Exchange("London Stock Exchange");
        var exchangeThrow = Assert.Throws<ArgumentException>(() => new Instrument("BTC", null, InstrumentType.Crypto, "USD", exchange));
        Assert.Equal("exchange", exchangeThrow.ParamName);
    }
}
