# CrudHub

**CrudHub** is a .NET project providing a **generic CRUD API** with **real-time updates** via **SignalR**, using the **UnitOfWork pattern** and secured with **JWT Bearer Authentication**.  
This project serves as a reusable template for building modular, secure, real-time APIs.

---

## Features

- **Generic CRUD**: Easily manage any entity using a reusable repository pattern.
- **UnitOfWork Pattern**: Encapsulates multiple repositories and ensures transactional consistency.
- **SignalR**: Real-time notifications to connected clients when entities are modified.
- **JWT Bearer Authentication**: Secures endpoints using tokens.
- **Code First**: The database is generated from models using EF Core migrations.
- **Database First (optional)**: Ability to generate DbContext and entities from an existing database.
- **Extensible**: Easily add new entities and SignalR events without changing the core architecture.

---

## Tech Stack

- **.NET 9 / C#**
- **Entity Framework Core** for database operations
- **SignalR** for real-time updates
- **JWT** for authentication
- **SQLite / SQL Server** (configurable)
- **UnitOfWork + Generic Repository Pattern**

---

## Database Approaches

- **Code First**: All tables and relationships are created from entity classes (`Models`) via EF Core migrations.  
- **Database First (optional)**: You can generate the DbContext and entities from an existing database:

```bash
dotnet ef dbcontext scaffold "YourConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o Models
```

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server or SQLite installed (depending on your choice)

### Installation

Clone the repository:

```bash
git clone https://github.com/your-username/CrudHub.git
cd CrudHub
```

### Update appsettings.json with your database connection string and JWT configuration:
```bash
{
  "AppSettings": {
    "Token": "your-secret-key",
    "Issuer": "CrudHubAPI",
    "Audience": "CrudHubClient"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CrudHub;Trusted_Connection=True;"
  }
}
```
### 🔑 Authentication
```bash
Login
POST /api/users/login
```

## Request body:
```
{ 
    "User":"alice@example.com",
    "Password":"alice123"

}
```

## Response:
```bash
{
  "token": "your-jwt-token"
}
```

