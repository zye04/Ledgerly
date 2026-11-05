namespace Ledgerly.Core;

/// <summary>
/// One real account at a broker, e.g. "my long-term Trading 212 account". A user can have several,
/// including several at the same broker; each is a separate account with its own positions.
/// </summary>
public class Account
{
    /// <summary>
    /// Ledgerly's own identifier, generated on creation. Distinguishes accounts even when all their values match.
    /// </summary>
    public Guid Id { get; }

    public BrokerType Broker { get; }

    /// <summary>
    /// The broker's own identifier for this account. Together with <see cref="Broker"/> it identifies the real
    /// account, which is how the same account is kept from being added twice (FR02).
    /// </summary>
    public string ExternalAccountId { get; }

    /// <summary>
    /// A name the user chooses, e.g. "Long-term".
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Creates an account, rejecting values that cannot exist in reality. Whether the same real account
    /// already exists is not checked here, since a single account cannot know about the others.
    /// </summary>
    /// <param name="broker">Which broker holds the account. Must be a defined <see cref="BrokerType"/>.</param>
    /// <param name="externalAccountId">The broker's own identifier for the account. Must not be empty.</param>
    /// <param name="name">The user's name for the account. Must not be empty.</param>
    /// <exception cref="ArgumentException">The name or external account id is null, empty or whitespace.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="broker"/> is not a defined broker.</exception>
    public Account(BrokerType broker, string externalAccountId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Account must have a name.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(externalAccountId))
        {
            throw new ArgumentException("Accounts must have a valid external account id.", nameof(externalAccountId));
        }

        if (!Enum.IsDefined(broker))
        {
            throw new ArgumentOutOfRangeException(nameof(broker), "Account must have a valid broker.");
        }

        Id = Guid.NewGuid();
        Broker = broker;
        ExternalAccountId = externalAccountId;
        Name = name;
    }
}
