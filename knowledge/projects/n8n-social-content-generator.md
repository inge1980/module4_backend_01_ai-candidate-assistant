---
title: n8n Social Content Generator

organization: Personal Project

role: Automation Developer

environment: development

period:
  from: 2025-04
  to: 2025-05

status: completed

technologies:
  - n8n
  - Google Gemini
  - Google Sheets
  - Google Drive
  - Pexels API
  - JavaScript
  - HTTP API
  - structured JSON

concepts:
  - workflow-automation
  - LLM-content-generation
  - human-in-the-loop
  - duplicate-detection
  - structured-output
  - image-selection
  - image-processing
  - cloud-file-storage
  - spreadsheet-as-interface
  - scheduled-processing
  - content-pipeline

dependencies:
  - n8n
  - Google Sheets
  - Google Drive
  - Google Gemini API
  - Pexels API

links:
  github:
  live:

---

# Overview

This project is an n8n-based automated social media content generation pipeline for a family planning and family organization themed social media channel.

The workflow uses Google Gemini to generate evergreen content ideas, descriptions, captions, and image-search keywords. It uses an existing Google Sheet as the content database and human-facing interface, Pexels as the source for stock photography, and Google Drive as the generated image storage location.

The system was designed around a human-in-the-loop workflow: automated content ideas are generated and stored in a spreadsheet, while content can be reviewed and controlled through spreadsheet status values before downstream image creation and publishing.

The generated content is intended to provide useful family-oriented advice while indirectly building awareness and perceived value around an upcoming family planner application.

---

# Context

The project originated from hobby experimentation with n8n and cloud productivity tools. The goal was to automate repetitive social media content production without removing human control over which ideas should ultimately become published content.

Google Sheets was used as the visual interface and lightweight content database. This allowed content to be inspected and controlled without requiring a dedicated administration application.

The intended social media theme covers family planning, parenting, household organization, time management, digital parenting, gamified chores, kid responsibility, household teamwork, allowance and money literacy, family technology, shopping and meal planning, seasonal planning, relatable family chaos, progress tracking, and family milestones.

The content pipeline also needed to avoid repeatedly generating the same subjects and, where possible, avoid reusing previously selected stock images.

The workflow had several external dependencies: Google Sheets for persistence and review, Google Drive for generated image storage, Google Gemini for LLM generation and image selection, and Pexels for stock photography.

---

# Task

The responsibility in this project was to design and implement an automated content-generation workflow in n8n.

The main responsibilities included:

- Designing the content generation workflow.
- Connecting n8n to Google Sheets as the content database and human-facing interface.
- Providing existing subjects and image IDs to the LLM to reduce duplication.
- Generating structured content using Google Gemini.
- Searching Pexels for relevant stock imagery.
- Using an LLM to select the most appropriate image from the search results.
- Downloading the selected image as binary data.
- Adding generated text to the image.
- Uploading the generated image to a designated Google Drive folder.
- Combining generated content and image metadata.
- Writing the completed content record back to Google Sheets.
- Supporting both scheduled and manual workflow execution.

The workflow was designed as an automation pipeline rather than a standalone application. Google Sheets acted as the primary human-readable control surface.

---

# Challenge

## Challenge: Generating Structured Content Reliably

### Problem

The workflow needed an LLM to generate multiple content records while maintaining a strict structure.

Each generated record needed a short subject, image caption, short content, long content, and image-search keywords. The generated subjects also needed to be distinct from subjects already stored in the spreadsheet.

Free-form LLM output creates a reliability problem because downstream n8n nodes expect predictable JSON fields. Invalid JSON, markdown code fences, missing properties, or unexpected structures could break subsequent workflow steps.

The content also had explicit constraints such as maximum subject length, maximum content lengths, evergreen topics, and restrictions on image keywords.

### Solution

The workflow used a Google Gemini chat model through n8n's LangChain nodes.

The system prompt explicitly defined:

- The social media theme.
- The target audience.
- The application capabilities.
- The desired brand/content direction.
- Required output properties.
- Maximum content lengths.
- Image keyword requirements.
- Existing subjects that should be avoided.

The AI Agent was instructed to return an array containing two distinct objects.

The workflow then used a JavaScript Code node named `Split out` to extract the LLM output:

    const raw = $input.first().json.output;
    const jsonStr = raw.replace(/```json|```/g, '').trim();
    return JSON.parse(jsonStr);

This converted the generated string into actual n8n items that could be processed individually.

