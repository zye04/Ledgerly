using System.Transactions;
using Ledgerly.Core;

namespace Ledgerly.Core.Tests;

public class InstrumentTests
{

    [Theory]
    [InlineData("SXR8", "abfas123nsdfa", InstrumentType.Etf, "EUR")]
    [InlineData("BTC", null, InstrumentType.Crypto, "USD")]
    [InlineData("NVDA", "hsyfgew123e", InstrumentType.Stock, "EUR")]
    public void Valid_instrument_is_created_with_its_value(string name, string? isin, InstrumentType type, string currency)
    {
        var instrument = new Instrument(name, isin, type, currency);

        Assert.Equal(name, instrument.Name);
        Assert.Equal(isin, instrument.Isin);
        Assert.Equal(type, instrument.Type);
        Assert.Equal(currency, instrument.Currency);
        Assert.NotEqual(Guid.Empty, instrument.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Instrument_with_empty_name_throws(string name)
    {
        var nameThrow = Assert.Throws<ArgumentException>(() => new Instrument(name, "test", InstrumentType.Stock, "EUR"));
        Assert.Equal("name", nameThrow.ParamName);
    }

    [Theory]
    [InlineData("EU")]
    [InlineData("1€3")]
    [InlineData("€€€")]
    public void Instrument_with_non_valid_currency_throws(string currency)
    {
        var currencyThrow = Assert.Throws<ArgumentException>(() => new Instrument("NVDA", "test", InstrumentType.Stock, currency));
        Assert.Equal("currency", currencyThrow.ParamName);
    }

}
