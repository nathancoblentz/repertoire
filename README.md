# CoblentzContext - Repertoire & Catalog Management System

## Project Overview
CoblentzContext is a premium ASP.NET Core MVC application designed for musicians to manage their song repertoire, organize work into project-specific catalogs, and maintain a personalized collection of favorites. Each song can link to YouTube reference videos and lead sheet charts for quick stage-side access.

---

## 🎓 Final Assignment Requirements Matrix

| Requirement | Implementation Detail |
| :--- | :--- |
| **Admin Section with Login** | Dedicated Admin Area protected by Identity `[Authorize]`. Admin account seeded on startup in `Program.cs` |
| **Area: About (2 Views)** | About Area with General Guide (Guide + Tutorial) and Documentation (Technical Specs) |
| **Action: Multiple Views** | `About/HomeController.Index(string id)` returns General or Documentation based on route ID |
| **Area-Specific Layouts** | Admin and About areas each have their own `_ViewStart.cshtml` pointing to unique layout wrappers |
| **Smart Search & Filters** | Real-time client-side search filtering on both Public Home and Admin Manage pages |
| **Dynamic Table Sorting** | Multidirectional column sorting logic implemented for all song repertoire tables |
| **Dashboard Metrics** | Dynamic stats dashboard on the Home page tracking total songs, readiness, and favorites |
| **Sessions** | Session tracks visit start time, displayed on the Home page (`HomeController.cs` line ~30) |
| **Cookies** | `SongCookies.cs` manages Theme preference and Last Interacted Song via browser cookies |
| **Trailing Slash** | `AppendTrailingSlash = true` in `Program.cs` routing options |
| **Lowercase Addresses** | `LowercaseUrls = true` in `Program.cs` routing options (not hardcoded) |
| **Database Setup** | NuGet: `Microsoft.EntityFrameworkCore.SqlServer`. Context: `Models/CoblentzContext.cs` |
| **Models (3 Classes)** | `Song.cs`, `Project.cs`, `User.cs` with navigation properties and data annotations |
| **CRUD lifecycle** | Complete Add (with Status dropdown), Edit, and Delete workflow with confirmations |

---

## 🕹️ Video Script Highlights

### 1. Repertoire Dashboard (Search & Sort)
- **Stats**: Show the dynamic dashboard metrics. Explain they update as the repertoire grows.
- **Search**: Type an artist's name to show the instant real-time filtering logic.
- **Sort**: Click the "Title" or "Status" headers to demo the client-side sorting algorithms.

### 2. State Management (Sessions & Cookies)
- **Session**: Show the "Session Started" badge. Code is in `HomeController.cs`.
- **Cookies**: Toggle **Dark Mode** — saved to `CoblentzTheme` cookie for persistence.
- **UI Feel**: Show the **Heart Pulse** animation when toggling favorites.

### 3. The Catalog System
- Navigate to **Catalogs** → select a project → show the stage-ready catalog view.
- Click **Print Catalog** to demonstrate the specialized CSS print mode for paper copies.
- Show the **Resources** column with YouTube and Chart icons for quick access.

### 4. Admin Dashboard (Advanced CRUD)
- **Status Dropdown**: Add a song via Admin and show the standardized status selection (Ready, Learning, Requested, New).
- **Badge Colors**: Show how the status colors (Green/Yellow/Cyan/Gray) match application-wide.
- **Delete Confirmation**: Perform a delete and show the safety confirmation screen.

---

## 🛠️ Technical Specifications
- **Framework**: ASP.NET Core 8.0 MVC (.NET 9 Runtime)
- **Database**: MS SQL Server LocalDB with EF Core Migrations
- **Security**: ASP.NET Identity Core with role-based access (Admin/Member)
- **UX/UI**: Bootstrap 5.3 + FontAwesome 6 + JQuery + Pulse.css Logic
- **Performance**: Async controllers, `AsNoTracking()`, Gzip response compression

---

## 📁 Project Structure
```
CoblentzContext/
├── Areas/
│   ├── Admin/          # Protected repertoire management (CRUD + Search/Sort)
│   └── About/          # General Guide and Technical Documentation
├── Controllers/        # Public HomeController, ProjectController, AccountController
├── Models/             # Song, Project, User, Favorite, SongCookies, CoblentzContext
├── wwwroot/css/        # site.css (dark mode, animations, print/table styles)
├── Migrations/         # EF Core database version history
└── Views/              # Public views (Home, Project catalogs, Account)
```

---

*This project is submitted as the final assignment. No in-class or textbook examples were used; all logic and design are original.*
