# RetailPulseAI

Production-ready multi-tenant SaaS platform for Retail Measurement Services and AI analytics.

## Solution Components

- `RetailPulseAI.API` (.NET 8 Web API)
- `RetailPulseAI.Application` (CQRS + MediatR + validation)
- `RetailPulseAI.Domain` (core entities and contracts)
- `RetailPulseAI.Infrastructure` (tenant resolution, AI services, background jobs)
- `RetailPulseAI.Persistence` (EF Core SQL Server + repositories)
- `RetailPulseAI.Frontend` (React 18 operations UI)

## Backend Highlights

- Clean Architecture with DTOs and CQRS
- Multi-tenant shared database with `TenantId` and EF Core global query filters
- JWT authentication + role/policy authorization
- Redis caching for dashboard endpoint
- Serilog logging and Swagger
- Demo seed data + sample migration

## Frontend Highlights (React 18)

- Session context panel to request JWT and switch role/tenant context
- Dashboard UI for overview and sales trends
- Market analytics UI for Market Share, ACV, and Price Index
- AI insights UI for 4-week forecast, promotion lift, and pricing optimization
- Role-aware UX for pricing optimization actions

## Run Frontend

```bash
cd RetailPulseAI.Frontend
npm install
npm run dev
```

Set API base URL if needed:

```bash
VITE_API_BASE_URL=http://localhost:5000 npm run dev
```

## Main API Endpoints

- `GET /api/dashboard/overview`
- `GET /api/sales/trends`
- `GET /api/marketshare`
- `GET /api/acv`
- `GET /api/pricing/index`
- `GET /api/forecast/{sku}`
- `POST /api/pricing/optimize`
- `GET /api/promotions/lift`
- `POST /api/auth/token`
