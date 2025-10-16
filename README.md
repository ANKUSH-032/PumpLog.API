
# Petro Pump Dispensing Log Application

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 12.0.0.

API Link : https://github.com/ANKUSH-032/PumpLog.API
Angular Link : https://github.com/ANKUSH-032/pump-logs-angular

## 1. Project Overview
This is a **Petro Pump Dispensing Log application** that allows users to manage and track dispensing records at a petrol pump. The application provides:

- **Login Page:** Authenticate users before accessing the application.
- **Dispensing Records Listing:** View all dispensing records in a table format with options to view details and download records.
- **Add Dispensing Record:** Add new dispensing records with details such as Dispenser Number, Quantity Filled, Vehicle Number, Payment Mode, and file upload (PDF, JPG, PNG).

The application is designed to run entirely on local servers, supporting file upload directly without cloud storage.


## 2. Technology Stack

| Layer             | Technology                    | Reason for Choice |
|------------------|--------------------------------|-------------------|
| Frontend         | Angular 12                     | Familiarity with Angular, supports reactive forms, data binding, and component-based architecture. |
| Backend          | ASP.NET Core Web API           | Efficient for building REST APIs, easy integration with Angular, supports middleware and authentication. |
| Database         | SQL Server                     | Relational database to store dispensing records and master data. |
| ORM              | Dapper                         | Lightweight ORM, easy to use, high performance, and full control over SQL queries. |

Why this stack:
The choice of Angular + ASP.NET Core + SQL Server + Dapper is based on existing knowledge and experience, enabling faster development with a robust, maintainable structure.



## 3. Setup & Running the Project Locally

### Prerequisites
- Node.js and npm installed
- Angular CLI installed (`npm install -g @angular/cli`)
- .NET Core SDK installed (Version 8.0)
- SQL Server (local Server)

### Backend Setup
1. Open the ASP.NET Core Web API project in Visual Studio or VS Code.
2. Update the **connection string** in `appsettings.json` to point to your local SQL Server database:


"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PumpLogDb;Trusted_Connection=True;"
}


3. Run the API project locally (`F5` in Visual Studio or `dotnet run` in terminal). The API will be available at `http://localhost:5000` (or configured port).

### Frontend Setup

1. Navigate to the Angular project folder:


cd pump-logs-angular


2. Install dependencies:


npm install


3. Update the API base URL in `auth.service.ts` or environment files:
4. 
const baseURL = 'http://localhost:44300/api/';

5. Run the Angular application:


The frontend will be available at `http://localhost:4200`.


## 4. File Upload

* File upload in **Add Dispensing** works locally.
* Supported formats: `.pdf, .jpg, .jpeg, .png`.
* Uploaded files are stored in the local server folder (no cloud required).


## 5. Assumptions

1. Users will run both backend and frontend on their local machines.
2. File uploads are stored locally; no cloud storage or external file hosting is required.
3. Authentication is implemented using JWT tokens stored in local storage.
4. Master data for **Dispenser Numbers** and **Payment Modes** is fetched from the backend API.


## 6. Angular CLI Notes

### Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

### Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

### Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

### Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

### Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice.

### Further help

To get more help on the Angular CLI use `ng help` or check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
