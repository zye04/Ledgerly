namespace Ledgerly.Core;

public record Exchange
{
    public string Name { get; }

    public Exchange(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Exchanges must have a name.", nameof(name));
        }

        Name = name;
    }
}