The workflow therefore separated content generation from downstream image processing. Each generated content object became an item entering the processing loop.

### Result

The workflow could transform a single LLM generation request into multiple structured content records suitable for automated downstream processing.

The main remaining reliability weakness is that JSON parsing depends on the LLM returning valid JSON after removing optional markdown fences. A malformed response can still fail the workflow.

---

## Challenge: Avoiding Duplicate Content Ideas

### Problem

Automated content generation can quickly become repetitive. The workflow needed to know which subjects had already been generated so that new LLM requests could avoid obvious duplicates.

The spreadsheet already contained previously generated subjects and image IDs, making it possible to use historical content as input to the generation process.

### Solution

The workflow first retrieves existing spreadsheet records through the `Get existing ideas` Google Sheets node.

A merge step combines that data with an empty-array fallback generated by the `Create empty array` node. This allows the workflow to continue even when there are no existing records.

The `Merge in` JavaScript node then transforms the spreadsheet records into two arrays:

    const subjects = items ? items.map(item => item.json.Subject?.trim()) : [];
    const images = items ? items.map(item => Number(item.json.ImageID)) : [];

    return [{
      json: {
        existingSubjects: subjects,
        existingImages: images
      }
    }];

These arrays are injected into the Gemini prompt.

The content-generation prompt receives the existing subjects and explicitly instructs the model not to generate those subjects again.

The image-selection prompt similarly receives existing image IDs and instructs the model to avoid them.

### Result

Existing spreadsheet history becomes contextual input to the generation process rather than merely passive storage.

This reduces obvious subject and image reuse without requiring a separate duplicate-detection database.

The approach is primarily LLM-based rather than deterministic. It therefore reduces duplication but does not guarantee semantic uniqueness.

---

## Challenge: Selecting Relevant Stock Images

### Problem

Finding an image that merely matches a keyword is not sufficient for social media content.

For example, content about a family meeting should visually represent a family meeting rather than a generic business meeting. Content about technology should contain technology when technology is central to the topic.

The Pexels API can return multiple plausible images, so the workflow needed another selection step.

### Solution

The generated `imageKeywords` are sent to the Pexels search API.

The request uses:

- Pexels search.
- Portrait orientation.
- Page 1.
- Up to 10 results.

The workflow then passes the returned image metadata to a Google Gemini LLM chain.

The LLM receives the generated `contentShort` together with the available Pexels image IDs, descriptions, URLs, and photographer metadata.

It is explicitly instructed to select the image that best matches the post topic and tone.

The prompt also contains semantic constraints such as not using office meeting imagery for family meetings.

A structured output parser defines the expected selected image and photographer metadata.

### Result

Image selection is based on both the original stock search keywords and semantic comparison between the generated post and the returned image descriptions.

This creates a two-stage image-selection process:

1. Pexels performs broad candidate retrieval.
2. Gemini performs semantic selection among the candidates.

---

## Challenge: Preserving Image Attribution Metadata

### Problem

The generated spreadsheet record needs more than the final image file.

The workflow also needs to retain information about the original stock image and its photographer, allowing the content record to preserve the source metadata.

The spreadsheet therefore contains separate fields for the selected image ID, original image URL, photographer ID, photographer name, and photographer URL.

### Solution

The LLM structured output contains both:

- `image`
- `photographer`

The `Pexel image info` Set node stores those values as:

- `selected.image`
- `selected.photographer`

The final Google Sheets mapping extracts the selected image ID, original URL, photographer ID, photographer name, and photographer URL.

The selected image itself is downloaded separately and processed before being uploaded to Google Drive.

### Result

The spreadsheet acts as both a content database and an attribution record, preserving the relationship between the generated social media asset and the original stock image source.

---

## Challenge: Turning Stock Images Into Finished Social Assets

### Problem

The stock image returned by Pexels is not the final social media asset.

The generated image needs to contain the generated subject and short image caption as text over the image.

The workflow also needed to create a persistent file that could be accessed later from Google Drive.

### Solution

The selected Pexels image is downloaded through an HTTP Request node configured to return binary data.

The `Add caption` image-processing node then performs multiple text operations over the image.

The workflow draws the `contentImage` text multiple times with small positional offsets before placing the final colored text layer on top. The same technique is used for the subject near the top of the image.

This creates an outlined/shadow-like text treatment without requiring a dedicated graphics application.

The resulting image is encoded as JPEG and named using the generated subject.

The processed binary image is then uploaded to a designated Google Drive folder.

