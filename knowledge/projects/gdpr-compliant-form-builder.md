---
title: GDPR-Compliant Dynamic Form Builder

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
  - spreadsheet-generation
  - spreadsheet-formatting
  - hyperlink-generation
  - streaming
  - cloud-storage
  - performance-optimization
  - memory-optimization

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

The system allowed administrators to create forms from predefined templates, edit and duplicate individual fields inline, reorder fields using drag-and-drop, and manage form submissions without developer involvement.

Form submissions could be reviewed in a list, deleted individually or in bulk, and used for manual communication with submitters through their submitted email addresses.

A configurable data-retention system limited how long submissions could remain stored. Each form defined its own retention period in days. Form owners received email warnings before submissions reached their deletion deadline, allowing them to review the data before automatic deletion.

The system also included a data-export workflow supporting CSV and XLS exports. Submissions containing uploaded files could be exported as ZIP packages containing the spreadsheet and associated files.

Uploaded files were stored in AWS S3. During file-inclusive exports, S3 objects were streamed into the ZIP generation process without first creating complete local copies of the uploaded files or a persistent intermediate ZIP archive.

A significant export challenge was representing multiple uploaded files belonging to one submission. Because each spreadsheet cell could only provide one independent hyperlink for the required use case, submissions with multiple files were represented across multiple physical rows. The non-file columns were visually grouped to preserve the appearance of one logical submission.

The initial spreadsheet implementation used native cell merging, but extensive use of `mergeCells()` caused excessive memory consumption. The final implementation reproduced the visual grouping using hidden gridlines, borders, and cell styling instead.

---

# Context

Moava AS operated an administration system where customers could create and manage dynamic forms and questionnaires.

The form builder needed to be usable by non-technical administrators while supporting reusable templates, flexible field configuration, inline editing, duplication, and drag-and-drop ordering.

The system also collected potentially sensitive or personal information. This made data retention and deletion part of the application's core data lifecycle rather than a separate administrative process.

After developing questionnaire templates, customers also needed to export collected responses for further processing, analysis, consolidation, and sharing.

The export requirement became more complex because submissions could contain uploaded files. Customers needed the exported spreadsheet to maintain the relationship between each submission and its associated files while also receiving the actual uploaded files as part of the export package.

The resulting solution combined four related concerns:

- Dynamic form management.
- Submission management.
- GDPR-oriented data retention and deletion.
- Export of structured data and associated files.

---

# Task

The main task was to develop and integrate a dynamic form builder into the existing administration system.

The system needed to support:

- Creating forms from predefined templates.
- Editing individual fields inline.
- Duplicating fields.
- Reordering fields through drag-and-drop.
- Detecting unsaved field changes.
- Persisting form and field data through a PHP REST API.
- Exchanging form data as JSON.
- Listing form submissions.
- Deleting individual submissions.
- Deleting all submissions for a form.
- Contacting submitters manually using submitted email addresses.
- Configuring form-specific data-retention periods.
- Warning form owners before data reached its retention deadline.
- Automatically deleting expired submissions.
- Automatically deleting uploaded files associated with expired submissions.
- Exporting submission data to CSV.
- Exporting submission data to XLS.
- Including uploaded files in exports.
- Providing individual file hyperlinks in XLS exports.
- Packaging spreadsheets and uploaded files into ZIP archives.
- Streaming files from AWS S3 during export.
- Avoiding unnecessary temporary local storage.
- Maintaining acceptable memory usage for larger spreadsheet exports.

---

# Challenge

## Challenge: Dynamic Form Editing

### Problem

Administrators needed to modify form structures without directly editing database records or relying on developers.

Fields had to be individually editable and reorderable while remaining synchronized with the persisted form configuration.

### Solution

Each form field was stored as a separate database record and associated with its parent form through a form ID.

The frontend communicated with the PHP backend through a self-developed REST API using JSON.

Backbone.js represented the JSON data as client-side models and collections. This provided a structured way to manipulate individual fields and the overall form structure.

Administrators could:

- Select a form template.
- Edit individual fields inline.
- Duplicate fields.
- Reorder fields using drag-and-drop.
- Save field changes through the REST API.

### Result

The form structure could be maintained directly through the administration interface without requiring developers to modify the underlying configuration.

---

## Challenge: Unsaved Change Detection

### Problem

The editor allowed multiple fields to be modified independently. Administrators needed to know which fields had changed but had not yet been saved.

### Solution

