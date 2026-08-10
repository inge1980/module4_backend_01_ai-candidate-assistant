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
  - aws-sdk
  - phpspreadsheet
  - zipstream
  - csv
  - xls
  - zip
  - subversion

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
  - data-processing
  - streaming
  - cloud-storage

dependencies:
  - maennchen/zipstream-php
  - aws/aws-sdk-php
  - phpoffice/phpspreadsheet

links:
  github:
  live: Not available

---

# Overview

Developed a GDPR-compliant dynamic form builder for the administration system at Moava AS.

The form builder allowed administrators to create forms from predefined templates, modify individual fields inline, duplicate fields, and organize fields through drag-and-drop interactions.

The system also handled form submissions, including viewing, deleting, responding to submitters, exporting results, and managing uploaded files.

A central requirement was GDPR-compliant data retention. Each form could define how many days submitted data was allowed to remain stored. The system automatically identified submissions approaching their retention limit and notified the form owner by email so the data could be reviewed before deletion.

The project also included a dedicated export and data-processing solution. Customers could export collected form results to CSV and XLS, including submitted files, and use the exported Excel data for further processing, analysis, consolidation, and sharing.

---

# Context

Moava AS operated an administration system where customers needed to create and manage GDPR compliant dynamic forms and questionnaires.

The form builder needed to support non-technical administrators while providing enough flexibility to construct forms from reusable templates and modify individual fields without requiring developer intervention.

After developing a questionnaire template for the form builder, a requirement emerged to export the collected responses so customers could process and analyze the data outside the administration system.

The export functionality needed to support both structured response data and uploaded files. Customers needed a practical way to receive the collected information in familiar spreadsheet formats while maintaining relationships between each response and its submitted attachments.

The system also handled potentially sensitive information submitted through these forms. This created a requirement for explicit data-retention controls and automated deletion processes to reduce the amount of personal data stored by Moava AS.

The underlying principle was to allow customers to take responsibility for further processing and retention of exported data while information stored by Moava AS could be deleted according to the configured retention policy and the users' consent.

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
- Export of submitted files together with the submission data.
- AWS S3 storage for uploaded files.
- Configurable GDPR data-retention periods.
- Email warnings to form owners before submission data reached its deletion deadline.
- Automatic deletion of submissions and associated uploaded files after the configured retention period.

A separate requirement was to implement an export solution that allowed customers to take collected form data out of the system for further processing.

The export solution needed to:

- Export form results to CSV.
- Export form results to XLS.
- Include submitted files in the export.
- Package submitted files into a ZIP archive.
- Provide working links from the Excel export to the corresponding submitted files.
- Allow customers to download and process their collected data outside the administration system.
- Reduce manual work for administrators.
- Support GDPR-oriented data lifecycle management by allowing customers to take responsibility for further processing while stored data could subsequently be deleted according to the configured retention policy.

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

## Challenge: Exporting and Consolidating Form Data

### Problem

After developing questionnaire templates for the form builder, customers needed a way to export collected responses for further processing and analysis.

The raw data needed to be available in common formats such as CSV and XLS. At the same time, submissions could contain uploaded files that needed to remain associated with the corresponding response.

Simply exporting database records would not provide a practical workflow for customers who needed both the structured data and the submitted attachments.

### Solution

I implemented the export functionality using PHPSpreadsheet for spreadsheet generation.

The system supported:

- CSV exports.
- XLS exports.
- Export of submitted files.
- ZIP packaging of submitted files.
- Links from the exported Excel spreadsheet to the corresponding files.

The file export was designed around streaming rather than first downloading all files from S3 to the application's local filesystem.

Uploaded files were stored in AWS S3 and retrieved through the AWS SDK for PHP.

When an export containing files was requested, the application streamed the S3 objects directly into a ZIP archive using `maennchen/zipstream-php`. The files were compressed on the fly as part of the export process.

The resulting ZIP archive could then be streamed directly to the customer's browser as a download without requiring the application server to first create a complete ZIP file on local disk.

The exported XLS file contained internal links to the corresponding submitted files. After unpacking the ZIP archive, customers could open the spreadsheet and follow the links to the associated files.

This approach avoided an unnecessary intermediate storage step:

`AWS S3 -> streaming ZIP generation -> browser download`

rather than:

`AWS S3 -> local server filesystem -> ZIP file -> browser download`

### Result

The export functionality gave customers a practical way to work with collected form data outside the administration system.

It reduced manual administrative work and avoided unnecessary local disk usage during file exports.

The export package maintained a usable relationship between response data and uploaded files, allowing customers to process, analyze, consolidate, and share the collected information.