### Result

The workflow transforms a remotely hosted stock photograph into a finished social-media-oriented image asset and stores it in Google Drive.

The Google Drive upload result provides a web-view link that is subsequently written into the spreadsheet.

---

## Challenge: Combining Independent Image Processing Results

### Problem

The image-selection and image-processing branches produce different pieces of information.

One branch contains the selected Pexels image and photographer metadata. Another branch contains the Google Drive upload result.

The final spreadsheet row requires information from both branches.

### Solution

The workflow uses the `Merge arrays` node to combine the output from:

- `Clean memory`, containing the Google Drive link.
- `Pexel image info`, containing the selected image and photographer metadata.

The resulting item is passed to `Add to Sheet`.

The Google Sheets node maps both the generated content and image metadata into the spreadsheet schema.

### Result

The workflow can independently process the image and retain its metadata before combining the results into a single content record.

This keeps the image-selection metadata available even after the binary image has been removed from the intermediate workflow data.

---

# Action

## Architecture

### Frontend

There is no conventional frontend application.

Google Sheets functions as the human-facing interface and lightweight content-management system.

The spreadsheet contains the generated subject, creation status, posting status, image metadata, image URL, image attribution, keywords, and generated content.

A human can therefore inspect generated ideas and control the content lifecycle through spreadsheet values rather than interacting directly with n8n.

The spreadsheet also provides a persistent visual overview of the content pipeline.

### Backend

n8n acts as the workflow orchestration layer.

The workflow coordinates:

- Scheduled execution.
- Manual execution.
- Google Sheets reads and writes.
- LLM content generation.
- JSON parsing.
- Pexels API requests.
- LLM image selection.
- Binary image downloading.
- Image manipulation.
- Google Drive uploads.
- Metadata merging.

Google Gemini provides the language-model functionality.

Pexels provides stock-image search.

Google Drive provides generated asset storage.

Google Sheets provides persistence and human review.

### Database

Google Sheets is used as the project's lightweight database.

Each row represents a content item.

The observed schema contains:

- `Subject`
- `Creation Status`
- `Posting Status`
- `ImageID`
- `ImageURL`
- `ImageOriginal`
- `ImageOwnerID`
- `ImageOwnerName`
- `ImageOwnerURL`
- `Keywords`
- `ContentImage`
- `ContentShort`
- `ContentLong`

The `ImageID` and `Subject` fields are also used as historical information when generating new content, allowing the workflow to provide existing subjects and image IDs to the LLM.

The spreadsheet therefore serves three purposes:

1. Content storage.
2. Human review interface.
3. Historical context for duplicate avoidance.

### File Storage

Generated images are stored in Google Drive.

The workflow uploads processed JPEG images to a configured Google Drive folder.

The resulting Drive web-view link is stored in the spreadsheet as `ImageURL`.

The original Pexels image URL is stored separately as `ImageOriginal`.

This separates the generated asset from the source stock image.

### Infrastructure

The automation runs inside n8n.

The workflow supports both scheduled and manual execution.

A schedule trigger is configured to execute at hour 10. A manual trigger is also available for testing or manual runs.

External services used by the workflow include Google Sheets, Google Drive, Google Gemini, and Pexels.

No dedicated backend server, application database, container infrastructure, or CI/CD pipeline is evidenced in the provided workflow export.

---

## Technical Decisions

### Decision: Use Google Sheets as the Content Interface

#### Context

The project needed a simple way to inspect generated content and retain human control without building a custom administration interface.

The content workflow was being developed as a hobby automation project, making a full CMS or admin application unnecessary overhead.

#### Chosen Solution

Google Sheets was used as both persistent storage and the human-facing content interface.

The workflow appends generated records directly into the sheet.

The schema includes separate fields for content, lifecycle status, image metadata, and attribution.

This also makes the content state visible without opening n8n.

#### Alternatives Considered

No alternative is explicitly documented in the provided workflow.

#### Trade-offs

The main advantage is simplicity and visibility.

The main disadvantage is that Google Sheets is not a robust database. Concurrent writes, schema changes, data validation, permissions, and large datasets can become problematic.

Using the spreadsheet as workflow state also couples the automation to a human-editable data structure.

---

### Decision: Use an LLM for Image Selection

#### Context

Pexels returns multiple candidate images, but keyword matching alone cannot reliably determine whether an image actually represents the concept described by the generated content.

#### Chosen Solution

The workflow first performs a Pexels search using generated image keywords and then passes the returned candidates to Gemini.

