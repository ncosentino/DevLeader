# Step 2: Strangler Facade (Partial Migration)

This step demonstrates the **transition phase** of the Strangler Fig pattern.

## Architecture

```
                    ┌──────────────────────────────┐
                    │  StranglerFacade (5000)      │
                    │    (YARP Reverse Proxy)      │
                    └──────────────────────────────┘
                              │
                ┌─────────────┴─────────────┐
                │                           │
                ▼                           ▼
┌───────────────────────────┐   ┌───────────────────────────┐
│  Products.Service (5002)  │   │    LegacyApp (5001)       │
│                           │   │                           │
│  ┌──────────┐            │   │  ┌──────────┐            │
│  │ Products │            │   │  │  Orders  │            │
│  │   API    │            │   │  │   API    │            │
│  └──────────┘            │   │  └──────────┘            │
│                           │   │  ┌──────────┐            │
│  ┌─────────────────┐     │   │  │Customers │            │
│  │ProductsDbContext│     │   │  │   API    │            │
│  │   (SQLite DB)   │     │   │  └──────────┘            │
│  └─────────────────┘     │   │                           │
└───────────────────────────┘   │  ┌─────────────────┐     │
                                │  │  AppDbContext   │     │
                                │  │   (SQLite DB)   │     │
                                │  └─────────────────┘     │
                                └───────────────────────────┘
```

## Key Concept: The Strangler Facade

The **StranglerFacade** is a YARP-based reverse proxy that:
1. Intercepts all incoming requests
2. Routes `/products/**` to the new Products.Service
3. Routes everything else to the legacy monolith
4. Clients don't know the difference

## What Changed

### Extracted
- **Products.Service**: New microservice handling all product operations
  - Owns its product data in a separate database
  - Runs independently on port 5002
  - Can be scaled independently

### Modified
- **LegacyApp**: Products functionality removed
  - Now only handles Orders and Customers
  - Products table removed from database
  - Still runs on port 5001

### Added
- **StranglerFacade**: The routing layer
  - YARP configuration routes requests
  - Single entry point for clients (port 5000)
  - Enables gradual migration

## Running

Start all three components:

```bash
# Terminal 1 - Legacy App
cd LegacyApp
dotnet run

# Terminal 2 - Products Service
cd Products.Service
dotnet run

# Terminal 3 - Strangler Facade
cd StranglerFacade
dotnet run
```

Access through the facade: http://localhost:5000

## Testing

```bash
# These go to Products.Service
curl http://localhost:5000/products
curl -X POST http://localhost:5000/products -d '{"name":"Widget","price":9.99}' -H "Content-Type: application/json"

# These go to LegacyApp
curl http://localhost:5000/orders
curl http://localhost:5000/customers
```

## Benefits

- **Zero Downtime**: Migration happens without taking the system offline
- **Reversible**: Can route back to legacy if new service has issues
- **Testable**: New service can be validated before full cutover
- **Incremental**: Extract one domain at a time

## Next Step

Continue extracting Orders and Customers services until the monolith is fully replaced (see Step 3).
