# Testing Guide - Strangler Fig Migration Demo

This guide provides step-by-step instructions for testing each step of the migration.

## Prerequisites

- .NET 9 SDK installed
- Terminal or PowerShell
- curl or any HTTP client (Postman, etc.)

## Step 1: Testing the Monolith

### Start the Application
```powershell
cd src\Step1_Monolith\LegacyApp
dotnet run
```

### Test Products Endpoints
```powershell
# List products
curl http://localhost:5001/products

# Create a product
curl -X POST http://localhost:5001/products `
  -H "Content-Type: application/json" `
  -d '{"name":"Laptop","price":999.99}'

# List products again to see the new one
curl http://localhost:5001/products
```

### Test Orders Endpoints
```powershell
# List orders
curl http://localhost:5001/orders

# Create an order
curl -X POST http://localhost:5001/orders `
  -H "Content-Type: application/json" `
  -d '{"customerName":"John Doe","totalAmount":1299.99}'

# List orders again
curl http://localhost:5001/orders
```

### Test Customers Endpoints
```powershell
# List customers
curl http://localhost:5001/customers

# Create a customer
curl -X POST http://localhost:5001/customers `
  -H "Content-Type: application/json" `
  -d '{"name":"Jane Smith","email":"jane@example.com"}'

# List customers again
curl http://localhost:5001/customers
```

Press Ctrl+C to stop the application.

---

## Step 2: Testing the Strangler Facade

This step requires running THREE applications simultaneously.

### Terminal 1: Start Legacy App
```powershell
cd src\Step2_StranglerFacade\LegacyApp
dotnet run
```
Should see: `Now listening on: http://localhost:5001`

### Terminal 2: Start Products Service
```powershell
cd src\Step2_StranglerFacade\Products.Service
dotnet run
```
Should see: `Now listening on: http://localhost:5002`

### Terminal 3: Start Strangler Facade
```powershell
cd src\Step2_StranglerFacade\StranglerFacade
dotnet run
```
Should see: `Now listening on: http://localhost:5000`

### Test Through the Facade (Port 5000)

All requests go through the facade, which routes them appropriately.

#### Test Products (Routed to Products.Service on 5002)
```powershell
# Create product through facade
curl -X POST http://localhost:5000/products `
  -H "Content-Type: application/json" `
  -d '{"name":"Smartphone","price":799.99}'

# List products through facade
curl http://localhost:5000/products
```

#### Test Orders (Routed to LegacyApp on 5001)
```powershell
# Create order through facade
curl -X POST http://localhost:5000/orders `
  -H "Content-Type: application/json" `
  -d '{"customerName":"Alice Johnson","totalAmount":899.99}'

# List orders through facade
curl http://localhost:5000/orders
```

#### Test Customers (Routed to LegacyApp on 5001)
```powershell
# Create customer through facade
curl -X POST http://localhost:5000/customers `
  -H "Content-Type: application/json" `
  -d '{"name":"Bob Wilson","email":"bob@example.com"}'

# List customers through facade
curl http://localhost:5000/customers
```

### Verify Direct Access Still Works

You can also access services directly to verify they're working independently:

```powershell
# Direct to Products.Service
curl http://localhost:5002/products

# Direct to LegacyApp
curl http://localhost:5001/orders
curl http://localhost:5001/customers
```

Press Ctrl+C in each terminal to stop all applications.

---

## Step 3: Testing Full Extraction

This step requires running THREE independent microservices.

### Terminal 1: Start Products Service
```powershell
cd src\Step3_FullExtraction\Products.Service
dotnet run
```
Should see: `Now listening on: http://localhost:5002`

### Terminal 2: Start Orders Service
```powershell
cd src\Step3_FullExtraction\Orders.Service
dotnet run
```
Should see: `Now listening on: http://localhost:5003`

### Terminal 3: Start Customers Service
```powershell
cd src\Step3_FullExtraction\Customers.Service
dotnet run
```
Should see: `Now listening on: http://localhost:5004`

### Test Each Service Independently

#### Test Products Service (Port 5002)
```powershell
curl -X POST http://localhost:5002/products `
  -H "Content-Type: application/json" `
  -d '{"name":"Tablet","price":599.99}'

curl http://localhost:5002/products
```

#### Test Orders Service (Port 5003)
```powershell
curl -X POST http://localhost:5003/orders `
  -H "Content-Type: application/json" `
  -d '{"customerName":"Carol Davis","totalAmount":1199.99}'

curl http://localhost:5003/orders
```

#### Test Customers Service (Port 5004)
```powershell
curl -X POST http://localhost:5004/customers `
  -H "Content-Type: application/json" `
  -d '{"name":"David Brown","email":"david@example.com"}'

curl http://localhost:5004/customers
```

Press Ctrl+C in each terminal to stop all applications.

---

## Key Observations

### Step 1: Monolith
- Single process, single database
- Everything accessible through one port (5001)
- Simplest to run but tightly coupled

### Step 2: Strangler Facade
- Three processes running simultaneously
- Single entry point through facade (5000)
- Products extracted to its own service
- Legacy still handles Orders and Customers
- Demonstrates incremental migration strategy

### Step 3: Full Extraction
- Three independent microservices
- Each with its own port and database
- Complete separation of concerns
- Ready for independent scaling and deployment

## Troubleshooting

### Port Already in Use
If you get an error about a port being in use:
```powershell
# Windows: Find and kill the process
netstat -ano | findstr :5001
taskkill /PID <process_id> /F
```

### Database Issues
If you encounter database errors, delete the .db files and restart:
```powershell
# In the project directory
Remove-Item *.db
dotnet run
```

### YARP Routing Issues (Step 2)
If requests aren't being routed correctly:
1. Ensure all three services are running
2. Check the logs in the StranglerFacade terminal
3. Verify the URLs in appsettings.json match the running services

## Next Steps

After exploring this demo:
1. Add authentication/authorization
2. Implement health checks
3. Add distributed tracing
4. Implement service-to-service communication
5. Add API Gateway features (rate limiting, caching)
6. Containerize with Docker
7. Deploy to Kubernetes
