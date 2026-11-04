namespace Ledgerly.Core;

/// <summary>
/// A market where stocks and ETFs are listed and traded, e.g. "XETRA" or "NASDAQ".
/// Two exchanges with the same name are the same exchange: equality compares values, not references.
/// </summary>
public record Exchange
{
    public string Name { get; }

    /// <summary>
    /// Creates an exchange with the given name.
    /// </summary>
    /// <param name="name">The exchange's name. Must not be empty.</param>
    /// <exception cref="ArgumentException"><paramref name="name"/> is null, empty or whitespace.</exception>
    public Exchange(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Exchanges must have a name.", nameof(name));
        }

        Name = name;
    }
}
