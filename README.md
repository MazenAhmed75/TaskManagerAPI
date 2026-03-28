# TaskManagerAPI
A Task Management REST API built using ASP.NET Core, designed to handle user-specific tasks with secure authentication and scalable architecture.

The API supports full CRUD operations, including partial updates using PATCH, and allows filtering tasks by status and category. It implements JWT-based authentication to ensure that each user can only access their own data.

Additional features include rate limiting to protect the API from abuse, structured logging for monitoring and debugging, and comprehensive API documentation using Swagger.

The project follows clean architecture principles with separation of concerns (Controllers, Services, DTOs), and includes unit testing using an in-memory database to validate business logic.

Technologies used:
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (configurable)
- JWT Authentication
- Swagger / OpenAPI
- Rate Limiting Middleware
- xUnit (Unit Testing)
