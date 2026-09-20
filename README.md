# 🚀 Customer Management System — Backend API

A robust, production-ready **ASP.NET Core Web API** built for a Customer Management System, following **Clean Architecture** principles with enterprise-grade security and best practices.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core-ORM-blue)
![JWT](https://img.shields.io/badge/Auth-JWT-black)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927?logo=microsoftsqlserver)

---

## 🌟 Key Features

- **Clean Architecture** — Strict `Controller → Service → Repository` layering for maintainability and separation of concerns
- **JWT Authentication** — Secure, stateless authentication for protected endpoints
- **Password Hashing** — `BCrypt.Net-Next` for salted password hashing (no plain-text storage)
- **Global Exception Handling** — Centralized middleware returning clean, standardized error responses
- **DTO-based Validation** — Data Annotations enforce input integrity at the boundary
- **CORS-Ready** — Pre-configured for Angular (`localhost:4200`) and React (`localhost:5173`) clients
- **Swagger/OpenAPI** — Interactive API docs out of the box

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (C#) |
| ORM | Entity Framework Core |
| Database | Microsoft SQL Server |
| Auth | JWT (JwtBearer) |
| Security | BCrypt.Net-Next |
| Docs | Swagger / OpenAPI |

---

## 📂 Project Architecture

```
├── Properties/
│   └── launchSettings.json
├── Controllers/
│   ├── AuthController.cs
│   └── CustomerController.cs
├── Data/
│   └── AppDbContext.cs
├── Dto/
│   ├── AuthResponseDto.cs
│   ├── CreateUpdateCustomerDto.cs
│   ├── CustomerDto.cs
│   ├── LoginUserDto.cs
│   └── RegisterUserDto.cs
├── Middlewares/
│   └── GlobalExceptionMiddleware.cs
├── Migrations/
├── Model/
│   ├── Customer.cs
│   └── User.cs
├── Repositories/
│   ├── AuthRepository.cs
│   ├── CustomerRepository.cs
│   ├── IAuthRepository.cs
│   └── ICustomerRepository.cs
├── Services/
│   ├── AuthService.cs
│   ├── CustomerService.cs
│   ├── IAuthService.cs
│   └── ICustomerService.cs
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
└── Test_Backend.http
```

This layered structure keeps each concern isolated, making the codebase easy to test, extend, and maintain.

---

## 🚀 Getting Started

### Prerequisites
- .NET SDK 8.0+
- SQL Server / SQL Server Express (SSMS recommended)
- Visual Studio 2022 or VS Code

### 1. Clone & Restore
```bash
git clone <your-repo-url>
cd <project-folder>
dotnet restore
```

### 2. Configure Secrets
Sensitive config is kept out of `appsettings.json` via .NET User Secrets:

```bash
dotnet user-secrets init

dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.\SQLEXPRESS;Database=sarasa003;Trusted_Connection=True;TrustServerCertificate=True;"

dotnet user-secrets set "Jwt:Key" "YourSuperSecretKeyForJwtAuthentication2026!@#"
```

### 3. Apply Migrations
```bash
dotnet ef database update
```

### 4. Run
```bash
dotnet run
```
API available at `https://localhost:<port>` — explore endpoints via Swagger at `/swagger`.

---

## 📡 API Endpoints

### 🔐 Authentication
| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Auth/register` | Register a new user |
| POST | `/api/Auth/login` | Login and receive JWT |

### 👤 Customers *(Requires Bearer Token)*
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Customer` | Get all customers |
| GET | `/api/Customer/{id}` | Get customer by ID |
| POST | `/api/Customer` | Create a customer |
| PUT | `/api/Customer/{id}` | Update a customer |
| DELETE | `/api/Customer/{id}` | Delete a customer |

---

## 🧩 Architecture Flow

```
Client Request → Controller → Service (Business Logic) → Repository (EF Core) → SQL Server
                     ↓
            GlobalExceptionMiddleware (catches errors)
```

---

<p align="center">Built with ❤️ — focused on clean code, security, and scalability.</p>