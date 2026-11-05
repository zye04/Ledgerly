namespace Ledgerly.Core.Tests;

public class AccountTests
{
    [Theory]
    [InlineData(BrokerType.Kraken, "1234", "Trading212")]
    [InlineData(BrokerType.Trading212, "0001", "Random Account")]
    [InlineData(BrokerType.Trading212, "999999", "Test")]
    public void Valid_account_values_creates_it_with_its_values(BrokerType broker, string externalAccountId, string name)
    {
        var account = new Account(broker, externalAccountId, name);
        Assert.NotEqual(Guid.Empty, account.Id);
        Assert.Equal(broker, account.Broker);
        Assert.Equal(externalAccountId, account.ExternalAccountId);
        Assert.Equal(name, account.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Null_or_blank_name_throws(string? name)
    {
        var accountThrows = Assert.Throws<ArgumentException>(() => new Account(BrokerType.Trading212, "1234", name!));
        Assert.Equal("name", accountThrows.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Null_or_blank_external_id_throws(string? externalAccountId)
    {
        var externalAccountIdThrows = Assert.Throws<ArgumentException>(() => new Account(BrokerType.Trading212, externalAccountId!, "Trading212"));
        Assert.Equal("externalAccountId", externalAccountIdThrows.ParamName);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(9999)]
    public void Not_valid_broker_enum_throws(int broker)
    {
        var brokerEnumThrows = Assert.Throws<ArgumentOutOfRangeException>(() => new Account((BrokerType)broker, "123", "test"));
        Assert.Equal("broker", brokerEnumThrows.ParamName);
    }
}
