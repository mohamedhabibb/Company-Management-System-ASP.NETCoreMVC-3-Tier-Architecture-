# 🏢 Company Management System — ASP.NET Core MVC (3-Tier Architecture)

A web application for managing **Departments** and **Employees**, built with **ASP.NET Core MVC** and a clean **3-Tier Architecture** (Presentation, Business Logic, Data Access). It uses the **Repository + Unit of Work** patterns, **AutoMapper**, **Entity Framework Core**, and **ASP.NET Core Identity** for authentication.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-Code%20First-blue)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?logo=bootstrap&logoColor=white)

---

## 📑 Table of Contents

- [Features](#-features)
- [Architecture](#-architecture)
- [Project Structure](#-project-structure)
- [Tech Stack](#-tech-stack)
- [Getting Started](#-getting-started)
- [Configuration](#-configuration)
- [Authentication Flow](#-authentication-flow)
- [Design Patterns Used](#-design-patterns-used)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)
- [License](#-license)
- [Author](#-author)

---

## ✨ Features

### 🏬 Department Management
- Full **CRUD** (Create, Read, Update, Delete)
- Details page for each department
- Server-side and **client-side validation**

### 👨‍💼 Employee Management
- Full **CRUD** with an async workflow
- Assign an employee to a **Department**
- **Search** employees by name
- **Upload / Delete profile images** (file handling via a Document Settings helper)
- Image preview in the Index view
- Reusable **Partial Views**

### 🔐 Authentication & Authorization (ASP.NET Core Identity)
- Register new account
- Login / Sign out
- **Forgot Password** (sends reset link by email)
- **Reset Password**
- Email sending service (SMTP)

### 🧱 Engineering
- 3-Tier layered architecture
- Generic Repository + specific repositories
- Unit of Work (with proper `Dispose`)
- AutoMapper for Entity ↔ ViewModel mapping
- Dependency Injection throughout
- Fully **asynchronous** controllers and data access

---

## 🏛 Architecture

```
┌──────────────────────────────────────────┐
│   Presentation Layer (PL)                │
│   MVC Controllers · Views · ViewModels   │
└───────────────────┬──────────────────────┘
                    │ depends on
┌───────────────────▼──────────────────────┐
│   Business Logic Layer (BLL)             │
│   Interfaces · Repositories · UnitOfWork │
└───────────────────┬──────────────────────┘
                    │ depends on
┌───────────────────▼──────────────────────┐
│   Data Access Layer (DAL)                │
│   Models · DbContext · Configurations    │
│   Migrations                             │
└───────────────────┬──────────────────────┘
                    ▼
              SQL Server Database
```

| Layer | Responsibility |
|-------|----------------|
| **PL** – Presentation | Handles HTTP requests, routing, views, validation, and mapping ViewModels |
| **BLL** – Business Logic | Business rules, repository interfaces/implementations, Unit of Work |
| **DAL** – Data Access | EF Core `DbContext`, entities, fluent configurations, migrations |

---

## 📁 Project Structure

```
CompanyApp/
│
├── CompanyApp.PL/                  # Presentation Layer (ASP.NET Core MVC)
│   ├── Controllers/
│   │   ├── DepartmentController.cs
│   │   ├── EmployeeController.cs
│   │   └── AccountController.cs
│   ├── Views/
│   │   ├── Department/
│   │   ├── Employee/
│   │   ├── Account/
│   │   └── Shared/                 # Layout + Partial Views
│   ├── ViewModels/
│   ├── Helpers/
│   │   ├── DocumentSettings.cs     # Upload / Delete files
│   │   └── EmailSettings.cs        # Send email
│   ├── MappingProfiles/
│   ├── wwwroot/
│   │   └── Files/Images/           # Uploaded images
│   ├── appsettings.json
│   └── Program.cs
│
├── CompanyApp.BLL/                 # Business Logic Layer
│   ├── Interfaces/
│   │   ├── IGenericRepository.cs
│   │   ├── IDepartmentRepository.cs
│   │   ├── IEmployeeRepository.cs
│   │   └── IUnitOfWork.cs
│   └── Repositories/
│       ├── GenericRepository.cs
│       ├── DepartmentRepository.cs
│       ├── EmployeeRepository.cs
│       └── UnitOfWork.cs
│
└── CompanyApp.DAL/                 # Data Access Layer
    ├── Contexts/
    │   └── AppDbContext.cs
    ├── Models/
    │   ├── Department.cs
    │   ├── Employee.cs
    │   └── ApplicationUser.cs
    ├── Configurations/
    └── Migrations/
```

> 📝 Rename folders/namespaces to match your actual solution.

---

## 🛠 Tech Stack

| Category | Technology |
|----------|------------|
| Framework | ASP.NET Core MVC (.NET 8) |
| Language | C# |
| ORM | Entity Framework Core (Code First) |
| Database | SQL Server |
| Authentication | ASP.NET Core Identity |
| Mapping | AutoMapper |
| Frontend | Razor Views, Bootstrap, jQuery Validation |
| Email | SMTP (MailKit / System.Net.Mail) |

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) (or LocalDB)
- Visual Studio 2022 / VS Code / Rider
- Git

### Installation

```bash
# 1. Clone the repository
git clone https://github.com/<your-username>/<your-repo>.git
cd <your-repo>

# 2. Restore packages
dotnet restore

# 3. Update the connection string in appsettings.json (see Configuration)

# 4. Apply migrations to create the database
dotnet ef database update --project CompanyApp.DAL --startup-project CompanyApp.PL

# 5. Run the app
dotnet run --project CompanyApp.PL
```

Then open: `https://localhost:5001`

### Creating the First Migration (if starting fresh)

```bash
dotnet ef migrations add InitialCreate --project CompanyApp.DAL --startup-project CompanyApp.PL
dotnet ef database update --project CompanyApp.DAL --startup-project CompanyApp.PL
```

---

## ⚙️ Configuration

Edit `CompanyApp.PL/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=CompanyDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "MailSettings": {
    "Email": "your-email@gmail.com",
    "DisplayName": "Company App",
    "Password": "your-app-password",
    "Host": "smtp.gmail.com",
    "Port": 587
  }
}
```

> ⚠️ **Never commit real credentials.** Use [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) or environment variables:
> ```bash
> dotnet user-secrets set "MailSettings:Password" "your-app-password" --project CompanyApp.PL
> ```

Add this to `.gitignore` if you keep local settings files:

```
appsettings.Development.json
**/wwwroot/Files/
```

---

## 🔑 Authentication Flow

```
Register ──► Login ──► Access Protected Pages ──► Sign Out

Forgot Password ──► Enter Email ──► Receive Reset Link
                ──► Reset Password ──► Login
```

1. **Register** – creates an `ApplicationUser` with Identity.
2. **Login** – validates credentials and signs the user in with a cookie.
3. **Sign Out** – clears the authentication cookie.
4. **Forgot Password** – generates a reset token and emails a link.
5. **Reset Password** – validates the token and sets the new password.

---

## 🧩 Design Patterns Used

| Pattern | Purpose |
|---------|---------|
| **Repository** | Abstracts data access behind interfaces |
| **Generic Repository** | Reuses common CRUD logic across entities |
| **Unit of Work** | Coordinates repositories and commits changes in one transaction |
| **Dependency Injection** | Loose coupling between layers |
| **ViewModel / AutoMapper** | Keeps entities separate from the UI |
| **Partial Views** | Reusable UI components |

### Example — Unit of Work

```csharp
public interface IUnitOfWork : IDisposable
{
    IDepartmentRepository DepartmentRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    Task<int> CompleteAsync();
}
```

### Example — Async Controller Action

```csharp
[HttpPost]
public async Task<IActionResult> Create(EmployeeViewModel model)
{
    if (!ModelState.IsValid) return View(model);

    model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");

    var employee = _mapper.Map<Employee>(model);
    await _unitOfWork.EmployeeRepository.AddAsync(employee);
    await _unitOfWork.CompleteAsync();

    return RedirectToAction(nameof(Index));
}
```

---

## 🗺 Roadmap

- [x] 3-Tier architecture setup
- [x] Department CRUD
- [x] Employee CRUD with Department relation
- [x] Generic Repository & Unit of Work
- [x] AutoMapper
- [x] Search
- [x] Image upload / delete
- [x] Async operations
- [x] Identity (Register / Login / Logout)
- [x] Forgot / Reset password via email
- [ ] Roles & Authorization (Admin / User)
- [ ] Pagination & sorting
- [ ] Unit tests
- [ ] Docker support
- [ ] Deploy to Azure

---

## 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Commit your changes: `git commit -m "Add amazing feature"`
4. Push to the branch: `git push origin feature/amazing-feature`
5. Open a Pull Request

---

## 👤 Author

**Mohamed Abdelnasser Ali**
