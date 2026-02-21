# Strangler Fig Pattern Migration Demo

This solution demonstrates the **Strangler Fig Pattern** for migrating a monolithic application to microservices incrementally.

## Pattern Overview

The Strangler Fig pattern (named after the strangler fig tree that grows around existing trees) involves:
1. Starting with a monolithic application
2. Gradually extracting functionality into separate services
3. Using a facade/proxy to route requests to new services while legacy handles the rest
4. Eventually replacing the entire monolith

## Solution Structure

### Step 1: The Monolith
- **LegacyApp**: A single ASP.NET Core application handling Products, Orders, and Customers
- All functionality in one codebase with a single database
- Runs on port 5001

### Step 2: Strangler Facade (Partial Migration)
- **Products.Service**: Extracted microservice handling only Products (port 5002)
- **LegacyApp**: Modified monolith handling Orders and Customers (port 5001)
- **StranglerFacade**: YARP reverse proxy routing requests (port 5000)
  - Routes `/products/**` → Products.Service
  - Routes everything else → LegacyApp
- This step demonstrates the transition phase where both old and new coexist

### Step 3: Full Extraction
- **Products.Service**: Products microservice (port 5002)
- **Orders.Service**: Orders microservice (port 5003)
- **Customers.Service**: Customers microservice (port 5004)
- Each service has its own database and runs independently
- The monolith is fully replaced

## Running the Steps

### Step 1
```bash
cd src/Step1_Monolith/LegacyApp
dotnet run
# Access at http://localhost:5001
```

### Step 2
```bash
# Terminal 1 - Legacy App
cd src/Step2_StranglerFacade/LegacyApp
dotnet run

# Terminal 2 - Products Service
cd src/Step2_StranglerFacade/Products.Service
dotnet run

# Terminal 3 - Strangler Facade
cd src/Step2_StranglerFacade/StranglerFacade
dotnet run
# Access through facade at http://localhost:5000
```

### Step 3
```bash
# Terminal 1
cd src/Step3_FullExtraction/Products.Service
dotnet run

# Terminal 2
cd src/Step3_FullExtraction/Orders.Service
dotnet run

# Terminal 3
cd src/Step3_FullExtraction/Customers.Service
dotnet run
```

## Key Technologies

- **.NET 9**: Latest .NET runtime
- **ASP.NET Core Minimal API**: Lightweight HTTP APIs
- **EF Core 9**: Entity Framework Core for data access
- **SQLite**: Embedded database
- **YARP**: Yet Another Reverse Proxy for request routing

## Benefits of Strangler Fig Pattern

1. **Incremental Migration**: No "big bang" rewrite
2. **Risk Mitigation**: Each service can be tested independently
3. **Business Continuity**: System remains operational throughout migration
4. **Flexibility**: Can pause or adjust strategy at any point
5. **Learning Opportunity**: Team learns microservices incrementally

## Use Cases

- Modernizing legacy applications
- Breaking up monoliths into microservices
- Introducing new technology stack gradually
- Testing microservices architecture before full commitment