The saved JSON representation of each field was retained as a reference state.

When a field was edited, its current JSON representation was compared with this stored "ghost" version.

If the current representation differed from the saved representation, the field was considered modified and received a faint red background.

### Result

Unsaved changes were immediately visible without interrupting the editing workflow.

---

## Challenge: GDPR Data Retention

### Problem

Form submissions could contain personal or sensitive information and could not simply be retained indefinitely.

Different forms could also have different legitimate retention requirements.

### Solution

Each form had a configurable retention period expressed as a number of days.

Each submission contained a date used to determine its retention deadline.

Before the deadline, the owner of the form received an email warning that the data was approaching deletion. This allowed the owner to review the information before it expired.

When the configured retention period was reached, the submission was deleted. Uploaded files associated with the submission followed the same retention period and were deleted as part of the same data lifecycle.

### Result

Data retention was built into the form and submission lifecycle rather than treated as an unrelated cleanup process.

This reduced the amount of personal data retained by the system and provided customers with a controlled mechanism for reviewing data before deletion.

---

## Challenge: Exporting Submissions with Multiple Files

### Problem

Customers needed to export collected form results to CSV and XLS for further processing and analysis.

Some submissions contained multiple uploaded files. The exported spreadsheet needed to preserve the relationship between each file and its originating submission.

The spreadsheet representation also imposed a limitation: one cell could not provide multiple independent file hyperlinks in the required form.

A submission containing three uploaded files therefore needed three physical spreadsheet rows if each file was to receive its own hyperlink.

### Solution

PHPSpreadsheet was used for XLS generation.

For submissions containing multiple files:

1. The first file used the original submission row.
2. Additional files generated additional spreadsheet rows.
3. Each file received its own hyperlink in the file column.
4. The remaining submission fields were visually grouped across the physical rows.
5. The result appeared to the user as one logical submission containing multiple file links.

This changed the spreadsheet representation rather than trying to force multiple independent hyperlinks into one cell.

The resulting XLS file was packaged together with the submitted files in a ZIP archive.

The spreadsheet contained relative links to the exported files so that the links remained usable after the ZIP archive was unpacked.

### Result

Customers could export submissions containing multiple attachments while retaining a clear relationship between each submission and its files.

The exported data could be processed, analyzed, consolidated, and shared outside the administration system.

---

## Challenge: Spreadsheet Export Performance

### Problem

The first implementation used PHPSpreadsheet's `mergeCells()` to vertically merge the non-file columns when one submission occupied multiple rows.

The approach produced the desired visual result but caused excessive memory consumption when many cells were merged.

This became problematic for larger exports containing many submissions and uploaded files.

### Solution

The implementation was changed so that actual spreadsheet cell merging was no longer required.

Gridlines were disabled with `setShowGridlines(false)` and the appearance of merged cells was reproduced using borders and other PHPSpreadsheet styling APIs such as `applyFromArray()`.

The spreadsheet therefore used independent cells structurally while making the relevant areas appear visually grouped to the user.

### Result

The export retained the intended visual layout without creating large numbers of native merged-cell ranges.

This reduced memory pressure and made the export more suitable for larger datasets.

---

## Challenge: Streaming Export Files from S3

### Problem

Exports could contain many and potentially large uploaded files.

Downloading every file from S3 to the application server before creating a ZIP archive would require temporary disk storage, additional I/O, and cleanup of both the downloaded files and the generated archive.

It would also introduce intermediate copies of data that did not need to exist for the actual export operation.

### Solution

Uploaded files were stored in AWS S3 and accessed through the AWS SDK for PHP.

During an export, the required S3 objects were streamed into `maennchen/zipstream-php`, which compressed them as part of the ZIP generation process.

The ZIP response was streamed directly to the customer instead of first creating a complete archive on the application server.

The effective data flow was:
`AWS S3` -> `AWS SDK for PHP` -> `ZipStream` -> `HTTP response` -> `browser`

This avoided the unnecessary intermediate flow:
`AWS S3` -> `local filesystem` -> `ZIP file` -> `browser`

### Result

The export process avoided unnecessary local copies of uploaded files and eliminated the need for a persistent intermediate ZIP archive.

The trade-off was that the HTTP request remained active while the archive was generated, making request duration and connection reliability relevant for very large exports.

---

# Action

## Architecture

### Frontend

The form builder frontend used:

- Backbone.js
- Underscore.js
- jQuery
- Bootstrap
- HTML
- CSS

