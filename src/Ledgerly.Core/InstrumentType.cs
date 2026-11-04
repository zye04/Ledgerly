namespace Ledgerly.Core;

/// <summary>
/// The kind of an <see cref="Instrument"/>. Stocks and ETFs behave the same in Ledgerly;
/// crypto differs because it has no ISIN and no exchange.
/// </summary>
public enum InstrumentType { Stock, Etf, Crypto }
