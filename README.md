## Technologies Used

- **C#** – Programming language  
- **.NET Core 8** – Backend framework  
- **Microsoft SQL Server** – Database  
- **Entity Framework Core** – ORM (Object-Relational Mapping)  
- **JWT (JSON Web Token)** – Used for authorization  
- **ASP.NET Core Identity** – Used for authentication and user management  

# Project Setup Guide

## 1. Clone the Repository
First, you'll need to clone the project from the repository.

### Step 1.1: Clone the Project Using Git
- Open a terminal or Git Bash.
- Run the following command to clone the repository:
  ```sh
  git clone https://github.com/GentiSe/project-management-v1.git
  ```

---

## 2. Install Prerequisites
Before running the project, make sure you have the following prerequisites installed:

### Step 2.1: Install .NET SDK
- Download and install **.NET 8 SDK** from [Microsoft's official website](https://dotnet.microsoft.com/en-us/download).

### Step 2.2: Install Microsoft SQL Server
- Ensure you have **Microsoft SQL Server** installed and running on your machine.

---

## 3. Configure the Database

### Step 3.1: Open `appsettings.json`
Locate the `appsettings.json` file in the root of the project. It contains the connection string for your database.

### Step 3.2: Update Database Connection String
Modify the `DefaultConnection` in `appsettings.json` to match your database configuration:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=servername;Database=ProjectManagement;TrustServerCertificate=Yes;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### Step 3.3: Apply Database Migrations
Run the following command in **Package Manager Console (PMC)** to create the database schema:
```sh
Update-Database
```
> **Note:** Open **PMC** in Visual Studio via `Tools > NuGet Package Manager > Package Manager Console`.

---

## 4. Initialize Data (Seeding)
The initial admin user and roles also proejct with items and role accesses to each project will be inserted automatically on the first run of the application.

---

## 5. Added APIs
The following APIs have been added to the project:

### **1. API for Authentication**
- **Route:** `/api/authenticate`
- **Method:** `POST`
- **Description:** Authenticates a user using email or username and password.
- **Request Body:**
  ```json
  {
      "email": "gentselimi7@gmail.com",  // User will be added automatically on first run with Role 'Admin'.
      "password": "Admin123.",
      "username": null
  }
  ```
- **Response:**
  ```json
  {
      "token": "JWT_BEARER_TOKEN"
  }
  ```
- **Usage:** The returned **JWT Bearer token** must be passed in the `Authorization` header for subsequent API requests.

---

### **2. API to Get List of Projects**
- **Route:** `/api/v1/projects/all`
- **Method:** `GET`
- **Description:** Retrieves a list of all available projects.
- **Headers:**
  ```sh
  Authorization: Bearer {JWT_TOKEN}  # Replace {JWT_TOKEN} with the token obtained from authentication.
  ```
  ```

---

### **3. API to Assign a Role to a Project**
- **Route:** `/api/v1/projects/assign-role`
- **Method:** `POST`
- **Description:** Assigns a role to a project.
- **Request Body:**
  ```json
  {
      "projectId": "project-guid",
      "roleId": "role-guid"  // Decided to use this approach since, in real use cases, roles would be selected from a dropdown list.
  }
  ```
- **Headers:**
  ```sh
  Authorization: Bearer {JWT_TOKEN}  # Replace {JWT_TOKEN} with the token obtained from authentication.
  ```

---
## 6. Sales Aggregator APIs (AllowAnonymous no token needed)
### **1. API to group sales by sales range**
- **Route:** `GET /api/v1/sales/group-by-sales-range`

- **Description:** Groups sales data into predefined ranges (0-10, 10-100, 100+).

---

### **2. Get Top Sold Products by Category (AllowAnonymous no token needed)

  - **Route:** `GET /api/v1/sales/top-sold-by-category`

  - **Description:** Retrieves the most sold products categorized by their category.
  ---
 ### **3. Get Grouped Sales Data by Brand (AllowAnonymous no token needed)

- **Route:** `GET /api/v1/sales/group-by-category`

- **Description:** Groups sales data by category.
```
Note: This README might not cover every details, even though i tried.
---


