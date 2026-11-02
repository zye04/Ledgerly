using Microsoft.Extensions.Configuration;
using var http = new HttpClient();

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var eodhdKey = config["Eodhd:ApiKey"];
Console.WriteLine(eodhdKey is null ? "EODHD key is missing" : $"EODHD key loaded({eodhdKey.Length} chars)");


var symbols = new[] { "NVDA.US", "TSLA.US", "ASML.AS", "SXR8.XETRA", "VEUA.LSE", "EURUSD.FOREX" };
foreach (var symbol in symbols)
{
    var url = $"https://eodhd.com/api/eod/{symbol}?api_token={eodhdKey}&fmt=json&from=2026-10-26";
    Console.WriteLine(symbol);
    var json = await http.GetStringAsync(url);
    Console.WriteLine(json);
}
