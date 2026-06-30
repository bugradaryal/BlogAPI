# Blog Application Backend API (.NET Core)

A RESTful API built with .NET Core for a blogging platform. It provides user authentication, content management, role-based authorization, and administrative features.

## 🏛️ Architectural & Technical Breakdown

### 1. Layered Architecture & Data Management
* **N-Tier Architecture:** Organized into four layers (API, Business, DataAccess, and Entities) to maintain separation of concerns and improve code maintainability.
* **Database & ORM:** Built with Entity Framework Core using the Code-First approach, including database migrations and data seeding.
* **Data Access:** Used asynchronous LINQ queries (`async/await`), eager loading (`.Include()`), and pagination for efficient data retrieval.
* **Data Validation:** Implemented Data Transfer Objects (DTOs) and Data Annotations to validate requests and separate API models from database entities.

### 2. Identity, Security & Authentication
* **User Management:** Integrated ASP.NET Core Identity for user registration, authentication, and role management.
* **JWT Authentication:** Secured API endpoints using JWT (JSON Web Tokens) for stateless authentication.
* **Refresh Tokens:** Implemented Refresh Token support with database persistence to maintain authenticated sessions.
* **Email Verification:** Integrated MailKit with SMTP to send email verification links during user registration.

### 3. Admin Operations & Statistics
* **Role-Based Authorization:** Protected administrative endpoints using `[Authorize(Roles = "Administrator")]`.
* **User Management:** Added administrative features for role assignment and account suspension (`IsUserSuspended`).
* **Statistics:** Created LINQ aggregation queries to generate dashboard statistics, including post, comment, and user metrics with date-based filtering.