Gemini receives the post content and candidate image descriptions and must select one image.

This allows semantic matching to happen after candidate retrieval.

#### Alternatives Considered

No alternative image-ranking implementation is explicitly documented in the provided workflow.

#### Trade-offs

The approach is flexible and requires little custom image-ranking logic.

However, LLM selection is probabilistic and adds another model invocation and therefore additional latency and API usage.

The selection also depends heavily on the quality of Pexels' returned image descriptions and the model's interpretation of them.

---

### Decision: Use Structured LLM Output

#### Context

Downstream workflow nodes need predictable fields from the image-selection model.

Free-form responses would make the workflow fragile.

#### Chosen Solution

The Basic LLM Chain uses an n8n structured output parser.

The expected output contains an image object and photographer object with IDs, descriptions, and URLs.

The parser provides a defined interface between the LLM and the rest of the workflow.

#### Alternatives Considered

No alternative structured-output mechanism is explicitly documented.

#### Trade-offs

Structured output makes downstream processing substantially easier.

However, schema validation does not guarantee semantic correctness. The model can still select an inappropriate image while returning syntactically valid data.

---

### Decision: Keep Human Review Outside the LLM Loop

#### Context

Fully automatic publishing of generated content was not desirable. The content should be reviewable before becoming part of the publishing workflow.

The existing spreadsheet already provided a convenient interface for human decisions.

#### Chosen Solution

Google Sheets contains lifecycle fields including:

- `Creation Status`
- `Posting Status`

The content pipeline writes generated content into the sheet while keeping posting state separate.

This allows generated content to exist independently from publishing.

#### Alternatives Considered

No dedicated approval UI or moderation system is present in the supplied workflow.

#### Trade-offs

The spreadsheet provides a cheap and understandable approval mechanism.

However, the supplied workflow does not demonstrate a complete approval-driven downstream publishing pipeline. The `Posting Status` field exists in the data model, but the provided workflow primarily generates and stores content rather than publishing it automatically.

---

## Implementation

### Features

- Scheduled content generation.
- Manual workflow execution.
- Existing-content retrieval.
- Duplicate-subject avoidance through LLM context.
- Duplicate-image avoidance through LLM context.
- Generation of multiple content ideas per run.
- Structured content generation.
- Pexels stock-image search.
- Semantic image selection using Gemini.
- Photographer attribution storage.
- Binary image download.
- Automated image captioning.
- JPEG generation.
- Google Drive asset storage.
- Google Sheets content storage.
- Separate creation and posting statuses.
- Human review through spreadsheet data.

### APIs

The workflow integrates with the Pexels REST API using:

    GET https://api.pexels.com/v1/search

The request sends the generated image keywords as the search query and requests portrait-oriented images.

The workflow also consumes the Google Gemini API through n8n's Google Gemini integration.

Google Sheets and Google Drive are accessed through their respective n8n nodes rather than manually implemented REST calls.

### Data and Persistence

A generated content record is persisted as one row in Google Sheets.

The record contains both generated text and image metadata.

A typical lifecycle is:

1. Generate subject and content.
2. Generate image keywords.
3. Search Pexels.
4. Select an image.
5. Download the selected image.
6. Add generated text to the image.
7. Upload the resulting image to Google Drive.
8. Obtain the Google Drive web-view link.
9. Combine image metadata and Drive information.
10. Append the final record to Google Sheets.

The observed spreadsheet uses `Created` as the generated creation status and `To Do` as the initial posting status.

### Automation

The workflow has two execution entry points.

The `Schedule Trigger1` node runs the workflow automatically at the configured hour.

The `When clicking ?Test workflow?` manual trigger allows the workflow to be executed manually.

Both execution paths retrieve existing spreadsheet content and construct the historical subject/image arrays used by the LLM.

The content-generation stage produces multiple ideas and passes them into `Loop Over Items` for individual processing.

Each item then runs through the Pexels search, image-selection, image-download, image-editing, Drive-upload, metadata merge, and spreadsheet-write stages.

### Testing

The workflow includes a manual trigger named `When clicking ?Test workflow?`, indicating that manual execution was available during development.

No automated unit-test, integration-test, end-to-end-test, or performance-test suite is evidenced by the provided n8n export.

The workflow's structure also contains explicit fallback handling for the case where there are no existing spreadsheet records, through the `Create empty array` node.

---

# Result

