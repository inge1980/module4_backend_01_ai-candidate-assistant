---
title: Offline-First Hierarchical Shopping List App

organization: Personal Project

role: Fullstack Developer

period:
  from: 2024-10
  to: present

status: active

technologies:
  - react-native
  - typescript
  - redux
  - sqlite
  - supabase
  - postgresql
  - github-actions
  - vercel
  - dotenv
  - rclone
  - react-native-gesture-handler
  - react-native-reanimated

concepts:
  - architecture
  - authentication
  - automation
  - ci-cd
  - database-design
  - database-migrations
  - deployment-automation
  - devops
  - environment-management
  - local-persistence
  - mobile-development
  - mobile-performance
  - offline-first
  - qr-authentication
  - single-source-of-truth
  - state-management
  - synchronization
  - web-authentication

links:

  github: Private repository
  live: Not available

---

# Overview

An offline-first React Native application for managing hierarchical shopping lists through intuitive drag-and-drop interactions and family collaboration.

The application allows users to organize nested shopping lists, reorder items using gesture-based interactions, and manage shared lists across multiple family members. Parents authenticate using email, while children can securely join an existing family group through a QR-based linking flow.

The application is designed around an offline-first architecture where Redux acts as the Single Source of Truth. User actions update the application state immediately, persist locally through SQLite, and synchronize with Supabase PostgreSQL whenever connectivity becomes available.

The project demonstrates production-oriented mobile application architecture, combining performant UI interactions, predictable state management, local persistence, cloud synchronization, authentication, deployment automation, and environment-aware infrastructure.

---

# Context

The project was created to explore how to design a production-oriented mobile application that remains fully functional regardless of network availability while supporting shared shopping lists across multiple users.

Besides offline functionality, an important goal was making onboarding simple for families. Parents should manage accounts through traditional authentication, while children should be able to join an existing household through a simplified onboarding flow instead of completing a traditional registration process.

Important requirements included:

- Complete offline functionality.
- Immediate user feedback.
- Reliable local persistence.
- Automatic cloud synchronization.
- Simple parent/child onboarding.
- Predictable application state ownership.
- Automated deployment and backup workflows.
- Environment-specific configuration for development and production.

The primary architectural challenge was designing a system where UI state, local storage, cloud synchronization, authentication, and deployment infrastructure could work together without creating inconsistent application state.

---

# Task

My responsibility was designing and implementing the complete application architecture and technical solution.

I was responsible for:

- Designing the React Native application architecture.
- Implementing gesture-based drag-and-drop interactions.
- Building reusable UI components.
- Managing global application state.
- Designing the offline-first data flow.
- Implementing local persistence using SQLite.
- Integrating Supabase synchronization.
- Designing database backup automation.
- Making performance decisions related to animations and user interactions.
- Designing the authentication flow.
- Implementing QR-based family account linking.
- Configuring environment-specific deployments.
- Building CI/CD and database backup pipelines.

The goal was creating a maintainable architecture where the application remained fast, reliable, and predictable regardless of network conditions.

---

# Challenge

## Challenge: Smooth Drag-and-Drop Interaction Without UI Lag

### Problem

Creating responsive drag-and-drop interactions in React Native can become challenging because gesture updates happen frequently. If these updates are handled mainly through the JavaScript thread, unnecessary rendering and performance issues can occur.

The application needed:

- Smooth item movement.
- Immediate visual feedback.
- Reliable item reordering.
- Minimal frame drops during interaction.

A naive implementation could cause lag if every gesture update triggered JavaScript execution and React component updates.

### Solution

Implemented drag-and-drop functionality using:

- `react-native-gesture-handler` version 2.
- `react-native-reanimated` version 3.

Gesture handling and animations were moved to the UI thread where possible, reducing dependency on JavaScript execution during interactions.

The drag interaction allows users to:

- Press and hold the drag handle.
- Move items vertically.
- Reorder items.
- Receive immediate visual feedback.

### Result

The application provides smooth drag-and-drop interactions with reduced risk of animation stuttering caused by JavaScript thread workload.

---

## Challenge: Offline-First Data Synchronization

