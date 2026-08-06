---
title: ERP Platform Development and Modernization

organization: Episteme AS

role: Fullstack Developer

period:
  from: 2023-02
  to: 2025-06

status: completed

technologies:
  - csharp
  - dotnet
  - javascript
  - sql-server
  - iis
  - rest-api

concepts:
  - enterprise-software
  - legacy-modernization
  - backend-development
  - api-design
  - system-integration
  - e-commerce
  - scalability
  - maintainability
  - debugging
  - production-systems
  - server-administration

links:
  github:
  live: Not available

---

# Overview

Continuous development and modernization of a business-critical ERP platform together with a complete redesign of its connected e-commerce solution.

The project involved extending an existing ERP system, developing new data access logic, maintaining IIS-hosted production environments, and modernizing the online store architecture by replacing direct database communication with a REST API integration.

The solution supported daily operations for a catering company with multiple warehouse locations, retail operations, order processing, invoicing workflows, and an integrated online ordering platform.

The main focus was improving maintainability, system stability, user experience, and communication between connected systems while preserving existing business-critical workflows.

---

# Context

The solution consisted of an established ERP platform and an online store that were central to daily business operations.

The existing online store was tightly coupled to the ERP database through direct database access. This made future development more difficult and created unnecessary dependencies between the customer-facing application and internal business systems.

The project focused on modernizing the architecture while ensuring uninterrupted operation.

The work included:

- Redesigning the customer-facing online store.
- Introducing a REST API between the online store and ERP system.
- Extending ERP functionality based on business requirements.
- Developing new SQL queries and data access logic required by new functionality.
- Maintaining production infrastructure.

Orders completed through the online store were automatically transferred into the ERP workflow where they continued through internal order handling, invoicing, and fulfillment processes.

The role required understanding of:

- Existing ERP business logic.
- Legacy application architecture.
- SQL Server data structures.
- IIS administration.
- REST API development.
- Production troubleshooting.
- Business workflows.

Important requirements included:

- Maintaining stable production operation.
- Supporting changing business requirements.
- Reducing system coupling.
- Ensuring reliable communication between ERP and external systems.

---

# Task

My responsibility was continuous development, modernization, and operational maintenance of the ERP platform and connected systems.

This included:

- Extending ERP functionality.
- Redesigning the online store application.
- Implementing REST API communication between systems.
- Replacing direct database access from the storefront.
- Developing SQL queries and data access logic for new functionality.
- Maintaining IIS server configuration.
- Investigating and resolving production issues.
- Improving system stability and reliability.
- Supporting customer-specific workflows.

The goal was creating a more maintainable architecture while keeping business-critical systems stable and operational.

---

# Challenge

## Challenge: Modernizing a Legacy ERP Integration

### Problem

The existing online store communicated directly with the ERP database.

This created a tightly coupled architecture where changes in one system could affect the other. It also limited future development possibilities and made the solution harder to maintain.

At the same time, the business depended on reliable order processing and uninterrupted daily operations.

### Solution

Designed and implemented a REST API layer between the online store and ERP platform.

The new architecture separated responsibilities:

- The online store handled customer-facing functionality and ordering.
- The REST API handled communication between systems.
- The ERP remained responsible for business workflows, order processing, and invoicing.

The online store was redesigned with a more modern responsive experience, authentication functionality, and API-based communication instead of direct database access.

SQL queries and data access logic were developed to expose required ERP data and functionality through the API layer.

### Result

The new integration reduced coupling between systems, improved maintainability, and created a cleaner architecture while preserving existing business processes.

The business gained a more flexible platform for future improvements without exposing internal database structures directly to the online store.

---

## Challenge: Extending a Mature Enterprise System

### Problem

The ERP platform was already deeply integrated into daily operations and contained important business logic.

New requirements needed to be implemented without disrupting existing workflows or introducing unnecessary risk.

Challenges included:

- Understanding existing business rules.
- Extending functionality safely.
- Creating new data access logic.
- Maintaining compatibility with existing processes.
- Resolving production issues.

### Solution

Implemented incremental improvements through:

- New ERP functionality.
- Custom business workflows.
- SQL queries supporting new features.
- REST API extensions.
- Production debugging and improvements.
- Infrastructure maintenance through IIS.

### Result

The ERP platform continued to support evolving business requirements while remaining stable and maintainable.

---

# Action

## Architecture

### Frontend

The project included a complete redesign of the existing online store to improve usability and provide a modern responsive experience.

The previous storefront was not optimized for mobile devices and required a redesign to support customers using different screen sizes.

Responsibilities included:

- Redesigning the customer-facing online store.
- Implementing responsive layouts for desktop and mobile devices.
- Improving user experience and ordering workflows.
- Adding authentication functionality.
- Updating frontend communication to use the new REST API integration.

The redesigned storefront provided a more accessible and maintainable user experience for customers across desktop and mobile devices.

---

### Backend

Backend development focused on extending ERP functionality and building communication between systems.

Responsibilities included:

- ERP business logic.
- REST API development.
- Order integration.
- Customer-specific functionality.
- Business workflow adjustments.
- SQL query development.
- Data access implementation.
- Production debugging.

