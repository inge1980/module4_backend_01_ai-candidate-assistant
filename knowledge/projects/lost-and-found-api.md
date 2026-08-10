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
  - environment-configuration
  - utc-timestamps
  - api-documentation

links:

  github:
  live:

---

# Overview

Lost & Found is a small backend system built with ASP.NET Core Web API, PostgreSQL, Docker Compose, and xUnit.

The application allows users to register found items and allows employees to mark items as claimed and returned.

The system supports creating and listing found items, filtering items by status and category, searching items, claiming found items, returning items to owners, and deleting available items.

---

# Context

The project is a backend system for managing found items.

The application uses ASP.NET Core Web API, Entity Framework Core, PostgreSQL 16, Docker Compose, Swagger/OpenAPI, DotNetEnv, and xUnit.

PostgreSQL provides persistent storage. Docker Compose is used to run the ASP.NET Core API and PostgreSQL database together during development.

The project also includes automated tests covering item creation, timestamps, item status rules, repository filtering, validation, and API responses.

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
- Testing application behaviour

---

# Challenge

## Challenge: Managing Found Item Status

### Problem

Found items have different statuses, and the available operations depend on the item's current status.

The application needs to apply rules around claiming, returning, and deleting items.

### Solution

The application implements claim, return, and delete rules.

New items are created with `Available` status.

Available items can be deleted.

The test suite verifies the claim, return, and delete rules.

### Result

The application's item operations are covered by explicit status-related rules.

These rules are also covered by automated tests.

---

# Action

## Architecture

### Frontend

The backend exposes an HTTP API that can be explored and tested through Swagger UI.

### Backend

The backend is built with ASP.NET Core Web API.

The API supports:

- Creating found items
- Listing found items
- Filtering items by status and category
- Searching items
- Claiming found items
- Returning items to owners
- Deleting available items

The API is available at:

`http://localhost:8080/api/items`

Swagger UI is available at:

`http://localhost:8080/swagger/index.html`

### Database

The application uses PostgreSQL 16 for persistent storage.

Entity Framework Core is used for database access with the PostgreSQL repository.

PostgreSQL runs through Docker Compose.

PostgreSQL data is stored in a Docker volume so that the data survives container restarts.

Entity Framework Core `EnsureCreated()` is used for automatic database setup during development.

### Infrastructure

Docker Compose runs the ASP.NET Core API and PostgreSQL database together.

The application can be started with:

`docker compose up --build`

This starts:

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

The application requires a persistence mechanism for storing and retrieving found items.

### Chosen Solution

The application uses a repository abstraction for persistence.

Two repository implementations are described:

- An in-memory repository for fast tests
- A PostgreSQL repository for production-like execution

---

## Decision: PostgreSQL with Docker Compose

### Context

The application requires persistent storage for found items and a development environment that includes both the API and database.

### Chosen Solution

PostgreSQL 16 is used as the database.

PostgreSQL is run through Docker Compose alongside the ASP.NET Core API.

PostgreSQL data is stored in a Docker volume.

### Trade-offs

Docker Desktop must be running to start the application with Docker Compose.

The Docker volume allows PostgreSQL data to survive container restarts.

---

## Decision: In-Memory Repository for Tests

### Context

The project includes automated tests and provides an in-memory repository specifically for fast tests.

### Chosen Solution

The in-memory repository is used for fast tests.

The PostgreSQL repository is used for production-like execution.

---

## Decision: UTC Timestamps

### Context

Found item timestamps are part of the application's tested behaviour.

### Chosen Solution

Found timestamps are generated in UTC.

---

## Decision: Swagger/OpenAPI

### Context

The API needs to be available for exploration and testing.

### Chosen Solution

Swagger/OpenAPI is used for API documentation.

Swagger UI provides an interface for exploring and testing the available endpoints.

---

## Implementation

The application is implemented as an ASP.NET Core Web API backed by PostgreSQL.

Users can register found items.

Employees can mark found items as claimed and returned.

The API supports creating and listing found items, filtering by status and category, searching items, claiming items, returning items to owners, and deleting available items.

New items are created with `Available` status.

Found timestamps are generated in UTC.

The persistence layer uses a repository abstraction with:

- An in-memory repository for fast tests
- A PostgreSQL repository for production-like execution

Entity Framework Core is used for database access.

Entity Framework Core `EnsureCreated()` provides automatic database setup during development.

Docker Compose starts the ASP.NET Core API and PostgreSQL database together.

Database configuration is provided through environment variables.

The PostgreSQL data is stored in a Docker volume so that it survives container restarts.

The example environment configuration can be created with:

`cp .env.example .env`

The application can be started with:

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

PostgreSQL can be exited with:

`\q`

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

The application can be run using Docker Compose, which starts the ASP.NET Core API and PostgreSQL database together.

The test suite covers:

- Item creation
- `Available` status for new items
- UTC timestamps
- Claim rules
- Return rules
- Delete rules
- Repository filtering by status
- API validation
- Missing resources
- API responses for creation, return, and delete

---

# Lessons Learned

## Repository Abstraction

The project uses a repository abstraction for persistence.

The implementation provides an in-memory repository for fast tests and a PostgreSQL repository for production-like execution.

This separates the documented testing persistence implementation from the PostgreSQL persistence implementation.

## Automated Testing

The project uses xUnit for automated testing.

The test suite verifies item creation status, UTC timestamps, claim rules, return rules, delete rules, repository filtering, validation, missing resources, and relevant API responses.

## Item Status Rules

The application's found items have status-dependent operations.

Claim, return, and delete rules are explicitly tested.

New items are created with `Available` status, and deletion is supported for available items.

## Docker-Based Development

Docker Compose runs the ASP.NET Core API and PostgreSQL database together.

PostgreSQL data is stored in a Docker volume so that it survives container restarts.

## Environment Configuration

Database configuration is provided through environment variables.

The project provides an example environment file that can be copied to `.env`.

DotNetEnv is included in the technology stack for environment configuration.

## API Documentation

Swagger/OpenAPI provides API documentation and allows the available endpoints to be explored and tested through Swagger UI.

## Database Setup

Entity Framework Core `EnsureCreated()` is used for automatic database setup during development.

---