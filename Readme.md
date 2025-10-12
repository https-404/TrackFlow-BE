
#  TrackFlow-BE

TrackFlow is a lightweight project management and task tracking system (similar to Linear, but simpler).  
Built with **.NET 9**, it follows **Clean Architecture** principles to keep code maintainable, scalable, and testable.

---

## 📂 Project Structure

The solution is organized into four layers:

```

src/
├── TrackFlow\.Api             → ASP.NET Core Web API (entry point)
├── TrackFlow\.Application     → Application logic (services, use-cases, DTOs)
├── TrackFlow\.Domain          → Core domain models & business rules
├── TrackFlow\.Infrastructure  → EF Core, database, and external integrations

````

### ✅ Responsibilities
- **Domain**  
  Pure business models, entities, and core logic.  
  → No external dependencies.

- **Application**  
  Business use-cases, service layer, DTOs, interfaces.  
  → Depends only on **Domain**.

- **Infrastructure**  
  Implementation of persistence (EF Core repositories), database migrations, and 3rd-party integrations.  
  → Depends on **Domain**.

- **API**  
  ASP.NET Core controllers, request/response handling, Swagger, dependency injection setup.  
  → Depends on **Application** & **Infrastructure**.

---

## 🛠️ Tech Stack

- **.NET 8 (ASP.NET Core Web API)**
- **Entity Framework Core (Code First)**
- **Repository Pattern**
- **Clean Architecture**
- **Swagger / OpenAPI** for API docs

---

## ⚡ Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/https-404/TrackFlow-BE.git
cd TrackFlow
````

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Run the API

```bash
cd src/TrackFlow.Api
dotnet run
```

API will be available at:
👉 `https://localhost:5001/swagger`

---

## 📖 Roadmap

* [ ] User authentication & authorization
* [ ] Task & project CRUD
* [ ] Assignments & status updates
* [ ] Notifications
* [ ] Deployment (Docker + Cloud)

