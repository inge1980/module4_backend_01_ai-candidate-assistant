---
title: GDPR Compliant Dynamic Form Builder

organization: Moava AS

role: Fullstack Developer

period:
  from: 2013-01
  to: 2022-01

status: completed

technologies:
  - php
  - php-8.1
  - mysql
  - javascript
  - jquery
  - backbone.js
  - underscore.js
  - html
  - css
  - bootstrap
  - rest-api
  - json
  - aws-s3
  - csv
  - xls

concepts:
  - dynamic-ui
  - cms
  - frontend-architecture
  - backend-development
  - rest-api
  - json
  - data-security
  - gdpr
  - data-retention
  - automated-data-deletion
  - automation
  - user-experience
  - drag-and-drop
  - inline-editing
  - modular-design
  - data-management
  - file-storage
  - data-export

links:
  github:
  live: Not available

---

# Overview

Developed a GDPR-compliant dynamic form builder for the administration system at Moava AS.

The form builder allowed administrators to create forms from predefined templates, modify individual fields inline, duplicate fields, and organize fields through drag-and-drop interactions.

The system also handled form submissions, including viewing, deleting, responding to submitters, exporting results, and managing uploaded files.

A central requirement was GDPR-compliant data retention. Each form could define how many days submitted data was allowed to remain stored. The system automatically identified submissions approaching their retention limit and notified the form owner by email so the data could be reviewed before deletion.

---

# Context

Moava AS operated an administration system where customers needed to create and manage dynamic forms.

The form builder needed to support non-technical administrators while providing enough flexibility to construct forms from reusable templates and modify individual fields without requiring developer intervention.

The system also handled potentially sensitive information submitted through these forms. This created a requirement for explicit data-retention controls and automated deletion processes to reduce the amount of personal data stored on the company's servers.

---

# Task

Develop a dynamic form builder integrated into the existing administration system.

The solution needed to provide:

- Form creation from predefined templates.
- Inline editing of individual form fields.
- Field duplication.
- Drag-and-drop field organization.
- Visual feedback for unsaved changes.
- Storage and retrieval of form configuration through a PHP REST API.
- JSON-based communication between the frontend and backend.
- Submission management.
- Individual and bulk deletion of submissions.
- Manual communication with form submitters.
- CSV and XLS exports.
- Export of submitted files together with the corresponding submission data.
- AWS S3 storage for uploaded files.
- Configurable GDPR data-retention periods.
- Email warnings to form owners before submission data reached its deletion deadline.
- Automatic deletion of submissions and associated uploaded files after the configured retention period.

---

# Challenge

## Challenge: Building a Flexible Form Editor

### Problem

Administrators needed to modify form structures without directly editing configuration data or relying on developers.

The interface needed to make dynamic form editing understandable and efficient while allowing fields to be reordered and modified individually.

### Solution

The form builder used Backbone.js models and collections to represent form and field data received from the backend.

Each form field was stored separately and associated with its parent form through a form ID.

The frontend communicated with the PHP backend through a self-developed REST API using JSON. Backbone.js handled the JSON representations and provided the client-side model structure used to manipulate the fields.

Fields could be reordered through drag-and-drop, allowing administrators to change the form structure directly through the UI.

Inline editing was activated through an edit action on the individual field. The field then switched into an editable view where its configuration could be changed and saved.

### Result

Administrators could construct and maintain dynamic forms directly through the administration system without requiring developer involvement for routine form changes.

---

## Challenge: Detecting Unsaved Changes

### Problem

A dynamic editor needs to clearly distinguish between saved and modified state. Without visual feedback, administrators could easily leave changes unsaved or lose track of which fields had been modified.

### Solution

The original JSON representation of each field was retained as a reference, effectively creating a "ghost" version of the field state.

When a field was edited, its current JSON representation was compared with this reference state.

If the current field differed from the saved representation and had not yet been saved, the field received a faint red background in the UI.

### Result

The editor provided immediate visual feedback about fields containing unsaved changes, reducing the risk of administrators overlooking modifications.

---

## Challenge: GDPR Data Retention

### Problem

Form submissions could contain sensitive or personal information. Keeping this data indefinitely would conflict with the requirement to limit retention of personal data.

The retention period also needed to be configurable because different forms could have different legitimate storage requirements.

### Solution

Each form had a configurable number of days defining how long its submitted data could be retained.

Each submission contained a date that was used to determine its retention deadline.

As submissions approached the end of their configured retention period, the system identified the relevant data and sent an email warning to the owner of the form.