The approach also supported the broader GDPR data lifecycle by allowing customers to take responsibility for further processing while limiting how long the original data needed to remain stored by Moava AS.

---

## Challenge: Managing Uploaded Files

### Problem

Form submissions could contain uploaded files. These files could be large and needed to be handled separately from the structured submission data.

The export functionality also needed to provide administrators with the submission data and its associated files in a usable package without requiring all files to be temporarily downloaded to the application server.

### Solution

Uploaded files were stored in AWS S3 rather than directly in the database.

The AWS SDK for PHP was used to access the S3 objects.

When an administrator requested an export containing uploaded files, the application streamed the objects from S3 into a ZIP archive using `maennchen/zipstream-php`.

The files were compressed on the fly rather than first being downloaded to the server's local filesystem.

The archive contained the exported XLS data together with the submitted files in a folder structure. The XLS file contained internal links pointing to the corresponding submitted files.

The ZIP archive was generated and streamed as part of the download process, avoiding the need to create and persist a complete intermediate archive on the application server.

The resulting architecture was effectively:

`AWS S3 -> AWS SDK for PHP -> ZipStream -> HTTP response -> customer`

This reduced temporary local storage requirements and allowed the application to process the files as a stream.

Uploaded files followed the same GDPR retention period as the associated submission data.

### Result

File storage was separated from the relational database while still providing administrators with a practical way to export complete submission packages.

Streaming the files directly from S3 into the ZIP generation process avoided unnecessary local disk usage and eliminated the need to maintain temporary ZIP files on the application server.

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

PHPSpreadsheet was used for generating spreadsheet exports.

---

### Database

MySQL was used for structured application data.

Each form field was stored as a separate record and associated with its parent form through a form ID.

This allowed individual fields to be created, updated, duplicated, and reordered without representing the entire form as one monolithic database record.

Form submissions were associated with their forms and included submission dates used by the retention mechanism.

---

### File Storage

Uploaded files were stored in AWS S3.

The AWS SDK for PHP was used to access S3 objects.

The file storage was kept separate from the relational database, while the submission data maintained the relationship between a submission and its associated files.

Uploaded files were subject to the same retention policy as the submission they belonged to.

---

### Export Pipeline

The export pipeline combined structured form data with uploaded files.

The general flow was:

1. Retrieve the requested form submissions.
2. Transform the submission data into CSV or XLS output.
3. Retrieve metadata for associated uploaded files.
4. Open a streaming ZIP response when files were included.
5. Stream each required file directly from AWS S3.
6. Compress the file data on the fly using `maennchen/zipstream-php`.
7. Add the generated spreadsheet and submitted files to the ZIP archive.
8. Maintain relative/internal links in the spreadsheet to the corresponding files.
9. Stream the resulting ZIP response to the customer's browser.
10. Avoid downloading all S3 files to the application server's local disk.
11. Avoid creating a complete persistent ZIP archive before the download begins.

The resulting data flow was:

`AWS S3 -> AWS SDK for PHP -> ZipStream -> HTTP response -> browser`

This was preferable to first copying every S3 object to local disk and then creating a ZIP file because the latter would introduce unnecessary disk I/O, temporary storage requirements, and cleanup work.

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

The AWS SDK for PHP provided the integration with S3.

### Alternatives Considered

Files could have been stored directly on the application server filesystem or as binary data in MySQL.

### Trade-offs

Using S3 separated file storage from the application database and avoided placing potentially large binary objects inside MySQL.

It also introduced a dependency on external object storage and required the application to manage S3 file lifecycle operations alongside the database records.

---

## Decision: PHPSpreadsheet for Data Export

### Context

Customers needed to export collected form responses in formats that could be opened and processed using common spreadsheet applications.

### Chosen Solution

PHPSpreadsheet was used to generate XLS exports from collected form data.

CSV export was also supported for workflows where a simpler tabular representation was more appropriate.

The XLS output could contain internal links to uploaded files included in the accompanying ZIP archive.

### Alternatives Considered

The application could have generated spreadsheet-compatible output manually or provided database-level exports.

### Trade-offs

Using a dedicated spreadsheet library provided more control over the generated XLS files and allowed the export to include working links to submitted files.

It added a third-party dependency but significantly reduced the complexity of generating valid spreadsheet documents.

---

## Decision: Streaming ZIP Generation

### Context

Exported submissions could contain many uploaded files stored in AWS S3.

Downloading all files to the application server before creating the ZIP archive would require temporary local storage and additional disk I/O.

Persisting a complete ZIP archive would also create another temporary artifact that would need to be managed and removed.

### Chosen Solution

`maennchen/zipstream-php` was used to generate ZIP archives as a stream.

