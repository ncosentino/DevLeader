# Step 1: The Monolith

This step represents the **starting state** - a traditional monolithic application.

## Architecture

```
┌─────────────────────────────────┐
│       LegacyApp (5001)          │
│                                 │
│  ┌──────────┐  ┌──────────┐   │
│  │ Products │  │  Orders  │   │
│  │   API    │  │   API    │   │
│  └──────────┘  └──────────┘   │
│  ┌──────────┐                  │
│  │Customers │                  │
│  │   API    │                  │
│  └──────────┘                  │
│                                 │
│    ┌─────────────────────┐     │
│    │   AppDbContext      │     │
│    │  (Single SQLite DB) │     │
│    └─────────────────────┘     │
└─────────────────────────────────┘
```

## Characteristics

- **Single Codebase**: All features in one ASP.NET Core project
- **Single Database**: All tables in one SQLite database
- **Tight Coupling**: Features share the same process and database
- **Simple Deployment**: One application to deploy

## Endpoints

- `GET /products` - List all products
- `POST /products` - Create a product
- `GET /orders` - List all orders
- `POST /orders` - Create an order
- `GET /customers` - List all customers
- `POST /customers` - Create a customer

## Running

```bash
cd LegacyApp
dotnet run
```

Access at: http://localhost:5001

## The Problem

While simple, this monolithic approach has limitations:
- All features must be deployed together
- Scaling requires scaling the entire application
- Technology choices affect the entire system
- Changes in one area can break others
- Large teams can have merge conflicts

This is why we migrate to microservices using the Strangler Fig pattern.