The owner could review the data before it was removed.

Once the retention period had expired, the submission data was deleted. Uploaded files associated with the submission were also subject to the same retention period.

### Result

Data retention became an explicit part of the form configuration rather than an indefinite storage policy.

The system reduced the risk of retaining personal data longer than required and provided form owners with advance notification before data was deleted.

---

## Challenge: Managing Uploaded Files

### Problem

Form submissions could contain uploaded files. These files could be large and needed to be handled separately from the structured submission data.

The export functionality also needed to provide administrators with the submission data and its associated files in a usable package.

### Solution

Uploaded files were stored in AWS S3 rather than directly in the database.

When an administrator requested an export containing uploaded files, the PHP application retrieved the files and used a PHP ZIP library to create the archive during the download process.

The archive contained the exported XLS data together with the submitted files in a folder structure. The XLS file contained internal links pointing to the corresponding submitted files.

The ZIP archive was generated as part of the download process, avoiding the need to create and persist an additional archive on the server.

Uploaded files followed the same GDPR retention period as the associated submission data.

### Result

File storage was separated from the relational database while still providing administrators with a practical way to export complete submission packages.

Generating the ZIP during download also avoided maintaining additional archive files on the server.

---

# Action

## Architecture

### Frontend

The form builder frontend was implemented using:

- Backbone.js
- Underscore.js
- jQuery
- Bootstrap
- HTML
- CSS

Backbone.js provided the client-side model and collection structure for the JSON-based form data.

Form fields were represented as frontend models and could be manipulated through the interface before being persisted through the REST API.

The UI supported:

- Template selection.
- Inline field editing.
- Field duplication.
- Drag-and-drop field ordering.
- Saved and unsaved field states.
- Visual indication of unsaved changes.
- Submission listing and management.

Bootstrap was used for the administration interface and jQuery supported frontend interaction and DOM manipulation.

---

### Backend

The backend was implemented in PHP 8.1.

The frontend communicated with PHP through a self-developed REST API using JSON.

The API provided the communication layer between the Backbone.js frontend and the backend data model.

The backend was responsible for:

- Persisting form and field configuration.
- Retrieving form configuration.
- Managing form submissions.
- Managing submission deletion.
- Processing exports.
- Managing retention-related operations.
- Handling uploaded files and their associated metadata.

---

### Database

MySQL was used for structured application data.

Each form field was stored as a separate record and associated with its parent form through a form ID.

This allowed individual fields to be created, updated, duplicated, and reordered without representing the entire form as one monolithic database record.

Form submissions were associated with their forms and included submission dates used by the retention mechanism.

---

### File Storage

Uploaded files were stored in AWS S3.

The file storage was kept separate from the relational database, while the submission data maintained the relationship between a submission and its associated files.

Uploaded files were subject to the same retention policy as the submission they belonged to.

---

### Infrastructure

The application used PHP and MySQL as its primary backend components, with AWS S3 used for uploaded file storage.

Export archives were generated dynamically when requested rather than being generated and stored in advance.

---

## Technical Decisions

## Decision: Backbone.js for Client-Side Form Management

### Context

The form builder required a structured way to manage dynamic collections of form fields received from the backend as JSON.

Fields needed to be edited, duplicated, reordered, and tracked for changes within the browser.

### Chosen Solution

Backbone.js was used to represent the form and its fields as client-side models and collections.

JSON responses from the PHP REST API could be mapped into Backbone's data structures, allowing the frontend to manipulate the form configuration before persisting changes.

### Alternatives Considered

A more direct jQuery-based implementation could have manipulated the DOM and JSON objects without a client-side model layer.

### Trade-offs

Backbone.js introduced additional structure compared with direct DOM manipulation, but provided a clearer model for managing dynamic form data and its relationship to the UI.

---

## Decision: JSON-Based REST API

### Context

The frontend and backend needed a consistent mechanism for exchanging dynamic form configuration.

### Chosen Solution

A self-developed REST API was used to exchange JSON between the Backbone.js frontend and PHP backend.

### Alternatives Considered

The form editor could have relied on traditional server-rendered HTML forms and full-page requests.

### Trade-offs

The REST/JSON approach required more client-side code but enabled a more interactive editor and made the form data structure explicit between the frontend and backend.

---

## Decision: Per-Form Data Retention

### Context

Different forms could have different legitimate requirements for how long submission data should be retained.

A fixed global retention period would not provide sufficient flexibility.

### Chosen Solution

Each form had its own configurable retention period expressed as a number of days.