The project produced an automated content-production pipeline capable of generating social media ideas, finding and selecting matching stock imagery, creating captioned image assets, storing those assets in Google Drive, and recording the resulting content and attribution metadata in Google Sheets.

The resulting spreadsheet provides a human-readable content library containing both content and asset information.

The automation removes a significant amount of repetitive manual work from the content-production process while retaining human control over the content lifecycle.

The provided workflow does not contain evidence of production publishing, audience growth, engagement metrics, time savings, or other quantified business results. Those outcomes should not be claimed without additional project data.

---

# Lessons Learned

## Lesson: A Spreadsheet Can Be a Useful Lightweight CMS

Google Sheets can work surprisingly well as an interface for an automation when the workflow is small and the data is naturally tabular.

The combination of generated content, status fields, image metadata, and source attribution makes the sheet useful for both humans and automation.

The limitation becomes apparent as workflow state and business logic become more complex. At that point, a real database and dedicated interface would provide stronger consistency and validation.

---

## Lesson: LLMs Should Not Be Treated as Deterministic Functions

The workflow relies heavily on prompts to enforce uniqueness, formatting, keyword quality, and semantic image selection.

That works reasonably well for experimentation, but prompt instructions are not hard guarantees.

A production version should validate important constraints programmatically instead of relying exclusively on the model.

Examples include:

- Checking subject length.
- Checking required fields.
- Checking duplicate subjects.
- Checking duplicate image IDs.
- Validating keyword count.
- Validating generated content length.
- Rejecting malformed or incomplete records.
- Retrying failed model responses.

---

## Lesson: Separate Candidate Retrieval From Semantic Selection

Using Pexels to retrieve candidate images and Gemini to select the best candidate creates a useful division of responsibilities.

The search API handles broad retrieval while the LLM handles contextual interpretation.

This pattern is generally more flexible than asking the LLM to invent an image URL or attempting to solve the entire image-selection problem through keyword search alone.

---

## Lesson: Preserve Source Metadata

The workflow deliberately stores the original image ID, image URL, and photographer information alongside the generated asset.

This is important because the generated file and its source asset are different entities.

Keeping that relationship in the content database makes attribution and later asset auditing much easier.

---

## Lesson: Human-in-the-Loop Is Valuable for Generated Content

The spreadsheet-based approval concept is a useful middle ground between completely manual content creation and completely autonomous publishing.

Generated ideas can be inspected before expensive downstream processing or publishing occurs.

A stronger implementation would move the approval decision earlier in the pipeline so that rejected ideas do not consume unnecessary image-search, LLM, image-processing, or storage resources.

---

# Future Improvements

- Move approval/rejection to an explicit workflow gate before image generation and asset processing.
- Replace LLM-only duplicate avoidance with deterministic subject and image-ID validation.
- Add programmatic schema and content-length validation before downstream processing.
- Add retry and fallback behavior for Gemini, Pexels, Google Drive, and Google Sheets failures.
- Remove API credentials from workflow configuration and rotate any credentials that have been exposed in exported workflow data.
- Replace hard-coded image text positioning with dynamic layout calculations based on actual image dimensions and text width.
- Improve typography and text contrast using a proper text-shadow or stroke implementation rather than multiple offset text layers.
- Add a deterministic content ID for each generated post instead of relying primarily on the subject as an identifier.
- Store workflow execution/error state separately from the human-facing creation and posting statuses.
- Add an explicit content approval status rather than overloading general creation/posting state.
- Add deterministic validation for Pexels image IDs before accepting an image selection.
- Add fallback image candidates when the selected image cannot be downloaded or processed.
- Add automated tests for JavaScript transformation nodes and important data mappings.
- Add observability around LLM failures, API failures, image-processing failures, and spreadsheet write failures.
- Move from Google Sheets to a proper database if the content library grows substantially or concurrent processing becomes necessary.
- Add a dedicated publishing stage only after human approval is confirmed.
- Consider replacing the current two-step LLM image-selection process with embeddings or deterministic ranking if image volume grows enough to justify lower-cost, more predictable retrieval.
- Use current n8n credential management rather than embedding service credentials directly in workflow exports.
- Upgrade the content generation model and structured-output approach as needed instead of depending on free-form JSON cleanup with regular expressions.
- Make the number of generated ideas configurable rather than hard-coding the generation request to two objects.
- Introduce explicit content categories so the system can balance topics across family organization, parenting, technology, chores, meals, finances, and other themes instead of relying entirely on the LLM's interpretation of the overall theme.

---