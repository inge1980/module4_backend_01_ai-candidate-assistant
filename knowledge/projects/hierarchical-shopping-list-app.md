---
title: Hierarchical Drag & Drop Shopping List App

organization: Personal Project

role: Fullstack Developer

period:
  from: 2024-01
  to: 2025-01

status: completed

technologies:
  - react-native
  - typescript
  - redux
  - sqlite
  - supabase
  - postgresql
  - github-actions
  - rclone
  - react-native-gesture-handler
  - react-native-reanimated

concepts:
  - architecture
  - automation
  - database-design
  - database-migrations
  - devops
  - local-persistence
  - mobile-development
  - mobile-performance
  - offline-first
  - state-management
  - synchronization

links:

  github: Private repository
  live: Not available

---

# Overview

A React Native shopping list application built around an offline-first architecture with hierarchical drag-and-drop interactions.

The application allows users to organize shopping items through interactive lists where items can be reordered using gesture-based drag-and-drop, created through an easily accessible action button, or removed using swipe gestures.

The main goal was to build a reliable mobile experience that remains fully functional regardless of network availability. Data is stored locally first and synchronized with Supabase when connectivity is available.

The project demonstrates advanced React Native concepts including gesture handling, UI-thread animations, predictable state management, local persistence, cloud synchronization, and automated infrastructure workflows.

---

# Context

The project was created to explore how to design and implement a production-oriented mobile application with offline capabilities and reliable data synchronization.

Many mobile applications assume constant network availability, which can result in poor user experiences when connectivity is unstable. A mobile application should provide immediate feedback and remain usable regardless of connection status.

Important requirements:

- The application needed to work fully offline.
- User interactions needed to feel immediate and responsive.
- Data needed local persistence.
- Cloud synchronization was required when connectivity returned.
- Application state needed a clear ownership model.
- Database reliability required automated backup processes.

The main technical challenge was designing an architecture where local storage, remote synchronization, and UI state could coexist without creating inconsistent application data.

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

Backend functionality is handled through Supabase, providing PostgreSQL storage and synchronization capabilities.

Responsibilities:

- Remote data persistence.
- Synchronization target for offline changes.
- Authentication and user-related backend services.
- Database management.

Supabase was chosen because it provides:

- Managed PostgreSQL infrastructure.
- Simple database access.
- Hosted backend services.

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

The local database contains entities for:

- Users.
- User settings.
- Hierarchical lists and items.
- User groups.
- Shared lists.
- Membership relationships.

The data model supports hierarchical structures through self-referencing relationships, allowing items to contain child items and nested lists.

#### Database Migrations

A custom SQLite migration system was implemented to manage database schema changes throughout the application lifecycle.

The migration system provides:

- Ordered schema updates.
- Migration state tracking.
- Prevention of duplicate migration execution.
- Controlled failure handling.
- Support for future schema evolution.

Migration execution is handled during application startup after the database connection has been established.

The migration process:

1. Opens the SQLite database through a centralized database manager.
2. Checks the current migration state.
3. Identifies pending migrations.
4. Executes migrations sequentially.
5. Updates migration state after successful completion.

If a migration fails, execution stops to prevent the application from running against an inconsistent database schema.

For future schema changes, migrations can include rollback strategies when required to safely recover from failed updates or incomplete changes.

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

The project uses managed cloud services combined with automated GitHub Actions workflows.

Current infrastructure:

- React Native mobile application.
- Supabase backend.
- PostgreSQL database.
- Private GitHub repository.
- GitHub Actions automation workflows.

Implemented automation:

- Scheduled database backup workflows.
- Production and developer backup pipelines.
- PostgreSQL dumps using `pg_dump`.
- Compression using `gzip`.
- Large backup file splitting.
- Cloud uploads using `rclone`.
- Retry handling for failed uploads.
- SMTP failure notifications.
- Automatic cleanup of backups older than 30 days.

Backup strategies:

### Production Backup

- Runs daily at 01:00 UTC.
- Creates compressed PostgreSQL backups.
- Uploads backups to external storage.
- Maintains historical backups for disaster recovery.

### Developer Backup

- Runs every third day at 02:00 UTC.
- Maintains separate developer environment backups.
- Uses the same retention and notification strategy.

The backup workflows reduce manual operational tasks and provide an automated recovery mechanism.

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

# Implementation

Implemented features:

## Mobile Application

- Hierarchical shopping lists.
- Drag-and-drop sorting.
- Swipe-to-delete.
- Add-item workflow.
- Offline-first persistence.
- SQLite integration.
- Redux state architecture.
- Supabase synchronization.
- PostgreSQL backend storage.

## Infrastructure & Automation

- GitHub Actions scheduled workflows.
- Automated production database backups.
- Automated developer database backups.
- PostgreSQL dump automation.
- Backup compression.
- Backup file splitting.
- External cloud upload automation.
- Failure notification handling.
- Backup retention cleanup.

---

# Result

The project resulted in a complete offline-first React Native application demonstrating production-oriented mobile architecture patterns.

Technical outcomes:

- Smooth gesture-driven interactions.
- Offline application support.
- Local persistence through SQLite.
- Cloud synchronization through Supabase.
- Centralized state management through Redux.
- Automated database backup workflows.

The application provides a reliable user experience where network availability does not affect core functionality.

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

# Interview Notes

## Possible Questions

### Why did you choose Redux instead of using SQLite directly?

Redux provides predictable application state management and keeps UI logic independent from persistence mechanisms. SQLite is responsible for storage, not controlling application behavior.

---

### Why use Reanimated instead of standard animations?

Drag-and-drop requires continuous updates. Running animations closer to the UI thread reduces JavaScript workload and improves responsiveness.

---

### How does offline synchronization work?

The application updates Redux immediately, persists changes locally through SQLite, and synchronizes with Supabase when connectivity becomes available.

---

### How did you handle database reliability?

I implemented automated GitHub Actions workflows that periodically create PostgreSQL backups, upload them to external storage, notify failures, and remove expired backups.

---

### What was the biggest technical challenge?

Designing a clean offline-first architecture where local persistence, remote synchronization, and UI state could coexist without creating inconsistent data.

---

# Key Talking Points

- Designed and implemented a complete React Native application architecture.
- Built an offline-first data flow.
- Used Redux as Single Source of Truth.
- Implemented performant gesture-driven interactions.
- Optimized animations using UI-thread execution.
- Integrated SQLite persistence with Supabase synchronization.
- Built automated database backup workflows using GitHub Actions.

---

# Future Improvements

Possible improvements:

- Add automated tests for synchronization logic.
- Implement background synchronization workers.
- Add conflict resolution strategies.
- Introduce optimistic updates with rollback support.
- Add analytics and crash monitoring.
- Improve database indexing for larger datasets.
- Add end-to-end testing using tools such as Detox.
- Add automated deployment pipelines.

---