The AWS SDK for PHP provided access to the S3 objects, which could then be passed through the ZIP generation process without first storing the complete files on local disk.

Files were compressed as they were streamed into the ZIP response.

The resulting ZIP was streamed directly to the customer's browser.

### Alternatives Considered

A conventional implementation could have:

1. Downloaded every S3 file to the application server.
2. Stored the files temporarily on local disk.
3. Created a ZIP archive from those files.
4. Served the completed ZIP file.
5. Removed the temporary files and archive.

### Trade-offs

The streaming approach reduced temporary disk usage and eliminated the need for a persistent intermediate ZIP file.

The implementation was more dependent on correct stream handling and required the export request to remain active while the ZIP was generated.

For large exports, this also made HTTP request duration and connection reliability important operational considerations.

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
14. PHPSpreadsheet generated the XLS representation of the collected data.
15. Uploaded files were stored in AWS S3.
16. The AWS SDK for PHP provided access to the submitted files.
17. For file-inclusive exports, the application streamed the required S3 objects instead of first downloading them to local disk.
18. `maennchen/zipstream-php` compressed the streamed file data into a ZIP archive on the fly.
19. The exported XLS file contained internal links to the corresponding submitted files.
20. The spreadsheet and submitted files were packaged into the streamed ZIP response.
21. The ZIP archive was streamed directly to the customer's browser.
22. No complete intermediate ZIP archive needed to be persisted on the application server.
23. No complete copy of every S3 file needed to be stored on the application server before ZIP generation.
24. Each submission's date was evaluated against the retention period configured for its form.
25. Form owners received email warnings when submissions were approaching their deletion deadline.
26. Expired submission data and associated uploaded files were deleted according to the configured retention period.

---

# Result

The resulting system provided Moava AS with a flexible administration interface for building and managing dynamic forms.

Administrators could create forms from templates, modify fields inline, duplicate fields, and reorganize fields through drag-and-drop without developer involvement.

The submission-management workflow covered viewing, individual and bulk deletion, manual communication, structured exports, and file exports.

The export functionality gave customers a practical way to extract and consolidate collected form data in CSV and XLS formats.

For submissions containing uploaded files, the system produced a ZIP package containing the exported spreadsheet and the corresponding files. The spreadsheet contained working internal links to the submitted files, making the exported dataset usable after it had been downloaded and unpacked.

The file-export architecture streamed files directly from AWS S3 into the ZIP-generation process. Files did not need to be downloaded completely to the application's local filesystem before compression, reducing temporary disk usage and avoiding the need to create a persistent intermediate ZIP archive.

The export workflow reduced manual administrative work and made it easier for customers to analyze, consolidate, and share collected information outside the administration system.

Most importantly, GDPR data retention was integrated directly into the form and submission lifecycle. Forms could define their own retention period, owners received advance email warnings, and both structured submission data and uploaded files were removed after the applicable retention period.

This allowed customers to take responsibility for further processing of exported information while limiting the amount of personal data that needed to remain stored by Moava AS.

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

The use of streaming ZIP generation meant the export pipeline could work directly with objects in S3 without first materializing every file on local disk.

## Streaming Is Useful for Large File Operations

Streaming data from S3 through ZIP generation avoided an unnecessary intermediate copy of every file on the application server.

The architecture reduced local disk requirements and allowed the application to process files incrementally as part of the HTTP response.

This is particularly useful when dealing with potentially large collections of uploaded files.

## Data Export Can Be Part of a Privacy Strategy

Exporting data is not only a convenience feature.

By giving customers a usable representation of their collected data, including its associated files, the system allowed further processing to happen outside the application while the original stored data could be removed according to the configured retention policy.

This required the export process to be treated as part of the overall data lifecycle rather than as an isolated reporting feature.

---

# Future Improvements

- Replace the legacy Backbone.js/jQuery frontend with a modern component-based frontend architecture.
- Introduce automated integration tests for form editing, persistence, exports, and retention workflows.
- Add automated tests specifically covering deletion of expired submissions and associated S3 objects.
- Improve audit logging around access, export, and deletion of sensitive submission data.
- Add more granular role-based permissions for accessing and exporting submissions.
- Provide configurable retention policies with more explicit administrative controls and reporting.
- Improve export scalability for very large submission datasets.
- Move very large export generation to background jobs instead of tying generation to a single HTTP request.
- Add progress reporting for large exports.
- Add stronger validation and schema handling for form field configurations.
- Introduce automated monitoring for failed deletion or file-cleanup operations.
- Add explicit verification that spreadsheet links remain valid after export packaging.
- Introduce automated end-to-end tests covering the complete export workflow, including S3 files, XLS generation, ZIP streaming, and linked attachments.

---