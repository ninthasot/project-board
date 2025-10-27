# Copilot Instructions for Project Board

## Project Overview

This is a modern project board application (similar to Trello/Kanban) built to demonstrate and implement CQRS (Command Query Responsibility Segregation), message bus patterns, and modern web development practices. The project consists of a .NET API backend and a SvelteKit frontend.

## Architecture

### Backend (.NET API)
- **Location**: `app/ProjectBoardApiDotNet/`
- **Framework**: .NET 9 with ASP.NET Core
- **Architecture**: Clean Architecture with modular monolith design
- **Database**: PostgreSQL with Entity Framework Core
- **Patterns**: CQRS with MediatR, Domain-Driven Design (DDD)

### Frontend (SvelteKit)
- **Location**: `app/project-board-front-svelte/`
- **Framework**: SvelteKit with TypeScript
- **Styling**: TailwindCSS
- **Testing**: Vitest with Playwright for browser testing
- **Build**: Vite

## Key Features

### Core Domain Features
1. **Board Management**
   - Create and manage project boards
   - Board descriptions and titles
   - Board member management

2. **Card System**
   - Create cards with titles and descriptions
   - Position cards within columns
   - Add attachments to cards
   - Comment on cards
   - Create checklists with items

3. **Organization**
   - Columns for organizing cards
   - Labels for categorization
   - User management and permissions

### Technical Features
1. **CQRS Implementation**
   - Commands for write operations
   - Queries for read operations
   - Separate handlers for each operation
   - MediatR as message bus

2. **Observability**
   - OpenTelemetry integration
   - Prometheus metrics
   - Grafana dashboards
   - Zipkin distributed tracing

3. **Containerization**
   - Docker support with multi-service composition
   - PostgreSQL database container
   - Monitoring stack containers

## Project Structure

### Backend Structure
```
app/ProjectBoardApiDotNet/
├── src/
│   ├── Api/                    # API layer (controllers, startup)
│   ├── Common/                 # Shared components
│   │   ├── Common.Application/ # Application layer abstractions
│   │   ├── Common.Domain/      # Domain layer abstractions
│   │   ├── Common.Infrastructure/ # Infrastructure abstractions
│   │   └── Common.Presentation/   # Presentation layer base
│   ├── Infrastructure/         # Infrastructure implementation
│   ├── Identity/              # Authentication/authorization
│   └── Modules/               # Domain modules
│       ├── Boards/            # Board management module
│       ├── Cards/             # Card management module
│       ├── Labels/            # Label management module
│       └── Users/             # User management module
```

Each module follows Clean Architecture:
- `Domain/`: Entities, value objects, domain services
- `Application/`: Commands, queries, handlers, DTOs
- `Infrastructure/`: Data access, external services
- `Presentation/`: Controllers, API endpoints

### Frontend Structure
```
app/project-board-front-svelte/
├── src/
│   ├── lib/                   # Reusable components and utilities
│   ├── routes/                # SvelteKit routes
│   └── app.html               # Main HTML template
```

## Development Best Practices

### Backend Best Practices

1. **Follow Clean Architecture**
   - Keep domain logic in the Domain layer
   - Use Application layer for orchestration
   - Implement Infrastructure layer for external concerns
   - Keep Presentation layer thin

2. **CQRS Patterns**
   - Create separate Command and Query objects
   - Implement dedicated handlers for each operation
   - Use MediatR for message dispatching
   - Keep commands focused on single operations

3. **Domain Modeling**
   - Use rich domain entities with behavior
   - Implement proper encapsulation
   - Use value objects for complex types
   - Follow Domain-Driven Design principles

4. **Error Handling**
   - Use proper exception handling patterns
   - Implement custom exception handlers
   - Return meaningful error responses
   - Log errors appropriately

### Frontend Best Practices

1. **Component Organization**
   - Keep components focused and reusable
   - Use TypeScript for type safety
   - Follow SvelteKit conventions
   - Implement proper component composition

2. **Testing**
   - Write unit tests for business logic
   - Use Vitest for testing framework
   - Implement browser testing with Playwright
   - Test user interactions and component behavior

3. **Code Quality**
   - Use ESLint for code linting
   - Format code with Prettier
   - Follow TypeScript best practices
   - Implement proper error boundaries

## Commands and Scripts

