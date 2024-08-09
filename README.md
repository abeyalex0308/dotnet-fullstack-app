# Preqin Platform README

## Overview

The Preqin Platform is a comprehensive solution for Preqin users to manage and view investor data and their commitments. It allows users to navigate through a list of investors, view detailed investor information, and analyze the total and breakdown of their commitments. Additionally, users can explore commitments associated with specific Asset Classes.

The platform is developed with .NET Core for the backend API and React with TypeScript for the frontend interface.

## Backend - .NET Core API

The backend API is structured using Clean Architecture principles to promote separation of concerns and code maintainability. The architecture is divided into several projects:

### Preqin.Application

This layer contains the application's business logic and defines interfaces for user operations. It serves as a bridge between the UI and the domain logic.

### Preqin.Core

The Core project encompasses the domain entities, enums, exceptions, interfaces, and domain-specific logic. It forms the heart of the architecture, with all other layers depending on it.

### Preqin.Infrastructure

Infrastructure is responsible for data access and external interactions such as file systems, web services, and third-party APIs. It provides concrete implementations for the interfaces defined in the Core project.

### Preqin.WebAPI

The WebAPI project provides the necessary endpoints for frontend interaction and includes configurations for middleware, dependency injection, and other API-level settings.

#### Logging and Exception Handling

A robust logging and exception handling middleware is implemented to log errors and respond with appropriate HTTP status codes and messages.

#### Database

SQLite is used as the database with Entity Framework Core, following a DB-first approach. The repository pattern is utilized within the application layer to abstract data access and ensure loose coupling.

## Frontend - React with TypeScript

The frontend is crafted using React with TypeScript for a type-safe development experience.

### Routing

The platform includes three primary pages:

- **Investor Page**: Lists investors and their details.
- **Commitments Page**: Provides a breakdown of commitments for a selected investor.

A **NotFound Page** is present to manage undefined routes.

### Components

Key reusable components include:

- **Navbar**: Facilitates navigation.
- **Layout**: Serves as a general layout wrapper for page content.
- **TableView**: Presents data in a tabular format.

## API Endpoints

The following API endpoints are available:

- List of Investors: `{{base_url}}/api/investors?pageIndex=1&pageSize=10`
- Investor Commitments: `{{base_url}}/api/investors/{investorId}/commitments`
- Asset Class Commitments: `{{base_url}}/api/investors/{investorId}/commitments/totalByAssetClass`

## Getting Started

To run the platform locally:

1. Clone the repository.
2. Navigate to the backend project and restore dependencies:
   ```
   cd Preqin.WebAPI
   dotnet restore
   ```
3. Start the backend server:
   ```
   dotnet run
   ```
4. Navigate to the frontend project and install dependencies:
   ```
   cd client-app
   npm install
   ```
5. Start the frontend server:
   ```
   npm start
   ```
6. Access the application at `http://localhost:3000`.