Backbone.js represented form fields as client-side models and collections based on JSON received from the backend.

The frontend supported:

- Form template selection.
- Inline field editing.
- Field duplication.
- Drag-and-drop field ordering.
- Saved and unsaved field states.
- Visual indication of unsaved changes.
- Submission listing and management.

Backbone.js was also responsible for maintaining the client-side representation used during drag-and-drop and editing operations.

---

### Backend

The backend was implemented in PHP 8.1.

A self-developed REST API provided JSON-based communication between the frontend and backend.

The backend handled:

- Form and field persistence.
- Form and field retrieval.
- Submission management.
- Individual and bulk submission deletion.
- Data-retention processing.
- Export generation.
- File metadata and relationships.
- AWS S3 file access.
- CSV and XLS generation.

---

### Database

MySQL was used for structured application data.

Each form field was stored separately and associated with its parent form through a form ID.

This allowed individual fields to be updated, duplicated, and reordered without storing the complete form structure as one monolithic record.

Form submissions were associated with their forms and contained submission dates used by the retention mechanism.

Uploaded files were not stored as binary data in MySQL. The database maintained the relationship between submissions and their associated files while the actual files were stored in AWS S3.

---

### File Storage

Uploaded files were stored in AWS S3.

The AWS SDK for PHP was used to access the objects.

S3 storage separated binary file storage from structured application data while allowing files to remain associated with individual submissions.

Uploaded files followed the same retention lifecycle as the submission they belonged to.

---

### Submission Management

Administrators could view submitted results in a list.

They could:

- Delete individual submissions.
- Delete all submissions for a form.
- Manually contact submitters using the email address provided with the submission.
- Export collected submissions.
- Export associated uploaded files.

The system did not automatically send replies to submitters. Communication was performed manually using the submitted email address.

---

### Export Pipeline

The export pipeline combined structured submission data with uploaded files.

For CSV exports, the structured submission data was represented as tabular data.

For XLS exports, PHPSpreadsheet was used to generate the spreadsheet and provide formatting and hyperlinks.

For submissions containing multiple files, the XLS representation used multiple physical rows so that every file could have an individual hyperlink.

The general XLS process was:

1. Retrieve the selected submissions.
2. Generate the submission data.
3. Determine files associated with each submission.
4. Create the spreadsheet row for the submission.
5. Add additional rows for additional uploaded files.
6. Place one file hyperlink in the file column of each relevant row.
7. Visually group the remaining submission columns.
8. Disable gridlines and use styling instead of native cell merging where required for performance.
9. Generate the spreadsheet.
10. Retrieve associated files from AWS S3.
11. Stream the S3 objects into the ZIP generation process.
12. Add the spreadsheet and submitted files to the ZIP archive.
13. Stream the resulting ZIP archive to the customer's browser.

The file packaging flow was:
`AWS S3` -> `AWS SDK for PHP` -> `ZipStream` -> `HTTP response` -> `Customer browser`

No complete copy of every S3 object needed to be stored on the application server before ZIP generation.

---

## Technical Decisions

## Decision: Backbone.js for Dynamic Form Management

### Context

The form builder needed to manage dynamic collections of fields that could be edited, duplicated, reordered, and persisted.

### Chosen Solution

Backbone.js models and collections were used to represent the form and its fields on the client.

The JSON returned by the REST API could be mapped into Backbone's model structure and manipulated before changes were sent back to the backend.

### Alternatives Considered

A simpler jQuery-based implementation could have manipulated DOM elements and JSON objects directly.

### Trade-offs

Backbone.js introduced additional client-side structure but provided a consistent model for dynamic form data and UI state.

For this type of editor, the model layer made operations such as reordering, duplication, and change detection easier to manage than relying exclusively on DOM state.

---

## Decision: JSON REST API

### Context

The frontend needed to exchange dynamic form configuration with the PHP backend.

### Chosen Solution

A self-developed REST API was used to exchange JSON between Backbone.js and PHP.

### Alternatives Considered

The editor could have used traditional server-rendered HTML forms and full-page requests.

### Trade-offs

The REST/JSON approach required more client-side implementation but enabled a more interactive editing experience and established a clear data contract between frontend and backend.

---

## Decision: Per-Form Retention Period

### Context

Different forms could have different requirements for how long their submissions should be retained.

### Chosen Solution

Each form defined its own retention period in days.

The submission date was evaluated against the configured period to determine when a submission should be reviewed and deleted.

