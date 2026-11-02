# ADR-0001: Modular monolith with ports and adapters

- Status: Accepted
- Date: 2026-11-02

## Context

Ledgerly is a single-user app that runs locally on the user's machine and is built by a single developer (C01, C02). It pulls data from several external sources: Trading 212, Kraken and a market data provider. These sources authenticate in very different ways, and the Trading 212 API is officially in Beta, so it may change without notice. A breaking change in one source must not spread through the rest of the system.

## Decision

Ledgerly is built as one deployable application (a modular monolith). Inside it, each external source sits behind a port (an interface owned by the core) with its own adapter, following the ports and adapters (hexagonal) architecture. The core logic depends only on the ports, never on a specific source.

Microservices were considered and rejected. Their main benefits are independent deployment, independent scaling and team autonomy, and none of these apply to one developer serving one user on one machine. Their costs would: network calls between services, several processes to run and monitor, and partial failures.

## Consequences

- A broken or changed source API is fixed in its adapter alone, without touching the core.
- New sources are added by writing a new adapter for an existing port.
- The core can be tested with fake adapters, with no real API calls.
- Downside: everything runs in one process, so a crash in any adapter takes the whole app down. Adapters must handle their own failures (timeouts, bad responses) instead of letting them propagate.
- Downside: if Ledgerly ever became multi-user or hosted, this decision would have to be revisited.
