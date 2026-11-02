## Use cases

- UC01 -> Consult portfolio value and its composition
- UC02 -> Expand individual assets of the portfolio
- UC03 -> Add a new source supported by the system
- UC04 -> See the last time the portfolio and asset values were  updated
- UC05 -> Consult historical data of the portfolio
- UC06 -> Consult historical data of the individual assets within the portfolio

## Functional requirements

- FR01 -> On opening the app must validate the recency of the data and update it to the last minute
- FR02 -> New external source addition must be unique
- FR03 -> Currency should be displayed in EUR
- FR04 -> Stores a snapshot of every asset and portfolio value every minute
- FR05 -> On launch, fill the gap since the last snapshot, converted to EUR:
  - Stocks: daily resolution, from the market data source
  - Crypto: hourly up to 30 days, daily beyond, from Kraken
- FR06 -> Stored snapshot values must be aggregatable upwards in five minute, one hour, one week, one month and yearly

## Non-functional requirements

- NFR01 -> Updated by the minute while the app is running
- NFR02 -> API keys are only ever exposed to the broker it belongs
- NFR03 -> System server only accepts connections from localhost, so no data is reachable from outside
- NFR04 -> API keys must be read-only for system use

## Constraint

- C01 -> Single user web interface
- C02 -> Local first solution