### Backend Commands
```bash
cd app/ProjectBoardApiDotNet
dotnet restore                    # Restore dependencies
dotnet build                      # Build solution
dotnet run --project src/Api      # Run API
docker-compose up                 # Run with all services
```

### Frontend Commands
```bash
cd app/project-board-front-svelte
npm install                       # Install dependencies
npm run dev                       # Development server
npm run build                     # Production build
npm run test                      # Run tests
npm run lint                      # Lint code
npm run format                    # Format code
```

## API Design Patterns

### Controllers
- Inherit from `BaseController`
- Use MediatR `ISender` for command/query dispatch
- Return appropriate HTTP status codes
- Use DTOs for request/response models

### Commands and Queries
- Commands should be focused on single operations
- Queries should return DTOs, not domain entities
- Use proper validation attributes
- Implement idempotent operations where appropriate

### Example Command Pattern
```csharp
// Command
public record CreateBoardCommand(string Title, string Description) : ICommand<Guid>;

// Handler
public class CreateBoardCommandHandler : ICommandHandler<CreateBoardCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBoardCommand command, CancellationToken cancellationToken)
    {
        // Implementation
    }
}

// Controller
[HttpPost]
public async Task<ActionResult<Guid>> CreateBoard([FromBody] CreateBoardDto dto)
{
    var command = new CreateBoardCommand(dto.Title, dto.Description);
    var result = await Sender.Send(command);
    return ToCreatedAtActionResult(result, nameof(GetBoardById));
}
```

## Database Design

### Entity Patterns
- Inherit from `BaseAuditableEntity<TId>` for audit fields
- Use proper navigation properties
- Implement proper foreign key relationships
- Use Entity Framework conventions and configurations

### Migration Strategy
- Use Entity Framework migrations
- Keep migrations focused and atomic
- Test migrations thoroughly
- Document breaking changes

## Monitoring and Observability

### Telemetry
- OpenTelemetry integration for distributed tracing
- Custom metrics for business operations
- Structured logging with Serilog
- Health check endpoints

### Monitoring Stack
- Prometheus for metrics collection
- Grafana for visualization
- Zipkin for distributed tracing
- Custom dashboards for application insights

## Security Considerations

### API Security
- Implement proper authentication/authorization
- Use HTTPS in production
- Validate all inputs
- Implement rate limiting
- Secure error handling (don't expose internal details)

### Frontend Security
- Sanitize user inputs
- Implement proper CORS handling
- Use secure cookie settings
- Validate all API responses

## Common Patterns to Follow

1. **Result Pattern**: Use `Result<T>` for operation outcomes instead of exceptions for business logic errors
2. **Repository Pattern**: Implement repositories for data access abstraction
3. **Unit of Work**: Use Entity Framework's DbContext as unit of work
4. **Dependency Injection**: Use built-in DI container for all dependencies
5. **Configuration**: Use strongly-typed configuration objects
6. **Validation**: Implement input validation at API boundaries

## File Naming Conventions

### Backend
- Commands: `{Operation}Command.cs`
- Queries: `{Operation}Query.cs`
- Handlers: `{Operation}CommandHandler.cs` or `{Operation}QueryHandler.cs`
- Entities: PascalCase with descriptive names
- DTOs: `{Entity}Dto.cs`

### Frontend
- Components: PascalCase for Svelte components
- Routes: Follow SvelteKit file-based routing
- Utilities: camelCase for utility functions
- Types: PascalCase for TypeScript interfaces/types

## Testing Guidelines

### Backend Testing
- Write unit tests for domain logic
- Test command/query handlers
- Mock external dependencies
- Use test databases for integration tests

### Frontend Testing
- Test component behavior
- Mock API calls
- Test user interactions
- Implement accessibility testing

## Performance Considerations

### Backend
- Use async/await for I/O operations
- Implement proper database indexing
- Use pagination for large datasets
- Implement caching where appropriate

### Frontend
- Optimize bundle size
- Use lazy loading for routes
- Implement proper error boundaries
- Optimize images and assets

## Development Workflow

1. **Feature Development**
   - Start with domain modeling
   - Implement commands/queries
   - Add API endpoints
   - Update frontend components
   - Write tests
   - Update documentation

2. **Code Review Guidelines**
   - Review for architectural consistency
   - Check test coverage
   - Verify error handling
   - Ensure security best practices
   - Validate performance implications

This project serves as a reference implementation for modern web application architecture using CQRS, Clean Architecture, and contemporary web development practices.