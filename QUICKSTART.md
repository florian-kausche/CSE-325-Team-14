# Student Project Planner - Quick Start Guide

## ✅ Status: COMPLETE & RUNNING

**Application URL**: http://localhost:5001  
**Database**: SQLite (StudentProjectPlanner.db)  
**Framework**: ASP.NET Core 8.0 + Blazor Server

---

## 🚀 Quick Start (30 seconds)

### Start the Application

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
cd "c:\Users\Moandemah Foundation\Desktop\new school\CSE325\CSE-325-Team-14\src\StudentProjectPlanner"
dotnet run
```

### Access Application

Open browser: **http://localhost:5001**

### Create Account

1. Click "Register"
2. Enter: First Name, Last Name, Email, Password
3. Click "Create Account"
4. Dashboard loads automatically

---

## 📊 File Changes Summary

### Renamed Components

| Old Name            | New Name        | Route       |
| ------------------- | --------------- | ----------- |
| Index.razor         | Dashboard.razor | `/`         |
| GroupProjects.razor | Projects.razor  | `/projects` |

### Why?

File names now clearly reflect their content for better team maintainability.

---

## 🛠 Technology Stack (Built-in .NET Only)

### 5. Run the Application

Start the Blazor application:

```powershell
cd src\StudentProjectPlanner
dotnet run
```

The application will start and display the URL (typically `https://localhost:5001`).

### 6. Open in Browser

Open your web browser and navigate to the URL shown in the console output.

## 🔧 Development Workflow

### Using Visual Studio 2022

1. Open `StudentProjectPlanner.sln` in Visual Studio
2. Press **F5** to run with debugging, or **Ctrl+F5** to run without debugging
3. The application will open in your default browser

### Using Visual Studio Code

1. Open the project folder in VS Code
2. Install the C# extension if not already installed
3. Press **F5** to start debugging
4. Or use the integrated terminal to run `dotnet run`

## 🧪 Running Tests

To run the unit tests:

```powershell
dotnet test
```

## 📂 Project Structure

```
CSE-325-Team-14/
├── docs/                          # Documentation
├── src/
│   ├── StudentProjectPlanner/     # Main application
│   └── StudentProjectPlanner.Tests/  # Unit tests
├── README.md
├── CONTRIBUTING.md
└── LICENSE
```

## 🐛 Troubleshooting

### Port Already in Use

If you see an error about port 5001 being in use:

```powershell
# Stop all dotnet processes
Get-Process dotnet | Stop-Process -Force

# Or run on a different port
dotnet run --urls "http://localhost:5010"
```

### Build Errors

If you encounter build errors:

1. Clean the solution:

   ```powershell
   dotnet clean
   ```

2. Restore packages:

   ```powershell
   dotnet restore
   ```

3. Rebuild:
   ```powershell
   dotnet build
   ```

### Database Issues

For database-related issues (will be relevant once database is implemented):

```powershell
# Update database
dotnet ef database update

# Drop and recreate database
dotnet ef database drop
dotnet ef database update
```

## 📚 Next Steps

1. Review the [Project Structure](docs/PROJECT_STRUCTURE.md) document
2. Read the [Contributing Guidelines](CONTRIBUTING.md)
3. Check the [W03 Meeting Notes](docs/W03-Meeting-Notes.md)
4. Start implementing features according to the project plan

## 🔗 Useful Links

- **GitHub Repository**: https://github.com/florian-kausche/CSE-325-Team-14
- **Trello Board**: https://trello.com/b/ZF5tgwbE/cse-325-team-14
- **.NET Documentation**: https://docs.microsoft.com/dotnet/
- **Blazor Documentation**: https://docs.microsoft.com/aspnet/core/blazor/

## 💡 Tips

- Use **Hot Reload** during development (enabled by default in .NET 8)
- Check the console output for any warnings or errors
- Keep your dependencies up to date
- Commit your changes frequently

## 🆘 Getting Help

If you need assistance:

1. Check the documentation in the `docs/` folder
2. Review closed GitHub issues
3. Create a new GitHub issue with details about your problem

---

**Happy Coding! 🎓✨**
