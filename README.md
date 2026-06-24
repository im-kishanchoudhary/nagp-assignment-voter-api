# Voter API: Multi-Tier .NET 10 + PostgreSQL on Kubernetes

A two-tier app deployed on Kubernetes. A stateless **.NET 10 Web API** (service tier) sits in front
of **PostgreSQL** (database tier). The API manages voter records through a REST interface, reports
its own health, and tags every response with the pod and node that handled the request.

## Links

- GitHub Repository: `https://github.com/im-kishanchoudhary/nagp-assignment-voter-api.git`
- Docker Image: `docker.io/kishanchoudhary/voter-api`
- API URL: `http://136.116.106.91/api/voters`

## Project Structure

- `src/VoterApi/`: .NET 10 Web API (controllers, services, EF Core data layer)
- `src/VoterApi/Migrations/`: EF Core migrations (schema and seed)
- `manifests/`: Kubernetes manifests (Namespace, ConfigMaps, Secret, StatefulSet, Services, migration Job, Ingress, HPA)
- `src/VoterApi/Dockerfile`: multi-stage build that also produces the EF migration bundle

## Features

- .NET 10 Web API with a layered structure (API, Service, Data) and EF Core over PostgreSQL
- Full CRUD on a single `voters` table with a unique business id (`id_number`) and audit columns
- Liveness and readiness probes, where readiness verifies real database connectivity
- `X-Pod-Name`, `X-Node-Name` and `X-Version` headers populated from the Kubernetes downward API
- REST endpoints with Swagger/OpenAPI documentation
- Connection details in a ConfigMap and the database password in a Secret, never in plain YAML
- PostgreSQL on a StatefulSet with a PVC so data survives pod restarts
- Schema and seed managed by EF Core migrations, applied by a migration Job that ships inside the image
- Rolling updates and self-healing on the 4-replica API Deployment
- Horizontal Pod Autoscaler (4 to 10) on CPU, plus CPU and memory requests and limits for FinOps
