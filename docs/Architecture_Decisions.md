# Architecture Decisions - EduCenterSys

This document notes down the key architectural choices made for the **EduCenterSys** project and the reasoning behind them.

---

## ADR 0001: Splitting the Solution into 4 Layers (Clean Architecture)

### Status
Accepted

### Why we did this
To keep the codebase organized, prevent mixing database logic with business rules or API controllers, and ensure each project has a clear responsibility.

### The Decision
We split the solution into 4 distinct projects:

1. **`EduCenterSys.Domain`**
   * **Content:** Entities, Enums, and Core Interfaces (`IGenericRepository`, `IUnitOfWork`).
   * **Dependencies:** None. This project is pure C# and depends on zero external projects or frameworks.

2. **`EduCenterSys.Infrastructure`**
   * **Content:** Database setup (`EduCenterDbContext`), EF Core Entity Configurations, Migrations, and Repository implementations.
   * **Dependencies:** Depends **ONLY** on `EduCenterSys.Domain` to implement its repository interfaces and access entities.

3. **`EduCenterSys.Application`**
   * **Content:** DTOs, Mapping logic, Application Services, and Service Interfaces (like `IStudentService`).
   * **Dependencies:** Depends on `EduCenterSys.Domain`. It has **no reference** to `EduCenterSys.Infrastructure`.

4. **`EduCenterSys.Api`**
   * **Content:** Controllers, Exception Handlers, and `Program.cs`.
   * **Dependencies:** References all projects to expose endpoints and register services in the Dependency Injection container.

### How Layer Dependencies Work
* `Domain` is the core and knows nothing about the other layers.
* `Infrastructure` handles EF Core data persistence by referencing `Domain`.
* `Application` manages business logic and DTOs using `Domain` definitions without seeing EF Core details.
* `Api` ties everything together at startup.

### Trade-offs
* **Good:** Clean separation of concerns, isolated EF Core logic, and structured code that is easy to navigate.
* **Extra Work:** Requires mapping between Entities and DTOs and maintaining multiple projects.

---

## ADR 0002: Designing Rich Domain Models and Core Interfaces

### Status
Accepted

### Why we did this
To protect entity data from invalid changes across the application and ensure that business rules belonging to a specific entity stay inside that entity, rather than leaking into services or controllers.

### The Decision
We decided to design the models inside `EduCenterSys.Domain` as **Rich Domain Models** instead of simple data containers (Anemic Models):

1. **Encapsulation & Behavior:**
   * Entities manage their own data, state, and behaviors.
   * Modifying an entity's state is done through dedicated methods inside the entity class, keeping the state transitions controlled internally while being triggered from the outside.

2. **Domain Enums:**
   * Used Enums for recurring states and types across entities to guarantee type safety and restrict inputs to valid values.

3. **Core Database Interfaces (`IGenericRepository` & `IUnitOfWork`):**
   * Placed data access interfaces inside `EduCenterSys.Domain`.
   * Since data contracts are a core part of the system design, placing them in `Domain` allows `Application` to use them without needing to know anything about EF Core or `Infrastructure`.

### Trade-offs
* **Good:** Strong data protection, cleaner code where entity validation stays inside the entity itself, and clear abstractions.
* **Extra Work:** Configures EF Core in `Infrastructure` slightly more explicitly (e.g., handling private setters/backing fields) compared to simple public getter/setter properties.

---

## ADR 0003: Infrastructure Setup (Repository Pattern, EF Core & DI Scope)

### Status
Accepted

### Why we did this
To isolate database operations and external provider details from the rest of the application, making data persistence maintainable and easily swappable without affecting business logic.

### The Decision
Inside `EduCenterSys.Infrastructure`, we implemented the following technical decisions:

1. **Repository Pattern & Dependency Inversion:**
   * Separated data access logic using the Repository Pattern, adhering to the Dependency Inversion Principle (DIP) via Dependency Injection (DI).

2. **Fluent API Configurations & Migrations:**
   * Used EF Core Code-First approach with Migrations stored in this layer.
   * Applied `IEntityTypeConfiguration` using Fluent API to explicitly define schema rules, database constraints, and data consistency.