### Problem

The application needed to support offline usage while maintaining synchronization with cloud storage.

The challenge was preventing multiple competing sources of truth:

- Local SQLite storage.
- Remote Supabase database.
- Temporary UI state.

Without a clear ownership model, inconsistent application state could occur.

### Solution

Implemented an offline-first architecture where:

- Redux acts as the runtime application state owner.
- SQLite provides durable local persistence.
- Supabase PostgreSQL provides remote synchronization.

Application flow:

1. User interacts with the application.
2. Redux state updates immediately.
3. Changes are persisted locally.
4. Synchronization runs when connectivity is available.
5. Remote data is synchronized back into application state.

The UI consumes data from Redux instead of directly reading from databases.

### Result

The application remains functional offline while maintaining cloud synchronization when connectivity returns.

---

## Challenge: Secure Family Account Linking With Child Privacy Controls

### Problem

The application needed a secure onboarding flow where children could access shared family shopping lists without requiring a traditional account registration process.

The challenge was designing a model where:

- Parents remained responsible for account ownership and lifecycle management.
- Children could join a family group through a simple onboarding flow.
- Child-related personal information was minimized.
- Invitations expired automatically.
- QR invitations could not be reused after successful linking.
- Multiple families could generate invitations independently.
- Access to shared lists was controlled through group membership and roles.

A simple QR code containing account information would create security and privacy issues because it could expose sensitive information and bypass proper authorization controls.

### Solution

Implemented a secure family linking workflow based on temporary invitations, device registration, and group-based authorization.

The solution separates authentication, linking, and authorization responsibilities:

- Parents authenticate through their own user accounts.
- A parent creates a temporary invitation for a child profile to join the family group.
- The invitation generates a unique QR token and temporary verification code.
- The QR code contains only a reference to the invitation and does not expose account information.
- The child device scans the QR code and completes the linking process.
- After successful linking, the child uses a simplified authentication flow based on a PIN code.
- The parent account remains responsible for ownership, permissions, and account lifecycle.

The database model supports this workflow through:

- `device_invitations` for temporary family invitations.
- `qr_token` for unique invitation identification.
- `temporary_code` for additional verification.
- `expires_at` to limit invitation lifetime.
- `used` to prevent invitation reuse.
- `devices` for linked device management.
- `users` for simplified child account representation.
- `usergroupmembership` for role-based authorization.
- `sharedlists` for controlled list access.

The device model intentionally references the parent-owned account instead of storing unnecessary personal information about the child device owner. This keeps ownership and lifecycle management controlled by the parent account while minimizing stored child-related data.

### Result

The application provides a simple family onboarding experience while maintaining secure access control and privacy-aware data handling.

Children can access shared lists without a complex registration process, while the system maintains clear ownership, permission management, and account lifecycle control through the parent account.

---

# Action

## Architecture

## Architecture Summary

Short explanation of the complete system architecture.

Example:

The application follows an offline-first architecture where Redux acts as the runtime state owner. SQLite provides local persistence, while Supabase PostgreSQL handles remote synchronization.

### Frontend

Built using:

- React Native.
- TypeScript.
- Redux.
- React Native Gesture Handler.
- React Native Reanimated.

Architecture principles:

- Redux manages application state as the Single Source of Truth.
- UI components consume predictable state.
- Persistence logic is separated from presentation logic.
- Performance-critical interactions avoid unnecessary JavaScript thread work.

Implemented interaction patterns:

- Hierarchical shopping lists.
- Drag-and-drop sorting.
- Swipe-to-delete.
- Quick item creation through accessible controls.

---

### Backend

Supabase provides authentication integration, PostgreSQL storage, and synchronization services.

A Vercel-hosted web application provides browser-based authentication flows such as email verification callbacks.

The backend implements a group-based ownership model where users belong to one or more groups with role-based permissions.

Implemented backend capabilities include:

- User authentication.
- User profile management.
- Device registration.
- QR invitation generation.
- Time-limited invitation tokens.
- Group membership management.
- Shared list ownership.
- Role-based authorization.
- PostgreSQL persistence.
- Cloud synchronization.

