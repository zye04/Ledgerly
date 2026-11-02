# ADR-0002: EODHD as the market data provider

- Status: Accepted
- Date: 2026-11-02

## Context

FR05 requires filling stock gaps (time when Ledgerly was not running) with daily prices, converted to EUR. Trading 212 provides live values but no historical prices, so a separate market data source is needed. Holdings currently trade on NASDAQ, Euronext Amsterdam, Xetra and the London Stock Exchange, so the source must cover US and European exchanges, plus EUR/USD history for conversion.

Yahoo Finance was ruled out: it has no official API, and the unofficial endpoints break its terms of service and can stop working without notice. Among the official providers, no free tier offers intraday history for European exchanges, which is why FR05 uses daily resolution for stocks. The free tiers considered:

- Twelve Data: US only (European exchanges from $229/month).
- Alpha Vantage: 25 calls/day, latest 100 days of daily data only.
- EODHD: 20 calls/day, 1 year of daily data, worldwide coverage including forex.

A spike (branch `spike/market-data`) called EODHD for all current holdings (`NVDA.US`, `TSLA.US`, `ASML.AS`, `SXR8.XETRA`, `VEUA.LSE`) and `EURUSD.FOREX`. All six returned daily data.

## Decision

Use EODHD's end-of-day API, behind its own adapter (see ADR-0001), for daily stock prices and EUR/USD rates.

EODHD prices carry no currency, so each instrument's currency (`currencyCode`) and exchange trading hours (`workingScheduleId`) come from Trading 212's instrument and exchange metadata.

Instruments are matched between the two sources by ISIN. When Ledgerly sees an instrument for the first time, it calls EODHD's search API with the ISIN (1 call) and stores the resulting EODHD symbol, so the lookup never repeats for that instrument. One ISIN can be listed on several exchanges; the listing matching the exchange Trading 212 reports is the one used.

The current holdings were the test sample. Any instrument on an exchange EODHD covers is supported without code changes, although only the four exchanges above have been verified.

## Consequences

- Coverage of all current exchanges and EUR/USD is verified, not assumed.
- The free tier covers normal use: with incremental fetching, a typical day needs one call per instrument plus one for EUR/USD, and each new instrument costs one extra lookup call, once.
- Gaps longer than one year cannot be filled on the free tier; older history stays missing.
- The 20 calls/day limit means a portfolio of more than about 19 instruments, or repeated full refetches, would exceed the free tier. Fetching only missing, closed trading days is required, not optional.
- EODHD returns forex rows for weekends, when the forex market is closed. The adapter must not treat these as real trading data.
- "Has the market closed?" is decided per exchange, using Trading 212's schedules, since European and US exchanges close at different times.
- Ledgerly now depends on two sources to value a stock: Trading 212 for metadata and EODHD for prices. If either changes, only its adapter changes.