3. **Generic Repository & Unit of Work:**
   * Implemented a `GenericRepository<T>` to eliminate redundant CRUD code across entities.
   * Repository methods handle memory-level tracking operations (e.g., Add, Delete) without immediately calling `SaveChanges()`.
   * Integrated `UnitOfWork` to manage transactions, allowing multiple operation calls before persisting changes all at once with a single `SaveAsync()`.

4. **Self-Contained Dependency Injection Registration:**
   * Created a dedicated extension method (e.g., `AddInfrastructureServices`) inside `Infrastructure`.
   * This keeps service registrations localized within the layer itself, so `EduCenterSys.Api` only needs to invoke one setup method without exposing internal implementation details.

### Trade-offs
* **Good:** Loose coupling, single database transactions via Unit of Work, clean `Program.cs`, and consistent EF Core configurations.
* **Extra Work:** Adding an abstraction layer over EF Core requires maintaining repository classes and Unit of Work interfaces.
---

## ADR 0004: Application Layer Design (Services, DTOs & Validation)

### Status
Accepted

### Why we did this
To orchestrate application use cases, validate incoming client input before it hits business operations, and shape returned data without exposing database entities directly to the API layer.

### The Decision
Inside `EduCenterSys.Application`, we established the following design patterns:

1. **Use Case Orchestration & Business Rules:**
   * Acts as the coordinator between the API and Domain/Infrastructure layers.
   * Handles multi-step business operations and verifies rules that require database queries (e.g., checking for duplicate records before creation).

2. **DTOs as Immutable Records & Early Validation:**
   * Grouped related DTOs inside container classes as C# `record` types to ensure immutability and value-based comparison across layer boundaries.
   * Applied Data Annotation attributes on DTOs to enforce property-level rules (text lengths, phone patterns). This triggers early validation via Model Binding before hitting controller actions.

3. **Object Mapping with AutoMapper:**
   * Integrated AutoMapper to handle conversions between Domain Entities and DTOs, reducing manual mapping boilerplate.

4. **Reusable Service Architecture (`BaseService`):**
   * Built a generic abstract `BaseService` that implements standard CRUD workflows.
   * Marked base service methods as `virtual` so specific services (e.g., `StudentService`) can override a method to inject custom validation logic before calling `base.Method()`.
   * Exposed services to `EduCenterSys.Api` using interface abstractions (e.g., `IStudentService`).

5. **Self-Contained DI Registration:**
   * Added a dedicated extension method (e.g., `AddApplicationServices`) inside `Application` so `Program.cs` registers all services and AutoMapper profiles with a single line.

### Trade-offs
* **Good:** Reduced code duplication via `BaseService`, protected entities from API exposure, immutable DTOs, and clear layer boundaries.
* **Extra Work:** Requires creating DTOs and AutoMapper profiles for every feature instead of passing entities directly.
---

## ADR 0005: API Layer Design and Global Exception Handling (`IExceptionHandler`)

### Status
Accepted

### Why we did this
To keep controllers clean by avoiding repeated `try-catch` blocks, ensure all API responses return a consistent error structure when something goes wrong, and leverage modern .NET techniques for global error handling.

### The Decision
Inside `EduCenterSys.Api`, we structured the entry point and error handling as follows:

1. **Thin Controllers:**
   * Controllers act strictly as HTTP entry points, receiving client requests, passing data to Application Services (e.g., `IStudentService`), and returning HTTP status codes.
   * Business validation and database operations are strictly delegated to lower layers.

2. **Global Exception Handling via `IExceptionHandler`:**
   * Instead of traditional custom middleware or writing `try-catch` inside every controller action, we adopted the built-in `IExceptionHandler` feature introduced in .NET 8.
   * Created a central exception handler that intercepts unhandled exceptions globally across the application.
   * Formats errors using the standard `ProblemDetails` format (RFC 7807), mapping specific exception types (like Validation or Entity Not Found exceptions) to appropriate HTTP status codes (400, 404, 500).

3. **Centralized Dependency Registration:**
   * `Program.cs` stays clean and readable by invoking layer-specific DI extensions (`AddInfrastructureServices()`, `AddApplicationServices()`) and registering the global exception handling services.

### Trade-offs
* **Good:** Zero `try-catch` boilerplate in controllers, consistent JSON error format for clients, better performance and integration compared to custom middleware.
* **Extra Work:** Requires mapping custom domain/application exceptions properly within the handler.