# RetailPulseAI

Production-ready multi-tenant SaaS backend template for Retail Measurement Services and AI analytics.

## Architecture

- RetailPulseAI.API
- RetailPulseAI.Application
- RetailPulseAI.Domain
- RetailPulseAI.Infrastructure
- RetailPulseAI.Persistence

## Highlights

- Clean Architecture with CQRS (MediatR)
- Multi-tenant shared DB model using `TenantId` and EF Core query filters
- JWT authentication + role/policy authorization
- Redis-backed caching for dashboard overview
- Serilog logging + Swagger
- Background worker for analytics refresh hooks
- SQL Server persistence and seed data for a demo tenant

## Main Endpoints

- `GET /api/dashboard/overview`
- `GET /api/sales/trends`
- `GET /api/marketshare`
- `GET /api/acv`
- `GET /api/pricing/index`
- `GET /api/forecast/{sku}`
- `POST /api/pricing/optimize`
- `GET /api/promotions/lift`
- `POST /api/auth/token`
