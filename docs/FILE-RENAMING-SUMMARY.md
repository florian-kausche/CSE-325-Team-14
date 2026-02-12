# File Renaming & .NET Module Documentation

## File Renaming Summary

### Changes Made

#### 1. Dashboard Component

- **Old name**: `Index.razor`
- **New name**: `Dashboard.razor`
- **Route**: `@page "/"`
- **Purpose**: Main dashboard page displaying overview metrics and quick actions
- **Contains**: Summary cards, progress bars, upcoming/overdue assignments, getting started guide

#### 2. Group Projects Component

- **Old name**: `GroupProjects.razor`
- **New name**: `Projects.razor`
- **Route**: `@page "/projects"`
- **Purpose**: Collaborative group project management
- **Contains**: Project listing, member management, task tracking

### Rationale for Renaming

File names now clearly reflect their content and functionality:

- `Dashboard.razor` - Explicitly identifies it as the dashboard/home page
- `Projects.razor` - Simpler name for group projects management
- Follows community naming conventions for Blazor components
- More maintainable and easier for team navigation

## Unchanged Files (Already Well-Named)

✅ `Courses.razor` - Course management page  
✅ `Assignments.razor` - Assignment tracking page  
✅ `Login.razor` - User authentication  
✅ `Register.razor` - Account creation  
✅ `Logout.razor` - Session cleanup  
✅ `MainLayout.razor` - Application layout  
✅ `NavMenu.razor` - Navigation menu  
✅ `Routes.razor` - Routing configuration  
✅ `RedirectToLogin.razor` - Authentication redirect

## Directory Structure

```
Components/
├── Pages/
│   ├── Dashboard.razor        ← Renamed from Index.razor
│   ├── Courses.razor
│   ├── Assignments.razor
│   ├── Projects.razor         ← Renamed from GroupProjects.razor
│   ├── Login.razor
│   ├── Register.razor
│   └── Logout.razor
├── Layout/
│   ├── MainLayout.razor
│   └── NavMenu.razor
├── Routes.razor
├── RedirectToLogin.razor
└── _Imports.razor
```

## .NET Built-in Modules Summary

### Application Framework

✅ **ASP.NET Core** - Web framework and HTTP pipeline  
✅ **Blazor** - Interactive component-based UI  
✅ **Razor Components** - Server-side components

### Authentication & Security

✅ **ASP.NET Core Identity** - User management, password hashing, roles  
✅ **Cookie Authentication** - Session management  
✅ **Authorization** - Role-based access control  
✅ **CSRF Protection** - Antiforgery tokens

### Data Access & Persistence

✅ **Entity Framework Core 8.0** - ORM for database access  
✅ **SQLite** - Development database  
✅ **SQL Server** - Production database provider  
✅ **Migrations** - Database schema management

### Component & UI Framework

✅ **Blazor Components** - Reusable UI components  
✅ **Forms & Validation** - EditForm, InputText, DataAnnotationsValidator  
✅ **Routing** - Client-side and server-side routing  
✅ **Cascading Parameters** - Component data flow

### Data Validation

✅ **System.ComponentModel.DataAnnotations** - [Required], [MaxLength], [Range]  
✅ **Client & Server Validation** - Form validation framework

### Dependency Injection

✅ **Built-in DI Container** - Service registration and resolution  
✅ **Scoped Services** - Repository and Service layer  
✅ **Configuration** - appsettings.json configuration files

### Logging & Monitoring

✅ **Microsoft.Extensions.Logging** - Application logging  
✅ **Structured Logging** - Formatted log output

## No External Dependencies

✓ **100% Microsoft First-Party Packages** - All functionality uses only official Microsoft packages  
✓ **No Third-Party Authentication** - Using built-in ASP.NET Core Identity  
✓ **No Third-Party ORM** - Using Entity Framework Core  
✓ **No Third-Party UI Libraries** - Using built-in Blazor components  
✓ **No Third-Party Validation** - Using built-in DataAnnotations

### NuGet Packages (All Microsoft)

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
</ItemGroup>
```

## Technical Implementation

### Built-in Features Used

#### Authentication Flow

1. `Register.razor` - Creates new user via `UserManager.CreateAsync()`
2. ASP.NET Core Identity hashes password with PBKDF2
3. `Login.razor` - Authenticates via `SignInManager.PasswordSignInAsync()`
4. Cookie-based session management
5. `AuthorizeRouteView` protects components
6. `Logout.razor` - Clears session via `SignInManager.SignOutAsync()`

#### Database Operations

1. Entity Framework Core maps C# classes to database tables
2. Migrations create schema automatically
3. LINQ queries compile to SQL
4. Relationships managed through navigation properties
5. Lazy loading for related entities

#### Component Authorization

1. `@attribute [Authorize]` requires authentication
2. `CascadingAuthenticationState` provides auth context
3. `AuthenticationState` parameter gets current user
4. `UserManager.GetUserAsync()` retrieves user details

### Middleware Pipeline

```csharp
app.UseHttpsRedirection()      // HTTPS enforcement
app.UseStaticFiles()           // Static assets (CSS, JS)
app.UseRouting()               // Route matching
app.UseAuthentication()        // User identification
app.UseAuthorization()         // Permission checks
app.UseAntiforgery()          // CSRF protection
app.MapRazorComponents()       // Component routing
```

## Verification

✅ **Build Successful** - `dotnet build` with 0 errors  
✅ **Application Running** - Server listening on http://localhost:5000  
✅ **Components Working** - All renamed components function correctly  
✅ **Routes Accessible** - All page routes respond correctly  
✅ **Database Connected** - SQLite database initialized and migrations applied

## Summary

The Student Project Planner application:

- ✅ Uses exclusively Microsoft .NET built-in modules
- ✅ Implements proper authentication with ASP.NET Core Identity
- ✅ Manages data with Entity Framework Core
- ✅ Builds UI with Blazor components
- ✅ Uses standard design patterns (Repository, Service, Dependency Injection)
- ✅ Enforces security best practices (HTTPS, CSRF, password hashing, authorization)
- ✅ Has well-organized, properly named components for maintainability

All core functionality is implemented using only .NET's native capabilities - no external frameworks or third-party libraries needed.