The API layer provided controlled communication between the online store and ERP platform.

---

### Database

SQL Server was used as the primary database platform.

Responsibilities included:

- Writing SQL queries for new functionality.
- Creating data access logic required by REST API endpoints.
- Working with existing ERP data structures.
- Ensuring reliable data retrieval.

Database work focused on supporting new functionality while preserving existing ERP behavior.

---

### Infrastructure

The production environment was managed through Windows Server and IIS.

Responsibilities included:

- IIS administration.
- Server configuration.
- Deployment support.
- Production troubleshooting.
- Stability improvements.

The environment required continuous operational support to maintain reliable business operation.

---

# Technical Decisions

## Decision: Introduce REST API Between Online Store and ERP

### Context

The existing architecture relied on direct database communication between the online store and ERP system.

This created unnecessary coupling and made future development more difficult.

### Chosen Solution

Implemented a REST API layer responsible for communication between systems.

The API provided:

- Controlled access to ERP functionality.
- Separation between customer-facing applications and internal systems.
- A cleaner integration architecture.

### Alternatives Considered

- Maintaining the existing direct database communication between the online store and ERP.

The existing architecture was functional, but the tight coupling made future improvements and maintenance more difficult.

### Trade-offs

Advantages:

- Reduced coupling.
- Improved maintainability.
- Better separation of responsibilities.
- Easier future development.

Disadvantages:

- Additional API development and maintenance.
- More components to monitor.

---

## Decision: Incremental Modernization Instead of Rebuilding

### Context

The ERP platform contained established business logic and was actively used in daily operations.

A complete replacement was not practical due to operational risk and the importance of existing workflows.

### Chosen Solution

Focused on incremental improvements:

- Modernizing integrations.
- Extending functionality.
- Building dedicated interfaces.
- Maintaining compatibility.

### Alternatives Considered

- Continuing with the existing tightly coupled architecture.

### Trade-offs

Advantages:

- Lower business risk.
- Faster delivery of improvements.
- Preserved existing workflows.

Disadvantages:

- Some legacy constraints remained.
- Required careful understanding of existing architecture.

---

# Implementation

Implemented improvements including:

- ERP functionality extensions.
- Complete redesign of online store frontend.
- REST API integration between ERP and online store.
- Authentication functionality for online store users.
- SQL query development for new functionality.
- IIS server administration.
- Production troubleshooting.
- Business workflow improvements.
- System stability enhancements.

Additional modernization work extended the architecture by extracting selected ERP functionality into dedicated interfaces.

This included:

- PIM functionality for improved product information management.
- Order office dashboards for improved operational workflows.
- Modern administrative interfaces built on top of the REST API.

These solutions reduced dependency on the ERP user interface and allowed specific business processes to be handled through more focused and user-friendly applications.

---

# Result

The ERP platform and connected online store became more maintainable, stable, and easier to extend.

Key outcomes:

- Modernized customer-facing online store.
- Replaced direct database communication with REST API integration.
- Improved communication between systems.
- Enabled new functionality through API-based integrations.
- Increased production stability.
- Reduced coupling between systems.
- Better support for future business requirements.

---

# Lessons Learned

## Technical Lessons

- Legacy systems can often be improved significantly through targeted incremental modernization.
- API boundaries create cleaner separation between business systems.
- Understanding existing data structures is essential when extending enterprise systems.
- Production systems require careful changes and continuous troubleshooting.

## Architectural Lessons

Working with established enterprise systems reinforced the importance of:

- Modernizing incrementally instead of unnecessary rewrites.
- Separating external applications from internal databases.
- Understanding business processes before changing architecture.
- Balancing technical improvements with operational stability.

The project demonstrated the value of gradually extracting functionality from large business systems into specialized applications.

By keeping the ERP as the source of core business logic while exposing functionality through APIs, new interfaces could be developed without tightly coupling users to the original ERP interface.

---

# Interview Notes

## Possible Questions

### Why did you introduce a REST API instead of continuing direct database access?

Direct database access created tight coupling between the online store and ERP system. The REST API created a clearer separation where each system could evolve independently while maintaining reliable communication.

---

### How did you modernize an existing ERP system without disrupting operations?

The approach was incremental. Existing workflows were preserved while improvements were introduced gradually through targeted changes, API integrations, and new user interfaces.

---

### What was your role beyond development?

The role included backend development, API integration, SQL query development, IIS administration, production troubleshooting, and maintaining stable operation of business-critical systems.

---

## Key Talking Points

- Modernized a legacy ERP integration architecture.
- Redesigned an online store connected to an existing ERP system.
- Replaced direct database access with REST API communication.
- Developed SQL queries and data access logic for new functionality.
- Built API-based integrations for future expansion.
- Worked with IIS-hosted production environments.
- Balanced modernization with stability requirements.

---

# Future Improvements

Possible improvements:

- Introduce automated monitoring and alerting.
- Add integration tests around critical business workflows.
- Improve deployment automation.
- Add structured API documentation.
- Introduce centralized logging and observability.
- Continue modernizing remaining ERP functionality through dedicated applications and API-based integrations.

---