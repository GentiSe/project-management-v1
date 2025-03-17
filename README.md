1. Clone the Repository
First, you'll need to clone the project from the repository.

Step 1.1: Clone the Project Using Git
Open a terminal or Git Bash.
Run the following command to clone the repository:
bash
Copy
Edit
git clone https://github.com/GentiSe/project-management-v1.git

2. Install Prerequisites
Before running the project, make sure you have the following prerequisites:

2.1: Install .NET SDK
Download and install the appropriate version of .NET SDK from https://dotnet.microsoft.com/en-us/download (.NET 8).

2.2 Database used Microsoft Sql Server.

3. Configure the Database
Step 3.1: Open appsettings.json
Locate the appsettings.json file in the root of the project. It contains the connection string for your database.

Step 3.2: Update Database Connection String
Edit the DefaultConnection in appsettings.json with your database information:

"ConnectionStrings": {
    "DefaultConnection": "Server=ServerName;Database=ProjectManagement;TrustServerCertificate=Yes;Trusted_Connection=True;;MultipleActiveResultSets=true"
}


Step 3.3: Apply Database Migrations
You need to run migrations to create the database schema.

Open Package Manager Console (PMC) in Visual Studio:
Tools > NuGet Package Manager > Package Manager Console.
Run the migration: Update-Database

4. Initialize Data (Seeding)
Tthe data will be inserted automatically during the first run.


5. Added APIs
The following APIs have been added to the project:

1. API for Authentication:
Route: /api/authenticate

Method: POST

Description: This API allows you to authenticate a user using either their email or username and password.

Request Body Model:

{
    "email": "gentselimi7@gmail.com", // User will be added automtically on first run of app with Role 'Admin'.
    "password": "Admin123.",
    "username: null
}
Response:
{
    "token": "bearer token"
}
This will return a JWT Bearer token that needs to be passed to authenticate subsequent requests.

2. API to Get List of Projects:
Route: /api/v1/projects/all

Method: GET

Description: This API returns the list of all available projects.

Headers:

Authorization: Bearer {JWT_TOKEN} (Replace {JWT_TOKEN} with the JWT token obtained from the authentication API)

3. API to Assign a Role to a Project:
Route: /api/v1/projects/assign-role

Method: POST

Description: This API assigns a role to a project.

Request Body Model:

{
    "projectId": "guid",
    "roleId": "guid", // decided to use this approach since in real use cases roles would be selected from a dropdown list.
}

Headers:

Authorization: Bearer {JWT_TOKEN} (Replace {JWT_TOKEN} with the JWT token obtained from the authentication API)
