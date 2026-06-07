School Portal

A modern School Management System built with ASP.NET MVC that streamlines academic and administrative operations. The application provides centralized management for students, teachers, courses, attendance, and academic records while following industry-standard software architecture and deployment practices.

Overview

School Portal is designed to simplify school operations by providing a web-based platform for managing educational workflows. The system supports role-based access and enables efficient communication and record management across the institution.

Features

Student Management

* Student registration and profile management
* Student enrollment tracking
* Academic record management

Teacher Management

* Teacher profile management
* Course assignment
* Class management

Course Management

* Create and manage courses
* Assign instructors
* Track course information

Attendance Tracking

* Record attendance
* Monitor attendance history
* Generate attendance reports

Academic Performance

* Grade management
* Performance tracking
* Student progress monitoring

Security

* Authentication and authorization
* Role-based access control
* Protected administrative operations

Technology Stack

Backend

* ASP.NET MVC
* C#
* Entity Framework

Database

* SQL Server

Frontend

* HTML5
* CSS3
* Bootstrap
* JavaScript

DevOps

* Docker
* Multi-Stage Docker Builds

Architecture

The application follows a layered architecture pattern:

Presentation Layer (MVC)
        │
Business Logic Layer
        │
Data Access Layer
        │
     SQL Server

Project Structure

SchoolPortal
│
├── Controllers
├── Models
├── Views
├── Services
├── Data
├── wwwroot
├── Dockerfile
└── appsettings.json

Prerequisites

Before running the project, ensure the following are installed:

* .NET SDK
* SQL Server
* Docker Desktop (Optional)
* Visual Studio 2022

Local Development Setup

Clone the Repository

git clone https://github.com/maabdella/SchoolPortal.git

Navigate to the Project

cd SchoolPortal

Configure Database Connection

Update the connection string inside:

appsettings.json

Example:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SchoolPortalDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

Apply Migrations

dotnet ef database update

Run the Application

dotnet run

The application will start locally and become available through the configured ASP.NET endpoint.

Docker Deployment

The project includes Docker support for simplified deployment and environment consistency.

Build Docker Image

docker build -t schoolportal .

Run Docker Container

docker run -d -p 8080:8080 --name schoolportal schoolportal

View Running Containers

docker ps

Stop Container

docker stop schoolportal

Remove Container

docker rm schoolportal

Multi-Stage Docker Build

The Docker image uses a multi-stage build process to:

* Reduce image size
* Improve security
* Separate build and runtime environments
* Optimize deployment performance

Build Stage

Uses the .NET SDK image to:

* Restore dependencies
* Build the application
* Publish release artifacts

Runtime Stage

Uses the lightweight ASP.NET Runtime image to:

* Run the published application
* Minimize container size
* Improve startup performance

Database

The application uses SQL Server with Entity Framework for:

* Data persistence
* Database migrations
* ORM functionality
* Query optimization

Security Considerations

* Authentication enabled
* Authorization policies implemented
* Secure database access
* Environment-based configuration support

Future Enhancements

* Online examinations
* Parent portal
* Email notifications
* Dashboard analytics
* Report generation
* Mobile application integration

Author

Muhamed Abdella

GitHub: https://github.com/maabdella

License

This project is licensed under the MIT License.
