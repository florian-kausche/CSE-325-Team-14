# Student Project Planner - Project Structure

This document outlines the planned architecture and structure for the Student Project Planner application.

## 📋 Table of Contents

- [Architecture Overview](#architecture-overview)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Data Models](#data-models)
- [Component Architecture](#component-architecture)
- [Service Layer](#service-layer)
- [Database Schema](#database-schema)
- [Security Architecture](#security-architecture)
- [UI/UX Design](#uiux-design)

## 🏗 Architecture Overview

The Student Project Planner follows a **layered architecture** pattern with clear separation of concerns:

```
┌─────────────────────────────────────────────────────┐
│              Presentation Layer (Blazor)             │
│  Components | Pages | Layouts | Client-Side Logic   │
└─────────────────────────────────────────────────────┘
                         ↕
┌─────────────────────────────────────────────────────┐
│               Business Logic Layer                   │
│     Services | Validators | Business Rules          │
└─────────────────────────────────────────────────────┘
                         ↕
┌─────────────────────────────────────────────────────┐
│              Data Access Layer                       │
│   Repositories | DbContext | Entity Framework       │
└─────────────────────────────────────────────────────┘
                         ↕
┌─────────────────────────────────────────────────────┐
│                  Database Layer                      │
│        SQL Server | SQLite (Development)            │
└─────────────────────────────────────────────────────┘
```

### Architecture Principles

1. **Separation of Concerns** - Each layer has distinct responsibilities
2. **Dependency Injection** - Loose coupling through DI container
3. **Repository Pattern** - Abstraction over data access
4. **Service Layer** - Business logic isolated from presentation
5. **Component-Based UI** - Reusable Blazor components

## 🛠 Technology Stack

### Frontend

- **.NET Blazor Server** - Interactive server-side UI
- **HTML/CSS** - Markup and styling
- **Bootstrap 5** - Responsive UI framework
- **JavaScript Interop** - Client-side functionality when needed

### Backend

- **.NET 8.0** - Application framework
- **ASP.NET Core** - Web framework
- **Entity Framework Core 8.0** - ORM for database access
- **ASP.NET Core Identity** - Authentication and authorization

### Database

- **SQL Server** - Production database
- **SQLite** - Development/testing database
- **Entity Framework Migrations** - Schema management

### Development Tools

- **Visual Studio 2022** or **VS Code** - IDE
- **Git** - Version control
- **GitHub** - Repository hosting
- **Trello** - Project management

### Testing

- **xUnit** - Unit testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Assertion library

## 📁 Project Structure

### Directory Layout

```
CSE-325-Team-14/
├── docs/                                    # Documentation
│   ├── W03-Meeting-Notes.md
│   ├── PROJECT_STRUCTURE.md
│   ├── DATABASE_SCHEMA.md (future)
│   └── API_DOCUMENTATION.md (future)
│
├── src/                                     # Source code
│   ├── StudentProjectPlanner/               # Main application
│   │   ├── Components/                      # Blazor components
│   │   │   ├── Pages/                       # Routable page components
│   │   │   │   ├── Index.razor              # Home/Dashboard
│   │   │   │   ├── Login.razor              # Login page
│   │   │   │   ├── Register.razor           # Registration page
│   │   │   │   ├── Courses/                 # Course management
│   │   │   │   │   ├── CourseList.razor
│   │   │   │   │   ├── CourseCreate.razor
│   │   │   │   │   └── CourseEdit.razor
│   │   │   │   ├── Assignments/             # Assignment management
│   │   │   │   │   ├── AssignmentList.razor
│   │   │   │   │   ├── AssignmentCreate.razor
│   │   │   │   │   └── AssignmentEdit.razor
│   │   │   │   └── GroupProjects/           # Group project management
│   │   │   │       ├── ProjectList.razor
│   │   │   │       ├── ProjectCreate.razor
│   │   │   │       ├── ProjectDetails.razor
│   │   │   │       └── TaskManagement.razor
│   │   │   │
│   │   │   ├── Layout/                      # Layout components
│   │   │   │   ├── MainLayout.razor         # Main app layout
│   │   │   │   ├── NavMenu.razor            # Navigation menu
│   │   │   │   └── LoginLayout.razor        # Login/Register layout
│   │   │   │
│   │   │   └── Shared/                      # Shared/reusable components
│   │   │       ├── LoadingSpinner.razor
│   │   │       ├── ErrorAlert.razor
│   │   │       ├── ConfirmDialog.razor
│   │   │       ├── AssignmentCard.razor
│   │   │       ├── CourseSelector.razor
│   │   │       ├── DeadlineCounter.razor
│   │   │       └── StatusBadge.razor
│   │   │
│   │   ├── Data/                            # Database context
│   │   │   ├── ApplicationDbContext.cs      # EF Core DbContext
│   │   │   ├── DbInitializer.cs             # Database seeding
│   │   │   └── Migrations/                  # EF migrations
│   │   │
│   │   ├── Models/                          # Domain models/entities
│   │   │   ├── ApplicationUser.cs           # User entity (extends IdentityUser)
│   │   │   ├── Course.cs                    # Course entity
│   │   │   ├── Assignment.cs                # Assignment entity
│   │   │   ├── GroupProject.cs              # Group project entity
│   │   │   ├── ProjectTask.cs               # Task entity
│   │   │   ├── ProjectMember.cs             # Project membership
│   │   │   └── Enums/                       # Enumerations
│   │   │       ├── AssignmentStatus.cs
│   │   │       ├── TaskStatus.cs
│   │   │       └── Priority.cs
│   │   │
│   │   ├── DTOs/                            # Data Transfer Objects
│   │   │   ├── CourseDto.cs
│   │   │   ├── AssignmentDto.cs
│   │   │   ├── ProjectDto.cs
│   │   │   └── UserDto.cs
│   │   │
│   │   ├── Repositories/                    # Data access layer
│   │   │   ├── Interfaces/
│   │   │   │   ├── IRepository.cs           # Generic repository interface
│   │   │   │   ├── ICourseRepository.cs
│   │   │   │   ├── IAssignmentRepository.cs
│   │   │   │   ├── IGroupProjectRepository.cs
│   │   │   │   └── IUserRepository.cs
│   │   │   │
│   │   │   └── Implementations/
│   │   │       ├── Repository.cs            # Generic repository
│   │   │       ├── CourseRepository.cs
│   │   │       ├── AssignmentRepository.cs
│   │   │       ├── GroupProjectRepository.cs
│   │   │       └── UserRepository.cs
│   │   │
│   │   ├── Services/                        # Business logic layer
│   │   │   ├── Interfaces/
│   │   │   │   ├── ICourseService.cs
│   │   │   │   ├── IAssignmentService.cs
│   │   │   │   ├── IGroupProjectService.cs
│   │   │   │   ├── IAuthService.cs
│   │   │   │   └── IDashboardService.cs
│   │   │   │
│   │   │   └── Implementations/
│   │   │       ├── CourseService.cs
│   │   │       ├── AssignmentService.cs
│   │   │       ├── GroupProjectService.cs
│   │   │       ├── AuthService.cs
│   │   │       └── DashboardService.cs
│   │   │
│   │   ├── Validators/                      # Input validation
│   │   │   ├── CourseValidator.cs
│   │   │   ├── AssignmentValidator.cs
│   │   │   └── ProjectValidator.cs
│   │   │
│   │   ├── Utilities/                       # Helper classes
│   │   │   ├── DateTimeHelper.cs
│   │   │   ├── StringExtensions.cs
│   │   │   └── Constants.cs
│   │   │
│   │   ├── wwwroot/                         # Static files
│   │   │   ├── css/
│   │   │   │   ├── app.css                  # Custom styles
│   │   │   │   └── bootstrap/               # Bootstrap files
│   │   │   ├── js/
│   │   │   │   └── site.js                  # Custom JavaScript
│   │   │   ├── images/
│   │   │   │   └── logo.png
│   │   │   └── favicon.ico
│   │   │
│   │   ├── Program.cs                       # Application entry point
│   │   ├── appsettings.json                 # Configuration
│   │   ├── appsettings.Development.json     # Development config
│   │   └── StudentProjectPlanner.csproj     # Project file
│   │
│   └── StudentProjectPlanner.Tests/         # Unit tests
│       ├── Services/
│       │   ├── CourseServiceTests.cs
│       │   ├── AssignmentServiceTests.cs
│       │   └── GroupProjectServiceTests.cs
│       ├── Repositories/
│       │   ├── CourseRepositoryTests.cs
│       │   └── AssignmentRepositoryTests.cs
│       ├── Validators/
│       │   └── AssignmentValidatorTests.cs
│       └── StudentProjectPlanner.Tests.csproj
│
├── .gitignore                               # Git ignore rules
├── README.md                                # Project overview
├── CONTRIBUTING.md                          # Contribution guidelines
├── LICENSE                                  # License file
└── StudentProjectPlanner.sln                # Solution file
```

## 📊 Data Models

### Entity Relationship Overview

```
ApplicationUser (IdentityUser)
    ├── Courses (1:many)
    ├── Assignments (1:many)
    └── ProjectMembers (1:many)

Course
    ├── Assignments (1:many)
    └── User (many:1)

Assignment
    ├── Course (many:1)
    └── User (many:1)

GroupProject
    ├── ProjectMembers (1:many)
    └── ProjectTasks (1:many)

ProjectMember
    ├── GroupProject (many:1)
    └── User (many:1)

ProjectTask
    ├── GroupProject (many:1)
    └── AssignedUser (many:1)
```

### Core Entities

#### ApplicationUser

```csharp
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<Course> Courses { get; set; }
    public ICollection<Assignment> Assignments { get; set; }
    public ICollection<ProjectMember> ProjectMemberships { get; set; }
}
```

#### Course

```csharp
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CourseCode { get; set; }
    public string Semester { get; set; }
    public string Color { get; set; } // For UI differentiation

    // Foreign keys
    public string UserId { get; set; }

    // Navigation properties
    public ApplicationUser User { get; set; }
    public ICollection<Assignment> Assignments { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

#### Assignment

```csharp
public class Assignment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Priority Priority { get; set; }

    // Foreign keys
    public int CourseId { get; set; }
    public string UserId { get; set; }

    // Navigation properties
    public Course Course { get; set; }
    public ApplicationUser User { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
```

#### GroupProject

```csharp
public class GroupProject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }

    // Navigation properties
    public ICollection<ProjectMember> Members { get; set; }
    public ICollection<ProjectTask> Tasks { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

#### ProjectTask

```csharp
public class ProjectTask
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime? DueDate { get; set; }

    // Foreign keys
    public int GroupProjectId { get; set; }
    public string? AssignedUserId { get; set; }

    // Navigation properties
    public GroupProject GroupProject { get; set; }
    public ApplicationUser? AssignedUser { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
```

#### ProjectMember

```csharp
public class ProjectMember
{
    public int Id { get; set; }
    public string Role { get; set; } // e.g., "Owner", "Member"

    // Foreign keys
    public int GroupProjectId { get; set; }
    public string UserId { get; set; }

    // Navigation properties
    public GroupProject GroupProject { get; set; }
    public ApplicationUser User { get; set; }

    // Audit
    public DateTime JoinedAt { get; set; }
}
```

### Enumerations

```csharp
public enum AssignmentStatus
{
    NotStarted = 0,
    InProgress = 1,
    Completed = 2
}

public enum TaskStatus
{
    NotStarted = 0,
    InProgress = 1,
    Completed = 2
}

public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2
}
```

## 🎨 Component Architecture

### Page Components

#### Dashboard (Index.razor)

- Displays upcoming assignments
- Shows course overview
- Displays group project status
- Quick action buttons

#### Course Management

- **CourseList.razor** - List all courses with actions
- **CourseCreate.razor** - Create new course form
- **CourseEdit.razor** - Edit existing course

#### Assignment Management

- **AssignmentList.razor** - List assignments (filterable by course/status)
- **AssignmentCreate.razor** - Create new assignment
- **AssignmentEdit.razor** - Edit existing assignment

#### Group Project Management

- **ProjectList.razor** - List all group projects
- **ProjectCreate.razor** - Create new group project
- **ProjectDetails.razor** - View project details and tasks
- **TaskManagement.razor** - Manage project tasks

### Shared Components

#### UI Components

```razor
<!-- LoadingSpinner.razor -->
<div class="spinner-border" role="status">
    <span class="visually-hidden">Loading...</span>
</div>

<!-- ErrorAlert.razor -->
@if (!string.IsNullOrEmpty(Message))
{
    <div class="alert alert-danger">@Message</div>
}

<!-- ConfirmDialog.razor -->
<!-- Modal for confirming delete actions -->
```

#### Business Components

```razor
<!-- AssignmentCard.razor -->
<!-- Displays assignment info with status badge -->

<!-- DeadlineCounter.razor -->
<!-- Shows days until deadline with color coding -->

<!-- StatusBadge.razor -->
<!-- Displays status with appropriate color -->
```

## ⚙ Service Layer

### Service Interfaces

```csharp
public interface IAssignmentService
{
    Task<List<AssignmentDto>> GetAllAssignmentsAsync(string userId);
    Task<List<AssignmentDto>> GetAssignmentsByCourseAsync(int courseId);
    Task<List<AssignmentDto>> GetUpcomingAssignmentsAsync(string userId);
    Task<AssignmentDto> GetAssignmentByIdAsync(int id);
    Task<AssignmentDto> CreateAssignmentAsync(AssignmentDto assignment);
    Task UpdateAssignmentAsync(int id, AssignmentDto assignment);
    Task DeleteAssignmentAsync(int id);
    Task UpdateStatusAsync(int id, AssignmentStatus status);
}

public interface ICourseService
{
    Task<List<CourseDto>> GetAllCoursesAsync(string userId);
    Task<CourseDto> GetCourseByIdAsync(int id);
    Task<CourseDto> CreateCourseAsync(CourseDto course);
    Task UpdateCourseAsync(int id, CourseDto course);
    Task DeleteCourseAsync(int id);
}

public interface IGroupProjectService
{
    Task<List<ProjectDto>> GetUserProjectsAsync(string userId);
    Task<ProjectDto> GetProjectByIdAsync(int id);
    Task<ProjectDto> CreateProjectAsync(ProjectDto project);
    Task UpdateProjectAsync(int id, ProjectDto project);
    Task DeleteProjectAsync(int id);
    Task AddMemberAsync(int projectId, string userId, string role);
    Task RemoveMemberAsync(int projectId, string userId);
    Task<List<ProjectTask>> GetProjectTasksAsync(int projectId);
    Task CreateTaskAsync(ProjectTask task);
    Task UpdateTaskStatusAsync(int taskId, TaskStatus status);
}

public interface IDashboardService
{
    Task<DashboardData> GetDashboardDataAsync(string userId);
}
```

## 🗄 Database Schema

### Connection String Configuration

```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentProjectPlanner;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}

// appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=StudentProjectPlanner.db"
  }
}
```

### DbContext Configuration

```csharp
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<GroupProject> GroupProjects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure relationships
        builder.Entity<Course>()
            .HasOne(c => c.User)
            .WithMany(u => u.Courses)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Assignment>()
            .HasOne(a => a.Course)
            .WithMany(c => c.Assignments)
            .HasForeignKey(a => a.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Additional configurations...
    }
}
```

## 🔒 Security Architecture

### Authentication Flow

1. User registers with email/password
2. Password is hashed using ASP.NET Core Identity
3. User logs in with credentials
4. Authentication cookie is created
5. User accesses protected pages
6. Authorization checks user's identity

### Authorization Rules

```csharp
// Users can only access their own data
public async Task<List<Assignment>> GetUserAssignments(string userId)
{
    return await _context.Assignments
        .Where(a => a.UserId == userId)
        .ToListAsync();
}

// Users can only modify their own resources
public async Task<bool> UserOwnsAssignment(int assignmentId, string userId)
{
    var assignment = await _context.Assignments.FindAsync(assignmentId);
    return assignment?.UserId == userId;
}
```

### Security Measures

1. **Password Security**
   - Hashed with PBKDF2
   - Salted automatically
   - Minimum complexity requirements

2. **Input Validation**
   - Server-side validation
   - Client-side validation (for UX)
   - Data annotations on models

3. **SQL Injection Prevention**
   - Entity Framework parameterized queries
   - No raw SQL where possible

4. **XSS Prevention**
   - Razor automatic encoding
   - Content Security Policy headers

5. **CSRF Protection**
   - Anti-forgery tokens
   - Enabled by default in Blazor

## 🎨 UI/UX Design

### Design Principles

1. **Simplicity** - Clean, uncluttered interface
2. **Consistency** - Uniform design patterns
3. **Responsiveness** - Mobile-first design
4. **Accessibility** - WCAG 2.1 compliance
5. **Usability** - Intuitive navigation

### Color Scheme

```css
:root {
  --primary-color: #007bff;
  --secondary-color: #6c757d;
  --success-color: #28a745;
  --danger-color: #dc3545;
  --warning-color: #ffc107;
  --info-color: #17a2b8;
  --light-color: #f8f9fa;
  --dark-color: #343a40;
}
```

### Layout Structure

```
┌─────────────────────────────────────────────┐
│              Header / Nav Bar                │
├─────────────────────────────────────────────┤
│                                             │
│                                             │
│              Main Content Area              │
│                                             │
│                                             │
├─────────────────────────────────────────────┤
│                  Footer                     │
└─────────────────────────────────────────────┘
```

### Responsive Breakpoints

- **Mobile**: < 576px
- **Tablet**: 576px - 768px
- **Desktop**: 768px - 1200px
- **Large Desktop**: > 1200px

## 🚀 Deployment Architecture

### Development Environment

- Local IIS Express
- SQLite database
- Hot reload enabled

### Production Environment (Future)

- Azure App Service
- Azure SQL Database
- CI/CD via GitHub Actions

---

**Document Version**: 1.0  
**Last Updated**: Week 03, Spring 2026  
**Next Review**: After initial implementation