The submission date and form retention period were used to determine when the data should be reviewed and eventually deleted.

### Alternatives Considered

A single global retention period could have been applied to every form.

### Trade-offs

Per-form retention introduced additional configuration and processing logic but allowed the data-retention policy to match the requirements of individual forms.

---

## Decision: AWS S3 for Uploaded Files

### Context

Form submissions could include uploaded files that should not be stored directly in the relational database.

### Chosen Solution

Uploaded files were stored in AWS S3, while the application maintained the relationship between the submission and its associated files.

### Alternatives Considered

Files could have been stored directly on the application server filesystem or as binary data in MySQL.

### Trade-offs

Using S3 separated file storage from the application database and avoided placing potentially large binary objects inside MySQL.

It also introduced a dependency on external object storage and required the application to manage S3 file lifecycle operations alongside the database records.

---

# Implementation

The form builder followed a client-server model.

1. The administrator selected a predefined form template.
2. The frontend loaded the form and its fields through the REST API.
3. PHP returned the form configuration as JSON.
4. Backbone.js represented the returned data through client-side models and collections.
5. Administrators could edit individual fields inline.
6. Fields could be duplicated and reordered using drag-and-drop.
7. The original JSON representation of a field was retained for comparison with its current state.
8. Modified but unsaved fields were visually marked with a faint red background.
9. Changes were sent back to the PHP backend through the REST API.
10. Form submissions were stored against their corresponding form.
11. Administrators could view submissions in a list and delete individual submissions or all submissions.
12. Administrators could manually contact submitters using the email address provided with the submission.
13. Submission data could be exported as CSV or XLS.
14. Exports containing uploaded files could be downloaded as ZIP archives.
15. The ZIP archive was generated during the download process using a PHP ZIP library.
16. Uploaded files were retrieved from AWS S3 as required for the export.
17. The XLS export contained internal links to the corresponding submitted files within the archive.
18. Each submission's date was evaluated against the retention period configured for its form.
19. Form owners received email warnings when submissions were approaching their deletion deadline.
20. Expired submission data and associated uploaded files were deleted according to the configured retention period.

---

# Result

The resulting system provided Moava AS with a flexible administration interface for building and managing dynamic forms.

Administrators could create forms from templates, modify fields inline, duplicate fields, and reorganize fields through drag-and-drop without developer involvement.

The system also provided a complete submission-management workflow covering viewing, deletion, manual communication, structured exports, and file exports.

Most importantly, GDPR data retention was integrated directly into the form and submission lifecycle. Forms could define their own retention period, owners received advance email warnings, and both structured submission data and uploaded files were removed after the applicable retention period.

---

# Lessons Learned

## Client-Side Models Are Valuable for Complex Dynamic UIs

Using Backbone.js models and collections provided a structured representation of dynamic form fields instead of relying exclusively on DOM state.

This became particularly useful when fields could be edited, duplicated, reordered, and compared against their persisted state.

## Unsaved State Should Be Explicit

The JSON comparison approach demonstrated that unsaved state needs to be visible in interfaces where users can make many independent changes.

A subtle visual indicator was enough to communicate that a field differed from its persisted representation without interrupting the editing workflow.

## Data Retention Should Be Designed Into the Data Lifecycle

GDPR compliance should not be treated as an isolated cleanup task.

Retention requirements affected form configuration, submission storage, notification logic, file storage, export behavior, and deletion of associated files.

## File Storage and Database Storage Have Different Responsibilities

Keeping uploaded files in S3 rather than MySQL allowed structured submission data and binary file storage to be handled independently.

The application still needed to treat the two as one logical submission for retention and deletion purposes.

## Export Requirements Can Influence Storage Design

The requirement to export submission data together with uploaded files influenced how relationships between submissions and files were maintained.

Generating ZIP archives only when requested avoided creating and storing additional archive artifacts that would themselves require lifecycle management.

---

# Future Improvements

- Replace the legacy Backbone.js/jQuery frontend with a modern component-based frontend architecture.
- Introduce automated integration tests for form editing, persistence, exports, and retention workflows.
- Add automated tests specifically covering deletion of expired submissions and associated S3 objects.
- Improve audit logging around access, export, and deletion of sensitive submission data.
- Add more granular role-based permissions for accessing and exporting submissions.
- Provide configurable retention policies with more explicit administrative controls and reporting.
- Improve export scalability for very large submission datasets.
- Move ZIP generation to a background job for very large exports.
- Add stronger validation and schema handling for form field configurations.
- Introduce automated monitoring for failed deletion or file-cleanup operations.