---

### Database

The application uses a hybrid persistence architecture combining local SQLite storage with remote PostgreSQL through Supabase.

The database design separates responsibilities between:

- Local persistence for offline functionality.
- Remote storage for synchronization and centralized data.
- Redux state for active application state management.

#### Local Storage

SQLite is used as the local database layer.

Purpose:

- Store application data while offline.
- Provide fast local reads and writes.
- Ensure the application remains usable without network connectivity.
- Act as the persistence layer between Redux state and remote synchronization.

The local database contains locally persisted application data required for offline functionality, including:

- Hierarchical lists and items.
- User-specific application state.
- Synchronization metadata.
- Local configuration data.

The data model supports hierarchical structures through self-referencing relationships, allowing items to contain child items and nested lists.

#### Database Migrations

A custom SQLite migration system manages local schema evolution throughout the application's lifecycle.

The migration runner:

- Executes migrations sequentially.
- Tracks completed versions.
- Prevents duplicate execution.
- Stops startup if a migration fails.

This approach provides predictable database upgrades while keeping full control over the local persistence layer.

#### Remote Storage

Supabase PostgreSQL is used as the remote database.

Purpose:

- Store synchronized application data.
- Provide centralized cloud persistence.
- Support multiple environments.
- Enable synchronization between devices.

The remote database acts as the synchronization target, while Redux remains the owner of active UI state.

#### Database Design Decisions

Important database decisions:

- SQLite was chosen because mobile applications require reliable local persistence and offline support.
- PostgreSQL was chosen through Supabase because it provides a robust relational database for synchronized cloud data.
- Foreign key relationships are used to maintain data integrity between related entities.
- Hierarchical data relationships are modeled using self-referencing foreign keys, allowing nested lists and items.
- Timestamps are stored for tracking creation and update history.
- Synchronization flags are used to identify local changes that need to be synchronized.

This database strategy allows the application to provide immediate local interactions while maintaining reliable synchronization with cloud storage.

---

### Infrastructure

The project combines managed cloud services with automated deployment and maintenance workflows.

Infrastructure includes:

- React Native mobile application.
- Web authentication application hosted on Vercel.
- Supabase backend services.
- PostgreSQL database.
- GitHub Actions automation workflows.
- Environment-specific configuration.

Deployment supports separate development and production environments using local `.env` files during development and Vercel Environment Variables in deployed environments.

---

# Technical Decisions

## Decision: Redux as Single Source of Truth

### Context

The application had multiple data sources:

- SQLite.
- Supabase.
- UI state.

A clear ownership model was required to prevent inconsistent state.

### Chosen Solution

Redux was selected as the central runtime application state layer.

UI components consume Redux state, while persistence layers synchronize with that state.

### Alternatives Considered

Direct database-driven UI:

- Components reading directly from SQLite or Supabase.

### Trade-offs

Advantages:

- Predictable state flow.
- Easier debugging.
- Clear separation of responsibilities.
- Reduced unnecessary UI updates.

Disadvantages:

- Additional architecture complexity.
- Synchronization logic becomes the developer's responsibility.

---

## Decision: UI Thread Animations

### Context

Drag-and-drop interactions require frequent updates and need to remain responsive.

### Chosen Solution

Used React Native Reanimated 3 to execute animations on the UI thread.

### Alternatives Considered

React Native Animated API.

### Trade-offs

Advantages:

- Better interaction performance.
- Reduced JavaScript thread dependency.
- Smoother gestures.

Disadvantages:

- More complex implementation model.

---

## Decision: Custom SQLite Migration System

### Context

The application required a predictable way to evolve the local database schema as features were added.

Manually changing local databases would risk schema inconsistencies between application versions.

### Chosen Solution

Implemented a custom migration runner that:

- Executes migrations sequentially.
- Tracks completed migration versions.
- Prevents duplicate execution.
- Stops initialization if a migration fails.

Migration versions are organized using folder and step numbering.

Example:

- Migration folder: version group.
- Migration step: execution order within that version.

The current implementation focuses on controlled forward migration execution. Automatic rollback is not currently implemented.

### Alternatives Considered

