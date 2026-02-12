# Student Project Planner

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-512BD4)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

A web-based application built using .NET Blazor that helps college and university students manage assignments, deadlines, and group projects in one centralized location.

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Problem Statement](#problem-statement)
- [Target Users](#target-users)
- [Key Features](#key-features)
- [Technical Stack](#technical-stack)
- [Project Scope](#project-scope)
- [Getting Started](#getting-started)
- [Project Management](#project-management)
- [Team](#team)
- [License](#license)

## 🎯 Project Overview

The **Student Project Planner** is designed to solve the common academic productivity challenges faced by college and university students. Many students struggle to keep track of multiple courses, overlapping deadlines, and collaborative responsibilities. This application simplifies academic organization and improves productivity by providing a centralized platform for managing all academic tasks.

### Why This Project?

- **Solves a real-world problem** - Addresses genuine academic productivity challenges
- **Practical learning** - Demonstrates full-stack Blazor development skills
- **Comprehensive scope** - Covers databases, authentication, CRUD operations, and UI design
- **Incremental development** - Designed for semester-long iterative development

## 🔍 Problem Statement

Students face multiple challenges in managing their academic workload:

- **Scattered information** - Difficulty tracking assignments across multiple courses
- **Missed deadlines** - Poor organization leading to overlooked due dates
- **Limited collaboration** - Lack of visibility into group project responsibilities
- **Stress and overwhelm** - No centralized view of all academic commitments

## 👥 Target Users

- College and university students
- Students working on individual and group assignments
- Students managing multiple courses per semester
- Academic groups needing collaboration tools

## ✨ Key Features

### User Actions

Users will be able to:

1. **Account Management**
   - Create an account and log in securely
   - Manage personal profile information

2. **Course Organization**
   - Create and manage multiple courses
   - Organize assignments by course

3. **Assignment Tracking**
   - Add assignments with due dates
   - View upcoming deadlines in a dashboard
   - Update assignment status (Not Started, In Progress, Completed)

4. **Group Project Collaboration**
   - Create group projects
   - Add tasks to group projects
   - Track progress on collaborative work
   - View team member responsibilities

5. **Dashboard Overview**
   - See upcoming deadlines at a glance
   - Track overall progress across courses
   - Monitor group project status

## 🛠 Technical Stack

### Frontend

- **.NET Blazor** - Interactive web UI framework
- **HTML/CSS** - Responsive design
- **Bootstrap** - UI component library

### Backend

- **.NET 8.0** - Server-side framework
- **Entity Framework Core** - Database ORM
- **ASP.NET Core Identity** - Authentication and authorization

### Database

- **SQL Server** (or SQLite for development)
- Stores: User profiles, courses, assignments, group projects, tasks

### Security

- User authentication and authorization
- Role-based access control
- Protection against unauthorized data access

## 📦 Project Scope

### ✅ What's IN (Planned Features)

- User account creation and login
- Course creation and organization
- Assignment and deadline tracking
- Group project creation and collaboration
- Task status updates (Not Started, In Progress, Completed)
- Basic dashboard overview of upcoming deadlines
- Responsive design for desktop, tablet, and mobile browsers

### ❌ What's OUT (Not Included in This Phase)

- Mobile push notifications
- File uploads for assignments
- Calendar synchronization with external platforms (Google Calendar, Outlook)
- Advanced analytics or AI-based recommendations
- Real-time notifications
- External API integrations

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/sql-server) (or SQLite for local development)
- [Git](https://git-scm.com/)

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/florian-kausche/CSE-325-Team-14.git
   cd CSE-325-Team-14
   ```

2. **Install dependencies**

   ```bash
   dotnet restore
   ```

3. **Update database connection string**
   - Edit `appsettings.json` with your database configuration

4. **Apply database migrations**

   ```bash
   dotnet ef database update
   ```

5. **Run the application**

   ```bash
   dotnet run
   ```

6. **Access the application**
   - Navigate to `https://localhost:5001` in your browser

### Development Setup

For detailed setup instructions and contribution guidelines, see [CONTRIBUTING.md](CONTRIBUTING.md).

## 📊 Project Management

- **GitHub Repository**: [CSE-325-Team-14](https://github.com/florian-kausche/CSE-325-Team-14)
- **Trello Board**: [CSE-325-Team-14](https://trello.com/b/ZF5tgwbE/cse-325-team-14)
- **Project Timeline**: Week 03 - Week 14 (Spring 2026)

## 👨‍💻 Team

**Team 14 - CSE 325**

| Name                | Role                     | GitHub                                                 |
| ------------------- | ------------------------ | ------------------------------------------------------ |
| Florian Adu Kausche | Group Leader & Developer | [@florian-kausche](https://github.com/florian-kausche) |

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📚 Documentation

- [W03 Meeting Notes](docs/W03-Meeting-Notes.md) - Initial planning and brainstorming
- [Project Structure](docs/PROJECT_STRUCTURE.md) - Application architecture overview
- [Contributing Guidelines](CONTRIBUTING.md) - How to contribute to this project

## 🤝 Acknowledgments

- **Course**: CSE 325 - .NET Application Development
- **Institution**: Brigham Young University - Idaho
- **Semester**: Spring 2026
- **Project Duration**: Week 03 - Week 14

---

**Built with ❤️ using .NET Blazor**
