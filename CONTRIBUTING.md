# Contributing to Student Project Planner

Thank you for your interest in contributing to the Student Project Planner! This document provides guidelines and instructions for contributing to this project.

## 📋 Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Coding Standards](#coding-standards)
- [Commit Guidelines](#commit-guidelines)
- [Pull Request Process](#pull-request-process)
- [Project Structure](#project-structure)
- [Testing Guidelines](#testing-guidelines)
- [Documentation](#documentation)

## 🤝 Code of Conduct

### Our Pledge

We are committed to providing a welcoming and inspiring environment for all contributors. We pledge to:

- Use welcoming and inclusive language
- Be respectful of differing viewpoints and experiences
- Gracefully accept constructive criticism
- Focus on what is best for the project
- Show empathy towards other contributors

### Expected Behavior

- **Be professional** in all interactions
- **Be collaborative** and help others when possible
- **Be respectful** of others' time and contributions
- **Communicate clearly** and constructively
- **Follow the guidelines** outlined in this document

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)
- [SQL Server](https://www.microsoft.com/sql-server) or SQL Server Express

### Setting Up Your Development Environment

1. **Fork the repository** (if external contributor)

   ```bash
   # Fork via GitHub UI, then clone your fork
   git clone https://github.com/YOUR-USERNAME/CSE-325-Team-14.git
   ```

2. **Clone the repository** (team members)

   ```bash
   git clone https://github.com/florian-kausche/CSE-325-Team-14.git
   cd CSE-325-Team-14
   ```

3. **Install dependencies**

   ```bash
   dotnet restore
   ```

4. **Set up database**

   ```bash
   # Update connection string in appsettings.json
   # Then run migrations
   dotnet ef database update
   ```

5. **Run the application**

   ```bash
   dotnet run
   ```

6. **Verify setup**
   - Navigate to `https://localhost:5001`
   - Ensure the application loads correctly

## 🔄 Development Workflow

### Branch Strategy

We follow a simplified Git workflow:

- **`main`** - Production-ready code (protected)
- **`develop`** - Integration branch for features
- **`feature/feature-name`** - Individual feature branches
- **`bugfix/bug-name`** - Bug fix branches
- **`hotfix/issue-name`** - Critical production fixes

### Creating a New Feature

1. **Create a branch from `develop`**

   ```bash
   git checkout develop
   git pull origin develop
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes**
   - Write clean, well-documented code
   - Follow coding standards (see below)
   - Test your changes thoroughly

3. **Commit your changes**

   ```bash
   git add .
   git commit -m "feat: add user authentication feature"
   ```

4. **Push to remote**

   ```bash
   git push origin feature/your-feature-name
   ```

5. **Create a Pull Request**
   - Go to GitHub
   - Create PR from your feature branch to `develop`
   - Fill out the PR template
   - Request review from team members

### Bug Fixes

1. **Create a bugfix branch**

   ```bash
   git checkout develop
   git checkout -b bugfix/issue-description
   ```

2. **Fix the bug and test**
   - Identify root cause
   - Implement fix
   - Add tests if applicable
   - Verify fix works

3. **Commit and push**

   ```bash
   git commit -m "fix: resolve login authentication issue"
   git push origin bugfix/issue-description
   ```

4. **Create Pull Request**

## 💻 Coding Standards

### C# Coding Conventions

Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions):

#### Naming Conventions

```csharp
// Classes and methods - PascalCase
public class StudentService { }
public void GetStudentAssignments() { }

// Private fields - _camelCase with underscore
private readonly ILogger _logger;

// Properties - PascalCase
public string AssignmentName { get; set; }

// Local variables and parameters - camelCase
var studentCount = 10;
public void ProcessAssignment(string assignmentName) { }

// Constants - PascalCase
public const int MaxAssignments = 100;

// Interfaces - PascalCase with 'I' prefix
public interface IAssignmentRepository { }
```

#### Code Formatting

```csharp
// Use meaningful names
// ❌ Bad
var d = DateTime.Now;
var list = new List<Assignment>();

// ✅ Good
var currentDate = DateTime.Now;
var assignments = new List<Assignment>();

// Always use braces for control structures
// ❌ Bad
if (isValid)
    return true;

// ✅ Good
if (isValid)
{
    return true;
}

// Use var when type is obvious
// ✅ Good
var students = new List<Student>();
var connection = CreateConnection();

// Explicit type when not obvious
// ✅ Good
IEnumerable<Student> activeStudents = GetActiveStudents();
```

#### LINQ and Queries

```csharp
// Use method syntax for simple queries
var activeAssignments = assignments
    .Where(a => a.IsActive)
    .OrderBy(a => a.DueDate)
    .ToList();

// Use query syntax for complex queries
var results = from assignment in assignments
              where assignment.CourseId == courseId
              join course in courses on assignment.CourseId equals course.Id
              select new { assignment.Name, course.Title };
```

### Blazor Component Standards

#### Component Structure

```csharp
@page "/assignments"
@using StudentProjectPlanner.Models
@inject IAssignmentService AssignmentService

<h3>Assignments</h3>

@if (assignments == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <table class="table">
        @foreach (var assignment in assignments)
        {
            <tr>
                <td>@assignment.Name</td>
                <td>@assignment.DueDate.ToShortDateString()</td>
            </tr>
        }
    </table>
}

@code {
    private List<Assignment>? assignments;

    protected override async Task OnInitializedAsync()
    {
        assignments = await AssignmentService.GetAssignmentsAsync();
    }
}
```

#### Component Best Practices

1. **Keep components small and focused**
   - One responsibility per component
   - Extract reusable logic into services

2. **Use parameters for component communication**

   ```csharp
   [Parameter]
   public int AssignmentId { get; set; }

   [Parameter]
   public EventCallback<Assignment> OnAssignmentSelected { get; set; }
   ```

3. **Handle loading and error states**
   ```csharp
   @if (isLoading)
   {
       <LoadingSpinner />
   }
   else if (errorMessage != null)
   {
       <ErrorAlert Message="@errorMessage" />
   }
   else
   {
       <!-- Content -->
   }
   ```

### Database Conventions

#### Entity Models

```csharp
public class Assignment
{
    // Primary key
    public int Id { get; set; }

    // Required fields
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public int CourseId { get; set; }
    public Course? Course { get; set; }

    // Audit fields
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

#### Repository Pattern

```csharp
public interface IAssignmentRepository
{
    Task<Assignment?> GetByIdAsync(int id);
    Task<List<Assignment>> GetAllAsync();
    Task<Assignment> AddAsync(Assignment assignment);
    Task UpdateAsync(Assignment assignment);
    Task DeleteAsync(int id);
}
```

## 📝 Commit Guidelines

### Commit Message Format

We follow the [Conventional Commits](https://www.conventionalcommits.org/) specification:

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Commit Types

- **feat**: New feature
- **fix**: Bug fix
- **docs**: Documentation changes
- **style**: Code style changes (formatting, no logic change)
- **refactor**: Code refactoring
- **test**: Adding or updating tests
- **chore**: Build process or auxiliary tool changes

### Examples

```bash
# Feature
feat(auth): add user registration functionality

# Bug fix
fix(dashboard): resolve deadline calculation error

# Documentation
docs(readme): update installation instructions

# Refactoring
refactor(services): extract assignment logic to service layer

# Test
test(assignment): add unit tests for assignment creation
```

### Commit Best Practices

1. **Make atomic commits** - One logical change per commit
2. **Write clear messages** - Explain what and why, not how
3. **Use present tense** - "add feature" not "added feature"
4. **Reference issues** - Include issue numbers when applicable
5. **Keep commits small** - Easier to review and revert if needed

## 🔍 Pull Request Process

### PR Checklist

Before submitting a pull request, ensure:

- [ ] Code follows project coding standards
- [ ] All tests pass locally
- [ ] New code has appropriate tests
- [ ] Documentation is updated if needed
- [ ] Commit messages follow conventions
- [ ] Branch is up to date with `develop`
- [ ] No merge conflicts exist
- [ ] Code has been self-reviewed

### PR Template

When creating a PR, include:

```markdown
## Description

Brief description of what this PR does

## Type of Change

- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing

How was this tested? What edge cases were considered?

## Screenshots

If applicable, add screenshots

## Checklist

- [ ] Code follows style guidelines
- [ ] Self-review completed
- [ ] Tests added/updated
- [ ] Documentation updated
```

### Review Process

1. **Submit PR** to `develop` branch
2. **Request review** from at least one team member
3. **Address feedback** - make requested changes
4. **Obtain approval** from reviewer(s)
5. **Merge** - Squash and merge into `develop`

### Review Guidelines

When reviewing PRs:

- **Be respectful** and constructive
- **Explain reasoning** behind suggestions
- **Check for** logic errors, edge cases, performance issues
- **Verify** tests pass and code works as intended
- **Approve** only when confident in the changes

## 🏗 Project Structure

```
CSE-325-Team-14/
├── docs/                          # Project documentation
│   ├── W03-Meeting-Notes.md
│   └── PROJECT_STRUCTURE.md
├── src/                           # Source code
│   ├── StudentProjectPlanner/     # Main application
│   │   ├── Components/            # Blazor components
│   │   │   ├── Pages/             # Page components
│   │   │   ├── Layout/            # Layout components
│   │   │   └── Shared/            # Shared components
│   │   ├── Data/                  # Database context and migrations
│   │   ├── Models/                # Domain models
│   │   ├── Services/              # Business logic services
│   │   ├── Repositories/          # Data access layer
│   │   └── wwwroot/               # Static files
│   └── StudentProjectPlanner.Tests/  # Unit tests
├── .gitignore
├── README.md
├── CONTRIBUTING.md
└── LICENSE
```

## 🧪 Testing Guidelines

### Unit Testing

```csharp
[Fact]
public async Task GetAssignmentById_ReturnsAssignment_WhenExists()
{
    // Arrange
    var repository = new AssignmentRepository(_context);
    var expectedAssignment = new Assignment { Id = 1, Name = "Test" };

    // Act
    var result = await repository.GetByIdAsync(1);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedAssignment.Name, result.Name);
}
```

### Test Coverage

- Aim for **80%+ code coverage** for critical paths
- Write tests for:
  - Business logic in services
  - Data access in repositories
  - Edge cases and error handling
  - Component interactions

### Running Tests

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true

# Run specific test
dotnet test --filter "FullyQualifiedName~AssignmentServiceTests"
```

## 📚 Documentation

### Code Documentation

Use XML documentation comments:

```csharp
/// <summary>
/// Retrieves all assignments for a specific course.
/// </summary>
/// <param name="courseId">The unique identifier of the course.</param>
/// <returns>A list of assignments for the specified course.</returns>
/// <exception cref="NotFoundException">Thrown when course is not found.</exception>
public async Task<List<Assignment>> GetAssignmentsByCourseAsync(int courseId)
{
    // Implementation
}
```

### Documentation Updates

When adding features:

1. Update relevant **README** sections
2. Add/update **inline code comments**
3. Update **API documentation** if applicable
4. Add **usage examples** for complex features
5. Update **project structure** documentation

## 🆘 Getting Help

### Resources

- **Project Trello**: [CSE-325-Team-14](https://trello.com/b/ZF5tgwbE/cse-325-team-14)
- **GitHub Issues**: Submit questions or issues
- **Course Materials**: CSE 325 Canvas resources

### Questions?

If you have questions:

1. Check existing documentation
2. Search closed GitHub issues
3. Ask on team communication channel
4. Create a new GitHub issue

## 🎓 Learning Resources

### .NET Blazor

- [Official Blazor Documentation](https://docs.microsoft.com/aspnet/core/blazor/)
- [Blazor University](https://blazor-university.com/)

### Entity Framework Core

- [EF Core Documentation](https://docs.microsoft.com/ef/core/)
- [EF Core Tutorial](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)

### C# Best Practices

- [C# Coding Conventions](https://docs.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [Clean Code in C#](https://github.com/thangchung/clean-code-dotnet)

## 📄 License

By contributing to this project, you agree that your contributions will be licensed under the MIT License.

---

**Thank you for contributing to Student Project Planner! 🎓✨**

If you have suggestions for improving these guidelines, please submit a PR with your proposed changes.