Using an external migration library.

### Trade-offs

Advantages:

- Full control over SQLite lifecycle.
- Lightweight implementation.
- No additional dependencies.
- Migration behavior is explicitly defined by the application.

Disadvantages:

- More maintenance responsibility.
- Requires custom testing.
- Less functionality than mature migration frameworks.
- Advanced rollback and recovery strategies require additional implementation.

---

## Decision: Environment-Based Configuration

### Context

The application required separate development and production environments while avoiding hardcoded configuration values.

### Chosen Solution

Environment-specific configuration was implemented using local `.env` files during development and Vercel Environment Variables in deployed environments.

Application services automatically route requests to the correct backend depending on the current environment.

### Alternatives Considered

- Hardcoded endpoints.
- Manual configuration before deployment.

### Trade-offs

Advantages:

- Cleaner deployments.
- Reduced deployment mistakes.
- Better separation between environments.

Disadvantages:

- Additional deployment configuration.
- More environment variables to manage.

---

# Implementation

Implemented features:

## Mobile Application

- Hierarchical shopping lists.
- Nested drag-and-drop sorting.
- Swipe-to-delete interactions.
- Offline-first persistence.
- Redux Single Source of Truth architecture.
- SQLite local persistence.
- Supabase synchronization.
- Parent authentication flow.
- QR-based device onboarding.
- Family and group-based list sharing.
- Role-based access control (Owner, Admin, Member).
- Environment-aware configuration for development and production.

## Backend

Implemented backend functionality including:

- User account management.
- User group management.
- Device registration.
- QR invitation workflow.
- Temporary invitation codes with expiration.
- Group membership management.
- Shared list ownership.
- PostgreSQL data persistence.
- Synchronization logic between local and remote storage.

## Backup Automation

Implemented automated database backup workflows using GitHub Actions.

Features:

- Scheduled PostgreSQL backups.
- Database dump generation.
- Backup compression.
- Cloud upload using rclone.
- Retry handling.
- Failure notifications.
- Backup retention cleanup.

## Deployment Automation

Implemented automated deployment workflows and environment configuration.

Features:

- GitHub Actions based automation.
- Separate development and production configuration.
- Environment variable management.
- Automated deployment validation.

---

# Result

The project evolved into a production-oriented mobile application demonstrating several advanced architectural patterns.

Key outcomes include:

- Offline-first architecture.
- Redux as a Single Source of Truth.
- Hybrid SQLite and Supabase persistence.
- QR-based family onboarding.
- Privacy-aware child profile linking.
- Smooth UI-thread driven interactions.
- Environment-aware deployments.
- Automated CI/CD workflows.
- Automated PostgreSQL backup infrastructure.

The final architecture remains responsive without network connectivity while providing reliable synchronization once connectivity returns.

---

# Lessons Learned

## Technical Lessons

- Offline-first applications require clear ownership of data state.
- Persistence layers should not directly control UI state.
- Mobile performance depends heavily on minimizing unnecessary JavaScript thread work.
- Gesture-heavy applications require architecture decisions early.

## Architectural Lessons

Using Redux as the Single Source of Truth simplified application behavior because all UI state had a predictable owner.

The project reinforced several architectural principles:

- Clear ownership of application state reduces synchronization complexity.
- Offline-first systems require careful separation between UI state, local persistence, and remote data.
- Background synchronization should run independently from user interactions.
- Database migrations need to be treated as part of the application's lifecycle, not as one-time changes.

If rebuilding the project today, I would consider:

- More advanced conflict resolution strategies for simultaneous updates.
- Automated end-to-end synchronization testing.
- Better telemetry around synchronization failures.
- More detailed monitoring of backup and recovery workflows.

---

# Future Improvements

Possible improvements:

- Add automated tests for synchronization logic.
- Add conflict resolution strategies.
- Introduce optimistic updates with rollback support.
- Add analytics and crash monitoring.
- Improve database indexing for larger datasets.
- Add end-to-end testing using tools such as Detox.
- Add automated deployment pipelines.
- Push notifications.
- Background synchronization services.
- Device-to-device synchronization optimization.

---