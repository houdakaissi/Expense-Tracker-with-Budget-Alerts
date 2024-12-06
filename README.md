# Expense Tracker Backend

## Overview

This is the backend for the **Expense Tracker** web application, designed to help users track their expenses and receive notifications when they exceed a predefined monthly budget. It is built with **.NET 8** and serves as an API for handling expense-related operations.

## Features

- **Add Expense**: Allows users to add expenses with details such as amount, category, and description.
- **View Expenses**: Users can view a list of their expenses.
- **Delete Expense**: Users can delete previously added expenses.
- **Categorize Expenses**: Expenses can be categorized (e.g., Food, Transport, Entertainment).
- **Set Monthly Budget**: Users can set a monthly budget and track their spending.
- **Budget Alert**: A notification will be triggered if the user's total expenses exceed their set monthly budget.

## Technologies

- **.NET 8**: Backend framework.
- **Entity Framework Core**: ORM for database interaction.
- **SQL Server (or any database)**: Used for storing expenses and user data.
- **Swagger**: API documentation.
- **JWT Authentication**: Used for securing API endpoints.

## Setup and Installation

### Prerequisites

- Install **.NET 8 SDK**: [Download .NET 8](https://dotnet.microsoft.com/download/dotnet).
- Install a database (e.g., **SQL Server**, **MySQL**).

### Installation Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/expense-tracker-backend.git
Navigate to the project folder:

bash
Copy code
cd expense-tracker-backend
Install the required dependencies:

bash
Copy code
dotnet restore
Update the appsettings.json with your database connection string.

Create and apply migrations to set up the database schema:

bash
Copy code
dotnet ef migrations add InitialCreate
dotnet ef database update
Run the application:

bash
Copy code
dotnet run
The API will be running on http://localhost:5000.

Database Setup
Expenses Table: Stores all expenses with details such as amount, category, and description.
User Table: Stores user information and their monthly budget.
Ensure that you have created the necessary tables as defined in the ExpenseTrackerDbContext class.

API Endpoints
POST /api/expenses: Add a new expense.
GET /api/expenses: Get a list of all expenses.
DELETE /api/expenses/{id}: Delete an expense by ID.
GET /api/expenses/categories: Get a list of expense categories.
POST /api/budget: Set the monthly budget.
GET /api/budget: Get the current monthly budget and remaining budget.
POST /api/notifications: Notify the user when their budget is exceeded.
Authentication
This backend uses JWT for securing endpoints. Ensure that the client (frontend) includes a valid JWT token in the Authorization header of requests to protected endpoints.

Running Tests
To run unit tests, use the following command:
bash
Copy code
dotnet test
