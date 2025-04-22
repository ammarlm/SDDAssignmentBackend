# SDD Assignment Backend

## Overview

The SDD Assignment Backend is a .NET Core 8 Web API that provides user management functionality, including user authentication, CRUD operations (create, update, delete), and an audit trail for tracking changes to the `Users` table. The backend integrates with a Microsoft SQL Server 2019+ database and supports role-based authorization (Admin and User roles). It is designed to work with an Angular 19 frontend, providing a secure and scalable solution for user administration.

Key features:

- User authentication using JWT tokens.
- CRUD operations for user management.
- Audit trail for all `Users` table activities (insert, update, delete).
- Role-based access control (Admin-only access to insert, update, and delete user.


## Prerequisites

- **.NET Core 8 SDK**: Download
- **Microsoft SQL Server 2019+**: Download
- **SQL Server Management Studio (SSMS)**: For running database scripts.
- **Visual Studio 2022** or **VS Code**: For development and debugging.
- **Git**: For cloning the repository.

## Setup Instructions

### 1. Clone the Repository

```bash
git clone 
cd Backend/SDDAssignmentBackend
```

### 2. Configure the Database

1. **Create the Database**:

   - Open SQL Server Management Studio.

   - Create a new database named `SDDAssignmentDB`:

     ```sql
     CREATE DATABASE SDDAssignmentDB;
     ```

2. **Run Database Scripts**:

   - Locate the scripts in the `Scripts/` folder:
   - Execute the scripts in SSMS:

3. **Update Connection String**:

   - Open `appsettings.json` and update the `DefaultConnection` string:

     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=localhost;Database=SDDAssignmentBackend;Trusted_Connection=True;MultipleActiveResultSets=true"
       }
     }
     ```

   - For SQL Server authentication, use:

     ```json
     "DefaultConnection": "Server=localhost;Database=SDDAssignmentBackend;User Id=your_username;Password=your_password;"
     ```

### 3. Install Dependencies

```bash
dotnet restore
```

### 4. Build and Run

```bash
dotnet build
dotnet run
```

- The API will start at `https://localhost:7126` (or the port specified in `launchSettings.json`).
- Access the Swagger UI at `https://localhost:7126/swagger` for API documentation.

### 5. Test the API

- Use tools like **Postman** or **cURL** to test endpoints.

- Example: Authenticate a user:

  ```bash
  curl -X POST https://localhost:7126/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin123"}'
  ```

## API Endpoints

### Authentication

- **POST /api/auth/login**
  - Authenticates a user and returns a JWT token.
  - Request Body: `{"username":"string","password":"string"}`
  - Response: `{"token":"string","user":{"username":"string","role":"string"}}`
  - Requires: Valid username and password.

### Users

- **GET /api/users**
  - Retrieves a paginated list of users.
  - Query Parameters: `page`, `pageSize`, `search`, `sortBy`
  - Response: Paginated list of users.
  - Authorization: Admin role.
- **POST /api/users**
  - Creates a new user.
  - Request Body: `{"username":"string","password":"string","role":"string"}`
  - Response: Created user details.
  - Authorization: Admin role.
- **PUT /api/users/{id**
  - Updates an existing user.
  - Request Body: `{"username":"string","role":"string"}`
  - Response: Updated user details.
  - Authorization: Admin role.
- **DELETE /api/users/{id**
  - Deletes a user.
  - Response: Success status.
  - Authorization: Admin role.


## Audit Trail

The audit trail tracks all `Users` table operations (insert, update, delete). Key details:

- **Storage**: Audit logs are stored in the `AuditLogs` table.
- **Fields**: `TableName`, `Operation`, `RecordId`, `OldData`, `NewData`, `CreatedBy`, `CreatedAt`.
- **ChangedBy**: Set to the authenticated user's username (from JWT token) via stored procedures (`sp_CreateUser`, `sp_UpdateUser`, `sp_DeleteUser`).

## Security

- **JWT Authentication**: All endpoints (except `/api/auth/login`) require a valid JWT token in the `Authorization` header (`Bearer <token>`).
- **Role-Based Authorization**: Admin role required for user management and audit log access.

## Development Notes

- **Entity Framework Core**: Used for database access, with stored procedures for CRUD operations to support audit logging.
- **Dependency Injection**: Services (`IUserService`, `IAuditLogService`) are registered in `DependncyConfig.cs`.
- **Configuration**: JWT settings and connection strings are stored in `appsettings.json`.
- **Logging**: Add logging (e.g., Serilog) for production use by updating `Program.cs`.

## Troubleshooting

- **Database Connection Issues**:

  - Verify the connection string in `appsettings.json`.
  - Ensure SQL Server is running and accessible.

- **Authentication Errors**:

  - Check the JWT token format and `name` claim in the token payload.
  - Ensure the frontend sends the token in the `Authorization` header.

