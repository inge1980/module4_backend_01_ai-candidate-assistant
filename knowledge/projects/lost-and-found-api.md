---
title: Lost & Found API

organization: School Project

role: Backend Developer

period:
  from: 2026-07
  to: 2026-07

status: completed

technologies:
  - csharp
  - dotnet
  - aspnet-core
  - entity-framework-core
  - postgresql
  - docker
  - xunit
  - swagger
  - openapi
  - dotnetenv

concepts:
  - backend-api
  - repository-pattern
  - automated-testing
  - docker-development
  - backend-validation
  - item-status-management
  - search
  - filtering

links:

  github:
  live:

---

# Overview

Lost & Found is a small backend system built with ASP.NET Core Web API, PostgreSQL, Docker Compose, and xUnit.

The application allows users to register found items and allows employees to mark items as claimed and returned.

The system provides functionality for creating and listing found items, filtering by status and category, searching items, claiming items, returning items to their owners, and deleting available items.

---

# Context

The project is a backend system for managing found items.

The application uses ASP.NET Core Web API for the API, PostgreSQL 16 for persistent storage, Entity Framework Core for database access, Docker Compose for running the application and database, and Swagger/OpenAPI for API documentation and testing.

The application also includes automated tests using xUnit.

---

# Task

The task was to build a backend API for a Lost & Found system.

The application needed to support:

- Creating found items
- Listing found items
- Filtering items by status
- Filtering items by category
- Searching items
- Claiming found items
- Returning items to owners
- Deleting available items
- Persisting data in PostgreSQL
- Validating API requests
- Testing the application's behaviour

---

# Challenge

## Challenge: Managing Found Item Status

### Problem

Found items have different statuses, and operations depend on the item's current status.

The application needs to enforce rules around claiming, returning, and deleting items.

### Solution

The application includes claim rules, return rules, and delete rules.

New items are created with `Available` status.

Deletion is supported for available items.

The test suite verifies the claim, return, and delete rules.

### Result

The application provides explicit behaviour for the different operations that can be performed on found items.

The relevant rules are covered by automated tests.

---

# Action

## Architecture

### Frontend

No frontend application is described in the available project documentation.

The project provides a backend API that can be accessed through HTTP.

### Backend

The backend is built with ASP.NET Core Web API.

The API supports:

- Creating found items
- Listing found items
- Filtering by status and category
- Searching items
- Claiming found items
- Returning items to owners
- Deleting available items

The API is available at:

`http://localhost:8080/api/items`

Swagger UI is available for exploring and testing the available endpoints.

### Database

The application uses PostgreSQL 16 for persistence.

Entity Framework Core is used with the PostgreSQL repository.

PostgreSQL runs through Docker Compose.

PostgreSQL data is stored in a Docker volume so that the data survives container restarts.

The application uses Entity Framework Core `EnsureCreated()` for automatic database setup during development.

### Infrastructure

Docker Compose is used to run the application and its PostgreSQL database.

Running:

`docker compose up --build`

starts:

- ASP.NET Core API
- PostgreSQL database

Database configuration is provided through environment variables.

The example environment file can be copied using:

`cp .env.example .env`

The project uses DotNetEnv for environment configuration.

---

## Technical Decisions

## Decision: Repository Abstraction

### Context

The application requires a persistence mechanism for storing found items.

### Chosen Solution

The application uses a repository abstraction for persistence.

It provides:

- An in-memory repository for fast tests
- A PostgreSQL repository for production-like execution

### Alternatives Considered

The README does not document alternative persistence approaches that were considered.

### Trade-offs

The repository abstraction allows the application to use an in-memory repository for tests and a PostgreSQL repository for production-like execution.

The README does not document additional trade-offs of this approach.

---

## Decision: PostgreSQL with Docker

### Context

The application requires persistent storage for found items.

### Chosen Solution

PostgreSQL 16 is used as the database.

PostgreSQL runs through Docker Compose, and its data is stored in a Docker volume.

### Alternatives Considered

The README does not document alternative database or infrastructure approaches that were considered.