### Alternatives Considered

A single global retention period could have been applied to all forms.

### Trade-offs

Per-form configuration added lifecycle logic but allowed retention to be defined according to the requirements of each individual form.

---

## Decision: AWS S3 for Uploaded Files

### Context

Submissions could contain uploaded files that should not be stored directly in the relational database.

### Chosen Solution

Files were stored in AWS S3 while the application maintained their relationship to the corresponding submission.

The AWS SDK for PHP provided access to the stored objects.

### Alternatives Considered

Files could have been stored on the application server filesystem or as binary data in MySQL.

### Trade-offs

S3 separated binary storage from relational application data and avoided storing potentially large files in MySQL.

It also introduced an external storage dependency and required the application to manage S3 objects as part of submission creation, export, and deletion.

---

## Decision: PHPSpreadsheet for XLS Export

### Context

Customers needed spreadsheet exports that could be opened and processed using common spreadsheet applications.

The export also needed to support multiple uploaded files per submission.

### Chosen Solution

PHPSpreadsheet was used to generate XLS files with formatting and hyperlinks.

Submissions containing multiple files were represented across multiple physical rows, with one hyperlink per file.

### Trade-offs

PHPSpreadsheet provided the required spreadsheet generation and formatting capabilities.

However, native cell merging created excessive memory usage at scale, which led to the separate styling-based solution described below.

---

## Decision: Visual Merge Instead of Native `mergeCells()`

### Context

Multiple files belonging to one submission required multiple physical spreadsheet rows.

The non-file fields needed to appear visually as one logical result.

### Chosen Solution

The first implementation used `mergeCells()` but was replaced after memory profiling showed excessive consumption.

Gridlines were disabled and borders and styles were used to create the visual appearance of merged cells without creating native merged ranges.

### Alternatives Considered

Continue using `mergeCells()` for every additional file row.

### Trade-offs

The styling-based solution did not create actual merged spreadsheet cells, so the structure differed from a true merged-cell representation.

However, the customer primarily needed the visual presentation. Reproducing that appearance through styling avoided unnecessary memory overhead and provided better scalability.

---

## Decision: Streaming ZIP Generation

### Context

The export process needed to package files stored in S3 without unnecessarily consuming local disk space or creating an intermediate archive.

Exports could contain many and potentially large uploaded files, making local temporary storage undesirable.

### Chosen Solution

`maennchen/zipstream-php` was used together with the AWS SDK for PHP to stream S3 objects directly into the ZIP response.

The spreadsheet and uploaded files could therefore be added to the ZIP as part of a streaming export process without first downloading all uploaded files to local storage or creating a complete intermediate ZIP archive.

### Alternatives Considered

A conventional temporary-file approach would:

1. Download S3 files to local disk.
2. Create a ZIP archive from the downloaded files.
3. Serve the completed archive.
4. Delete the temporary files and archive.

### Trade-offs

Streaming reduced local disk usage and avoided persistent intermediate files.

The trade-off was that the HTTP request remained active while the archive was generated, making request duration and connection reliability relevant for very large exports.

For extremely large exports, a background-job architecture would be more appropriate because it would remove the dependency on a single long-running HTTP request.

---

# Implementation

The system followed a client-server architecture connecting a Backbone.js frontend, a PHP backend, MySQL, and AWS S3.

The main application flow was:

1. An administrator selected a predefined form template.
2. The frontend loaded the form configuration through the REST API, with PHP returning the data as JSON.
3. Backbone.js represented forms and fields as client-side models and collections.
4. Administrators could edit, duplicate, and reorder fields through the administration interface.
5. Saved field state was retained so that unsaved changes could be detected and shown visually.
6. Changes were persisted through the REST API, with individual form fields stored as separate MySQL records.
7. Form submissions were associated with their forms and managed through the administration interface.
8. Each form's configured retention period was used to determine when submissions and their associated uploaded files should be deleted.
9. Submission data could be exported as CSV or XLS.
10. PHPSpreadsheet generated XLS exports, including formatting and individual hyperlinks for uploaded files.
11. Submissions containing multiple files were represented across multiple spreadsheet rows, with styling used to visually group the fields belonging to the same submission.
12. For file-inclusive exports, the required S3 objects were accessed through the AWS SDK for PHP.
13. `maennchen/zipstream-php` added the spreadsheet and S3-backed files to a ZIP archive without requiring the uploaded files to be fully materialized on the application server first.
14. The ZIP response was streamed directly to the customer's browser.
15. Relative file links in the spreadsheet pointed to the corresponding files within the exported ZIP package.

