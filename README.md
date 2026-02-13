# RetailPulseAI

Production-ready multi-tenant SaaS platform for Retail Measurement Services and AI analytics.

## Solution Components

- `RetailPulseAI.API` (.NET 8 Web API)
- `RetailPulseAI.Application` (CQRS + MediatR + validation)
- `RetailPulseAI.Domain` (core entities and contracts)
- `RetailPulseAI.Infrastructure` (tenant resolution, AI services, background jobs)
- `RetailPulseAI.Persistence` (EF Core SQL Server + repositories)
- `RetailPulseAI.Frontend` (React 18 + Material UI operations UI)

## Backend Highlights

- Clean Architecture with DTOs and CQRS
- Multi-tenant shared database with `TenantId` and EF Core global query filters
- JWT authentication + role/policy authorization
- Redis caching for dashboard endpoint
- Serilog logging and Swagger
- Demo seed data + sample migration

## Frontend Highlights (React 18 + MUI)

- Responsive navigation with `AppBar` + `Drawer`
- Elevated dashboard cards and analytics sections built with `Card`, `Grid`, `Button`, and `Typography`
- Session context panel with accessible `TextField` forms and status alerts
- AI insights workflows with improved information density and hierarchy
- Consistent theme (spacing, palette, typography, hover transitions)
- Mobile-first layout behavior for small screens and desktop optimization for larger screens

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
