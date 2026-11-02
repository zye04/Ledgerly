# ADR-0003: Solution structure and Blazor WebAssembly with a REST API

- Status: Accepted
- Date: 2026-11-02

## Context

ADR-0001 chose a modular monolith with ports and adapters. That decision needs a concrete project layout in which the core cannot depend on external systems by accident. The UI also needs a way to get its data: either call the core directly from the server, or fetch it from an API over HTTP.

## Decision

The solution has five projects:

- `src/Ledgerly.Core`: domain model, ports (interfaces) and application logic. It references no other project and no infrastructure packages.
- `src/Ledgerly.Infrastructure`: adapters (Trading 212, Kraken, EODHD) and persistence, one folder each. References Core.
- `src/Ledgerly.Web`: the host. Serves the REST API and the UI, runs the background snapshot job, and wires adapters to ports at startup (composition root). References Core and Infrastructure.
- `src/Ledgerly.Web.Client`: the Blazor WebAssembly UI, running in the browser. It gets all data from the REST API and also holds the API's request and response types, which the host already references.
- `tests/Ledgerly.Core.Tests`: xUnit tests for Core, using fake adapters.

All dependencies point towards Core (dependency inversion), and project references enforce it at compile time.

The UI uses Blazor WebAssembly and a REST API instead of Blazor Server. Blazor Server would be simpler, with no API at all, but a REST API is an explicit target skill for this project, and here it has a real consumer instead of existing only for show.

Adapters share one Infrastructure project rather than one project each: the ports already isolate them, so separate projects would add build overhead without adding isolation.

## Consequences

- Core cannot reference HTTP, the database or any broker: the compiler rejects it.
- Core logic is testable without network access or a database.
- The API is a real boundary: the UI can only do what the API exposes, and the API can be called and tested on its own.
- Downside: more moving parts than Blazor Server: an extra project, request and response types to maintain, and HTTP calls that can fail between UI and host.
- Downside: the API must be bound to localhost only (NFR03), since it now exposes portfolio data over HTTP.
