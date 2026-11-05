namespace Ledgerly.Core;

/// <summary>
/// The brokers Ledgerly can connect to. Each one needs its own adapter, so adding a broker is a code change, not a setting.
/// </summary>
public enum BrokerType { Trading212, Kraken }
