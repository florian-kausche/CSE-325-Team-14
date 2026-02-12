# W03 Group Project: Setup, Planning, and Brainstorming

**Date**: Week 03, Spring 2026  
**Team**: Team 14 - CSE 325  
**Meeting Leader**: Florian Adu Kausche  
**Next Meeting Leader**: Florian Adu Kausche

---

## 📋 Meeting Overview

### Purpose

To establish the foundation for our semester-long .NET Blazor application project by:

- Sharing insights from individual learning activities
- Reviewing Week 07 project requirements
- Brainstorming potential application ideas
- Setting up collaboration tools

### Meeting Opening

The meeting was opened with a prayer, inviting the Spirit to guide the discussion and decision-making process.

---

## 👥 Participants

| Name                | Role                     |
| ------------------- | ------------------------ |
| Florian Adu Kausche | Group Leader & Developer |

---

## 🔍 Learning Activity Reflections

### Challenges Faced

During the .NET Blazor learning activities, the following challenges were encountered:

1. **Debugging Complexity**
   - Spent considerable time debugging code issues
   - Worked through compilation and runtime errors
   - Learned debugging techniques specific to Blazor applications

2. **Database Integration**
   - Faced challenges with database setup and configuration
   - Worked through Entity Framework Core integration
   - Learned about database migrations and data seeding

### Key Discoveries

- Understanding of Blazor component lifecycle
- Experience with data binding and event handling
- Practical knowledge of database operations with EF Core

---

## 📖 Week 07 Project Requirements Review

### Project Scope Understanding

- Reviewed complete project description
- Understood peer-review expectations
- Clarified individual responsibilities
- Established timeline from Week 03 to Week 14

### Deliverables Identified

- Working .NET Blazor application
- Database integration
- User authentication system
- CRUD operations implementation
- Peer reviews at designated checkpoints

---

## 💡 Brainstorming Session

### Application Ideas Considered

During the brainstorming session, multiple project ideas were evaluated based on:

- Feasibility within the semester timeframe
- Technical learning opportunities
- Real-world applicability
- Target user needs

### Selected Project: Student Project Planner

**Rationale for Selection:**

- Addresses a real problem faced by students
- Provides comprehensive learning opportunities
- Manageable scope for semester-long development
- Allows for incremental feature implementation

---

## 🎯 Project Selection Details

### Application Name

**Student Project Planner**

### Purpose

Help students track assignments, deadlines, and group projects in one centralized location.

### Key Features

1. **Assignment & Deadline Tracking**
   - Create and manage assignments
   - Set due dates and reminders
   - Track completion status

2. **Course-Based Organization**
   - Organize assignments by course
   - View course-specific workload
   - Manage multiple courses simultaneously

3. **Group Collaboration Features**
   - Create group projects
   - Assign tasks to team members
   - Track collaborative progress

4. **Deadline Reminders**
   - Dashboard view of upcoming deadlines
   - Priority-based task organization
   - Status tracking (Not Started, In Progress, Completed)

### Target Audience

- College and university students
- Students managing multiple courses and projects
- Academic groups requiring collaboration tools

---

## 📋 Project Proposal

### Project Overview

The Student Project Planner is a web-based application built using .NET Blazor that helps college and university students manage assignments, deadlines, and group projects in one centralized location. Many students struggle to keep track of multiple courses, overlapping deadlines, and collaborative responsibilities. This application aims to simplify academic organization and improve productivity.

### Problem Statement

The application addresses several key challenges:

1. **Difficulty tracking assignments** across multiple courses
2. **Missed deadlines** due to poor organization
3. **Limited visibility** into group project responsibilities
4. **Academic stress** from lack of centralized task management

### Solution

By providing a clear overview of tasks and responsibilities, students can:

- Reduce stress and anxiety
- Avoid missed deadlines
- Stay focused on academic goals
- Collaborate more effectively with peers

### Value Proposition

This project is valuable because it:

- **Reflects a real-world problem** faced by students daily
- **Provides hands-on experience** with Blazor, databases, and authentication
- **Requires practical application** of CRUD operations
- **Allows incremental development** throughout the semester
- **Remains realistic in scope** while being feature-rich

---

## 🎯 Project Scope

### ✅ What's IN Scope (Planned Features)

1. **User Management**
   - Account creation and login
   - Secure authentication
   - User profile management

2. **Course Management**
   - Create and organize courses
   - Course-based assignment organization
   - Multiple course support

3. **Assignment Tracking**
   - Add assignments with due dates
   - Update assignment status
   - View upcoming deadlines

4. **Group Projects**
   - Create group projects
   - Add tasks to projects
   - Track task progress
   - Collaborative features

5. **Dashboard**
   - Overview of upcoming deadlines
   - Progress tracking across courses
   - Task status visualization

### ❌ What's OUT of Scope (Not Included)

The following features are explicitly excluded from this phase:

1. **Mobile push notifications** - Not implemented
2. **File uploads** for assignments - Future enhancement
3. **Calendar synchronization** with external platforms (Google Calendar, Outlook) - Not included
4. **Advanced analytics** or AI-based recommendations - Beyond current scope
5. **Real-time collaboration** features - Not in initial version
6. **External API integrations** - Not planned for this phase

---

## ✨ App Features (User Actions)

### Detailed User Capabilities

Users will be able to perform the following actions:

1. **Account Operations**
   - Create an account with email and password
   - Log in securely to access personal data
   - Log out to protect account security

2. **Course Management**
   - Create new courses with names and codes
   - Edit existing course information
   - Delete courses when no longer needed
   - View list of all courses

3. **Assignment Management**
   - Add new assignments with:
     - Assignment name
     - Due date and time
     - Course association
     - Priority level
   - Edit assignment details
   - Mark assignments as complete
   - Delete assignments
   - View all assignments by course or deadline