---

# Result

The resulting system provided a flexible administration interface for creating and managing dynamic forms.

Administrators could create forms from templates, edit and duplicate fields, reorder fields using drag-and-drop, and identify unsaved changes without developer involvement.

The submission workflow supported list-based review, individual and bulk deletion, manual communication with submitters, and export of collected data.

The GDPR-oriented retention mechanism allowed each form to define its own data-retention period. Form owners received email warnings before submissions reached their deletion deadline, and both submission data and associated uploaded files were deleted after the configured retention period.

The export functionality supported CSV and XLS formats and allowed customers to take collected data outside the administration system for further processing, analysis, consolidation, and sharing.

For submissions with uploaded files, the export produced a ZIP package containing the XLS spreadsheet and corresponding files. Each uploaded file received its own hyperlink in the spreadsheet.

Multiple files belonging to one submission were represented using multiple spreadsheet rows. The other fields were visually grouped so that the exported data still appeared as one logical submission.

The initial use of `mergeCells()` caused excessive memory consumption for larger exports. Replacing native cell merging with gridline removal and styling-based visual grouping reduced the memory overhead while preserving the intended user-facing layout.

Uploaded files were stored in AWS S3 and streamed directly into ZIP generation. This avoided unnecessary local copies of the files and removed the need to create a complete intermediate ZIP archive on the application server.

The resulting architecture combined dynamic form management, submission handling, configurable data retention, cloud-based file storage, spreadsheet generation, and streaming export into a single administration workflow.

---

# Lessons Learned

## Dynamic UI State Should Be Explicit

Backbone.js provided a useful model layer for an interface where fields could be edited, duplicated, reordered, and persisted independently.

The same model representation also made it possible to compare current and saved JSON state to identify unsaved changes.

## Visual Feedback Can Prevent Data Loss

The faint red background used for modified fields provided a low-friction way to communicate unsaved state.

For editors with multiple independently editable objects, explicit visual state is preferable to relying on users to remember which objects they changed.

## GDPR Affects the Entire Data Lifecycle

Retention cannot be treated as a single database cleanup operation.

In this system, the retention policy affected:

- Form configuration.
- Submission dates.
- Warning notifications.
- Submission deletion.
- Uploaded file deletion.
- Export workflows.
- Data processing responsibilities.

The submission and its associated files therefore needed to be treated as one logical data lifecycle even though they were stored in different systems.

## Spreadsheet Constraints Can Require a Different Data Representation

The requirement for multiple file hyperlinks exposed a limitation of the target spreadsheet representation.

Instead of trying to force multiple links into one cell, the export represented one logical submission across multiple physical rows.

This preserved the required relationships while remaining compatible with the spreadsheet format.

## Optimize for the Actual Requirement

Native `mergeCells()` was technically capable of producing the desired spreadsheet layout, but it introduced unnecessary memory overhead.

The customer did not require the cells to be structurally merged. They required them to appear grouped.

Replacing structural merging with styling was therefore a better solution because it implemented the actual user-facing requirement with lower resource usage.

## Streaming Can Remove Unnecessary Intermediate Storage

The S3-to-ZIP streaming architecture avoided downloading every uploaded file to the application server before compression.

The general principle is applicable beyond this specific system:
`Remote object storage` -> `streaming transformation` -> `HTTP response`

can be preferable to:
`Remote object storage` -> `local temporary files` -> `generated artifact` -> `HTTP response`

when the transformation can be performed incrementally.

## Export Is Part of the Data Lifecycle

The export feature was not only a reporting convenience.

It allowed customers to obtain the collected information and continue processing it outside the administration system while the original stored data could be removed according to the configured retention policy.

This made export part of the overall privacy and data-management strategy rather than an isolated feature.

---

# Future Improvements

- Replace the legacy Backbone.js/jQuery frontend with a modern component-based architecture.
- Move very large export generation to background jobs instead of keeping a single HTTP request open for the entire operation.
- Add progress reporting for long-running exports.
- Profile PHPSpreadsheet memory usage across different dataset sizes and optimize generation further.
- Consider more modern spreadsheet-generation approaches if future export requirements exceed the capabilities or performance characteristics of PHPSpreadsheet.
- Make export and retention processing observable through structured logging and operational metrics.

----