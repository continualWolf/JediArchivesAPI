# The Jedi Archives API

> _“If an item does not appear in our records, it does not exist.”_  
> — Jocasta Nu, Chief Archivist of the Jedi Order

The **Jedi Archives API** is a scalable, secure, and modular web API modeled after the legendary data repositories of the Jedi Temple on Coruscant. Built with enterprise-grade architecture and galactic foresight, it handles requests from all ranks of Jedi — from Padawan learners to Grand Masters of the Council.

Whether you are tracking Jedi lineage, promoting Knights, or mapping galactic sectors, this API serves as the canonical access point to the Jedi Order's accumulated knowledge.

---

## 🧠 Architecture 

### ✨ CQRS — Command Query Responsibility Segregation

The API is structured using the **CQRS pattern** to clearly separate:
- 🟡 **Commands** — Operations that modify the state of the galaxy (e.g. `CreateUserCommand`)
- 🔵 **Queries** — Force-sensitive lookups and read-only access (e.g. `GetUserByIdQuery`)

(A separate Database using reflection is available but must be handled by the user at this time, just ensure to update the `JediArchives_Write` & `JediArchives_Read` Connection Strings)

Advantages:
- Clean logic and separation of concerns
- Ready for read/write replication, event sourcing, and caching
- Easier to scale galactically

Disadvantages:
- Can be overly complicated and slow to implement

Reasoning:
- For smaller minimal API's using MediatR and CQRS may be considered overkill, however for my Jedi Archives API i am imagining that this API's endpoints will grow exponentially as the Jedi Archives are said to contain all the knowledge in the galaxy.
---

## 🔐 Security 

- **JWT Authentication** — Secure login system issues access tokens for each user
- **Role-Based Authorization** — Every endpoint requires appropriate Jedi clearance (`Padawan`, `Archivist`, `CouncilMember`, `GrandMaster`)
- **Rate Limiting** — Prevents brute-force attacks and misuse from suspicious systems in the Outer Rim
- **Custom `[JediAuthorize]` Attribute** — Protects endpoints based on Jedi rank with an expressive, readable syntax

---

## ⚙️ Powered By the Force (Tech Stack)

| Tool/Library          | Use                          |
|-----------------------|------------------------------|
| .NET 9                | Core framework               |
| EF Core               | Data access layer            |
| MediatR               | CQRS command/query bus       |
| JWT Auth              | Secure access control        |
| Swagger/OpenAPI       | API docs + exploration       |
| ASP.NET Rate Limiting | Request protection           |

---

## 📦 Features

- ✅ Register, update, retrieve, and delete Jedi
- ✅ Login for token-based authentication
- ✅ Enforce rank-based access to sensitive operations
- ✅ Full CQRS command/query infrastructure
- ✅ Modular project structure for future galaxy-expanding features

---

## 🗺️ API Exploration

- **Swagger UI**: `/swagger`
- **Explain a Command**: `/api/docs/explain/CreateUserCommand`
- **Health Check**: `/health`
- **Current User Info**: `/users/me`

---

## 🔭 Roadmap (What I'm doing next)

- 🧩 Modular Feature Modules (e.g. `Planets`, `Species`, `Wanted Criminals`)
- 🌐 Front-End Application to interact with the API
- 🗺️ Interactive Galactic Map built with HTML Canvas
- 🛡️ Audit Logs & Change Tracking for system accountability
- 🧮 Admin Dashboard
- 🚀 Dockerize the project

---

## 🧪 Testing

- ✅ xUnit-based unit and integration tests
- ✅ In-memory EF Core for isolation
- ✅ Behavior tested for MediatR handlers, validators, and controllers

---

## 🚀 Getting Started

```bash
# Clone the archives from Coruscant
git clone https://github.com/yourname/jedi-archives-api.git

# Move into the temple
cd jedi-archives-api

# Restore galactic dependencies
dotnet restore

# Prepare the Archives (apply migrations)
dotnet ef database update

# Start the Force
dotnet run