4. **Dashboard Usage**
   - View upcoming deadlines at a glance
   - See overdue assignments highlighted
   - Track overall progress across all courses
   - Filter assignments by status

5. **Group Project Collaboration**
   - Create new group projects
   - Add team members to projects
   - Create tasks within projects
   - Assign tasks to team members
   - Update task status (Not Started, In Progress, Completed)
   - View project progress overview

---

## 🛠 Technical Considerations

### Data Storage

The application will store the following data:

1. **User Profiles**
   - Username
   - Email address
   - Encrypted password
   - Account creation date

2. **Courses**
   - Course name
   - Course code
   - Semester/term
   - User association

3. **Assignments**
   - Assignment name
   - Due date
   - Course association
   - Status (Not Started, In Progress, Completed)
   - Priority level
   - User association

4. **Group Projects**
   - Project name
   - Project description
   - Team members
   - Creation date

5. **Tasks**
   - Task name
   - Task description
   - Project association
   - Assigned user
   - Status
   - Due date

### User Accounts

**Authentication Requirements:**

- Users **must** create an account to use the application
- Users **must** log in to access personal data
- Authentication required for all data operations
- Session management for security

**Security Measures:**

- Password encryption
- Secure authentication tokens
- Role-based access control
- Protection against SQL injection
- CSRF protection

### External Services

**Current Phase:**

- No external APIs planned for initial implementation
- Focus on core functionality first

**Future Considerations:**

- Email notifications (possible future enhancement)
- Calendar integration (post-MVP)

### Device Compatibility

**Responsive Design Support:**

1. **Desktop**
   - Full-featured experience
   - Optimized for large screens
   - Multi-column layouts

2. **Tablet**
   - Touch-optimized interface
   - Responsive grid layouts
   - Portrait and landscape support

3. **Mobile Browsers**
   - Mobile-first design approach
   - Touch-friendly controls
   - Simplified navigation
   - Vertical scrolling optimization

### Basic Security Measures

1. **User Authentication**
   - ASP.NET Core Identity implementation
   - Secure password storage with hashing
   - Session management

2. **Authorization**
   - Role-based access control
   - User-specific data access
   - Prevention of unauthorized operations

3. **Data Protection**
   - Protection against unauthorized access
   - Secure data transmission (HTTPS)
   - Input validation and sanitization
   - Protection against common vulnerabilities (XSS, CSRF)

4. **Personal & Group Data Security**
   - Users can only access their own assignments
   - Group members can only access shared group data
   - Admin roles for project management (if needed)

---

## 🚀 Initial Setup Tasks

### Completed Setup Activities

1. ✅ **GitHub Repository Created**
   - Repository URL: https://github.com/florian-kausche/CSE-325-Team-14
   - Initialized with README
   - Public repository for course access

2. ✅ **Trello Board Created**
   - Board URL: https://trello.com/b/ZF5tgwbE/cse-325-team-14
   - Lists created for project management:
     - Backlog
     - To Do
     - In Progress
     - Testing
     - Done
   - Initial tasks added

3. ✅ **Local Repository Clone**
   - Repository cloned to local development machine
   - Git configuration completed
   - Ready for development work

### Screenshots

#### Local Clone Verification

_Screenshot showing local repository clone on development machine_

#### Trello Board Setup

_Screenshot of Trello board with initial project organization_

---

## 👔 Group Leadership

### Current Meeting Leader

**Florian Adu Kausche**

### Leadership Transition

As the sole group member, leadership responsibilities remain with Florian Adu Kausche for continuity and consistency throughout the project.

### Next Meeting Leader

**Florian Adu Kausche**

---

## 📅 Next Steps

### Immediate Actions (Week 04)

1. **Technical Setup**
   - [ ] Create initial .NET Blazor project structure
   - [ ] Set up database configuration
   - [ ] Implement basic authentication scaffolding

2. **Documentation**
   - [ ] Create detailed technical specification
   - [ ] Design database schema
   - [ ] Plan UI/UX wireframes

3. **Project Management**
   - [ ] Update Trello board with detailed tasks
   - [ ] Create development timeline
   - [ ] Set up version control workflow

### Short-Term Goals (Weeks 05-07)

1. Implement user authentication system
2. Create course management functionality
3. Build assignment tracking features
4. Design and implement dashboard
5. Prepare for Week 07 peer review

### Long-Term Goals (Weeks 08-14)

1. Implement group project collaboration features
2. Add task management capabilities
3. Enhance UI/UX based on feedback
4. Perform thorough testing
5. Complete final documentation
6. Prepare final presentation

---

## 📝 Meeting Notes

### Key Decisions Made

1. **Project Selection**: Student Project Planner chosen as the semester project
2. **Technology Stack**: .NET Blazor with Entity Framework Core
3. **Development Approach**: Incremental feature implementation
4. **Collaboration Tools**: GitHub + Trello for project management

### Lessons Learned

1. Importance of clear project scope definition
2. Value of brainstorming before committing to an idea
3. Need for realistic feature planning
4. Benefit of incremental development approach

### Action Items

| Action Item                            | Owner   | Due Date |
| -------------------------------------- | ------- | -------- |
| Update README with project details     | Florian | Week 03  |
| Create project structure documentation | Florian | Week 03  |
| Initialize .NET Blazor project         | Florian | Week 04  |
| Design database schema                 | Florian | Week 04  |
| Set up authentication                  | Florian | Week 05  |

---

## 🙏 Meeting Closing

The meeting concluded with confidence in the project direction and excitement for the development work ahead. The Student Project Planner addresses a real need and provides excellent learning opportunities throughout the semester.

---

**Document Version**: 1.0  
**Last Updated**: Week 03, Spring 2026  
**Next Review**: Week 04 Meeting
