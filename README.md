---
````markdown
# Task Management System (ASP.NET Core + SQLite)

This project is a simple **Task Management API** built with **ASP.NET Core**, **Entity Framework Core**, and **SQLite**.  
It allows you to **create, read, update, and delete (CRUD)** tasks.

---

## 📌 Features
- Create a task with:
  - Task Name
  - Created At
  - Estimation (in hours)
  - Status (Pending / In Progress / Done)
- Store tasks in **SQLite** database
- RESTful API endpoints
- Built with **Entity Framework Core Migrations**

---

## 🛠️ Tech Stack
- ASP.NET Core 8
- Entity Framework Core
- SQLite
- C#

---

## ⚙️ Setup Instructions

### 1. Clone the repo
```bash
git clone https://github.com/aro26/TaskManagementSystem.git
cd TaskManagementSystem
````

### 2. Update Database

Make sure EF Core tools are installed:

```bash
dotnet tool install --global dotnet-ef
```

Run migrations:

```bash
dotnet ef database update
```

This creates the **Tasks** table in `Task-Management-Db.db`.

---

## 🚀 Run the project

```bash
dotnet run
```

The API will start on:

```
https://localhost:5001
http://localhost:5000
```

---

## 📡 API Endpoints

### ✅ Create Task

```http
POST /api/v1/CRUDTask/CreateTask
```

Request Body:

```json
{
  id: 1,
  "taskName": "Learn EF Core",
  "createdAt": "2025-08-16T12:00:00",
  "estimation": 3,
  "status": "Pending"
}
```

---


### 📋 Get All Tasks

```http
GET /api/v1/CRUDTask/GetAllTask
```

---

### 🔍 Get Task By Id

```http
GET /api/v1/CRUDTask/{id}
```

---

### ✏️ Update Task

```http
PUT /api/v1/CRUDTask/{id}
```

---

### ✏️ Patch Task

```http
PUT /api/v1/CRUDTask/{id}
```

---

### 🔍 Get a Task by ID

```http
GET /api/v1/CRUDTask/{id}
```

---

### ❌ Delete Task

```http
DELETE /api/v1/CRUDTasks/{id}
```

---

## 📂 Project Structure

```
TaskManagementSystem/
│-- Controllers/
│   └── TaskController.cs
│-- Entities/
│   └── TaskItem.cs
│-- SqlLite/
│   └── AppDbContext.cs
│-- Migrations/
│-- appsettings.json
│-- Program.cs
```

---

## 📝 License

MIT License

```