### Trade-offs

Using PostgreSQL through Docker Compose requires Docker Desktop to be running.

The Docker volume allows PostgreSQL data to survive container restarts.

---

## Decision: In-Memory Repository for Tests

### Context

The project includes automated tests and provides a separate repository implementation for testing.

### Chosen Solution

An in-memory repository is used for fast tests.

The PostgreSQL repository is used for production-like execution.

### Alternatives Considered

The README does not document alternative testing persistence approaches.

### Trade-offs

The README specifically identifies the in-memory repository as being used for fast tests.

It does not document the limitations or additional trade-offs of the in-memory implementation.

---

## Decision: Swagger/OpenAPI

### Context

The API needs to be available for exploration and testing.

### Chosen Solution

Swagger/OpenAPI is used to document the API.

Swagger UI can be used to explore and test the available endpoints.

### Alternatives Considered

The README does not document alternative API documentation approaches.

### Trade-offs

The README does not document specific trade-offs of using Swagger/OpenAPI.

---

## Implementation

The application is implemented as an ASP.NET Core Web API backed by PostgreSQL.

Found items can be created and listed through the API.

Items can be filtered by status and category and can also be searched.

Users can register found items.

Employees can mark items as claimed and returned.

Available items can be deleted.

New items are created with `Available` status.

Found timestamps are generated in UTC.

The persistence layer uses a repository abstraction.

An in-memory repository is used for fast tests, while a PostgreSQL repository is used for production-like execution.

Entity Framework Core is used for database access, with `EnsureCreated()` providing automatic database setup during development.

Docker Compose starts both the ASP.NET Core API and PostgreSQL database.

Database configuration is provided through environment variables.

PostgreSQL data is stored in a Docker volume.

The API can be started with:

`docker compose up --build`

The API is available at:

`http://localhost:8080/api/items`

Swagger UI is available at:

`http://localhost:8080/swagger/index.html`

Tests can be executed with:

`dotnet test`

The test suite verifies:

- New items are created with `Available` status
- Found timestamps are generated in UTC
- Claim rules
- Return rules
- Delete rules
- Repository filtering by status
- API responses for creation
- API responses for return
- API responses for delete
- API validation
- API responses for missing resources

PostgreSQL can be accessed inside Docker using:

`docker exec -it module3_backend_02_lostandfound-db-1 psql -U your_username -d your_database_name`

An example database query is:

`SELECT * FROM "Items";`

---

# Result

The completed backend provides a Lost & Found system with PostgreSQL persistence.

The application supports:

- Creating found items
- Listing found items
- Filtering by status
- Filtering by category
- Searching items
- Claiming found items
- Returning items to owners
- Deleting available items
- PostgreSQL persistence through a Docker volume
- Swagger/OpenAPI documentation
- Automated testing

The application can be run using Docker Compose, with the ASP.NET Core API and PostgreSQL database started together.

The test suite covers item creation, UTC timestamps, claim rules, return rules, delete rules, repository filtering, validation, missing resources, and relevant API responses.

---

# Lessons Learned

## Repository Abstraction

The project uses a repository abstraction for persistence.

The implementation includes an in-memory repository for fast tests and a PostgreSQL repository for production-like execution.

## Automated Testing

The project uses xUnit for automated testing.

The test suite verifies item creation status, UTC timestamps, claim rules, return rules, delete rules, repository filtering, validation, missing resources, and API responses.

## Docker-Based Development

Docker Compose allows the ASP.NET Core API and PostgreSQL database to be started together.

PostgreSQL data is stored in a Docker volume so it survives container restarts.

## Environment Configuration

Database configuration is provided through environment variables.

The project includes an example environment file that can be copied to `.env` for local configuration.

## API Documentation

Swagger/OpenAPI provides a way to explore and test the available API endpoints.

## Database Setup

Entity Framework Core `EnsureCreated()` is used for automatic database setup during development.

---

# Future Improvements

The README does not specify planned future improvements.

Potential improvements should therefore be determined from the project's requirements rather than inferred from the existing implementation.

---