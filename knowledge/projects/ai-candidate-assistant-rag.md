---
title: AI Candidate Assistant with RAG Knowledge Base

organization: Personal Project

role: Fullstack Developer

environment: development

period:
  from: 2026-08
  to: 2026-08

status: active

technologies:
  - dotnet
  - csharp
  - aspnet-core
  - react
  - typescript
  - postgresql
  - pgvector
  - docker
  - ollama
  - bge-small-en-v1.5
  - github

concepts:
  - artificial-intelligence
  - llm
  - rag
  - document-ingestion
  - text-chunking
  - metadata-extraction
  - embeddings
  - semantic-search
  - vector-database
  - knowledge-management
  - api-design
  - backend-architecture

dependencies:

links:
  github:
  live:

---

# Overview

An AI-powered candidate assistant that uses Retrieval-Augmented Generation (RAG) to match job descriptions and candidate-oriented questions against a structured knowledge base of personal project experience.

The project documentation is maintained as Markdown with YAML frontmatter containing structured information about each project, including role, technologies, concepts, organization, period, and status.

The system ingests these documents, extracts their metadata, divides the content into semantic sections, generates embeddings, and stores the resulting retrieval representation in PostgreSQL using pgvector.

The current implementation focuses on building and validating the ingestion and semantic retrieval foundation. The next stage is to connect the retrieved project knowledge to an LLM and use that context to generate grounded candidate responses and job-matching results.

The intended workflow is:

1. A user provides a job description or candidate-oriented question.
2. The backend generates an embedding for the query.
3. PostgreSQL performs semantic similarity search against the indexed project knowledge.
4. Relevant project sections and their metadata are retrieved.
5. The retrieved information is supplied to an LLM.
6. The LLM generates a response grounded in the candidate's documented project experience.

The Markdown knowledge base remains the source of truth, while PostgreSQL acts as a generated retrieval index.

---

# Context

A general-purpose LLM does not reliably know the details of a candidate's personal project history.

Using an LLM without a controlled knowledge source creates a risk of producing plausible but unsupported claims about technologies, responsibilities, architectural decisions, or project outcomes.

The project therefore uses a version-controlled knowledge base containing structured documentation of completed and ongoing projects.

The knowledge base is intentionally maintained as Markdown rather than being authored directly in the database. This keeps the information human-readable, reviewable, and version controlled.

The system separates four concerns:

- Human-maintained project knowledge.
- Generated retrieval data.
- Runtime semantic search.
- Future LLM response generation.

The project is also intended to demonstrate a practical RAG architecture rather than simply calling an LLM with a large static prompt.

---

# Task

The current task is to build and validate the knowledge ingestion and semantic retrieval foundation for an AI-powered candidate assistant.

My responsibilities include:

- Designing the Markdown-based knowledge representation.
- Defining structured project metadata through YAML frontmatter.
- Building recursive Markdown document discovery.
- Excluding template documents from ingestion.
- Parsing YAML frontmatter.
- Converting Markdown documents into structured document objects.
- Splitting documents into semantic sections.
- Propagating project metadata to generated chunks.
- Validating generated chunks.
- Generating 384-dimensional embeddings locally.
- Persisting chunks, metadata, and vectors in PostgreSQL.
- Integrating pgvector for similarity search.
- Generating embeddings for user queries.
- Retrieving the most semantically relevant project sections.
- Inspecting retrieval results and similarity scores.
- Evaluating whether semantic retrieval is sufficient for candidate-oriented questions.
- Evaluating whether retrieved evidence supports the claims made in generated answers.
- Designing answer-generation instructions that prevent unsupported claims, including distinguishing technology usage from explicitly supported production experience.
- Identifying where metadata-aware retrieval can improve the system.
- Designing the foundation for the later LLM-powered response-generation workflow.

The current implementation intentionally stops before full LLM generation so that the retrieval layer and evidence grounding can be evaluated independently.

---

# Challenge

## Challenge: Building a Reliable Personal Knowledge Retrieval Layer

### Problem

A RAG system is only useful if the retrieved information is relevant to the question and grounded in the underlying source material.

A general-purpose vector search can retrieve semantically similar text without necessarily retrieving the project that is most appropriate for a specific candidate question.

The system therefore needs to preserve enough structure from the original project documentation to make retrieval inspectable and eventually support more sophisticated ranking.

The main requirements were:

- Project documentation must have a predictable structure.
- Metadata must survive the ingestion pipeline.
- Retrieved chunks must retain their source project and section.
- Document and query embeddings must use the same embedding model.
- Retrieval must be easy to inspect and rebuild.
- The original Markdown documents must remain authoritative.
- The retrieval layer must be extensible toward metadata-aware and hybrid search.

### Solution

I designed the ingestion pipeline around structured Markdown documents with YAML frontmatter.

Each document is processed through a deterministic pipeline:

1. Discover Markdown files recursively.
2. Exclude template files.
3. Parse YAML frontmatter.
4. Separate metadata from Markdown content.
5. Split the content into sections based on Markdown headings.
6. Propagate project metadata to each generated chunk.
7. Validate the generated chunks.
8. Generate embeddings for each chunk.
9. Persist the chunks, metadata, and vectors in PostgreSQL.
10. Generate an embedding for each user query.
11. Perform vector similarity search using pgvector.
12. Return the highest-ranked project sections for inspection.

The Markdown repository remains the source of truth, while PostgreSQL contains a generated representation that can be discarded and rebuilt.

### Result

The ingestion and retrieval pipeline is operational.

The system can ingest the project knowledge base, generate embeddings, persist the resulting vectors, and retrieve semantically related project sections from natural-language questions.

The retrieval layer can be inspected independently from the future LLM generation layer, making it possible to evaluate retrieval quality before introducing generation into the workflow.

Initial manual retrieval tests have been performed using representative candidate-oriented questions. The retrieval output is inspected together with similarity scores, source documents, sections, content, and project metadata.

---

## Challenge: Preserving Project Metadata During Retrieval

### Problem

Project documentation contains important structured information that cannot safely be inferred from arbitrary text.

For example, a project may explicitly declare that a technology was implemented, while the same technology might merely be mentioned in a discussion of alternatives or future improvements.

The retrieval system therefore needs to preserve the project's declared metadata rather than relying exclusively on semantic interpretation of the prose.

### Solution

Project metadata is stored in YAML frontmatter.

The metadata includes information such as:

- Project title.
- Organization.
- Role.
- Period.
- Status.
- Technologies.
- Concepts.
- Links.

The ingestion pipeline attaches this metadata to every generated chunk originating from the project.

This makes the metadata available alongside the retrieved content and creates a foundation for future filtering and ranking.

### Result

Retrieved chunks retain their project-level context instead of becoming anonymous pieces of text.

The metadata can later be used for:

- Technology filtering.
- Role filtering.
- Organization filtering.
- Project status filtering.
- Project-level ranking.
- Retrieval explanations.
- Candidate-to-job matching.

---

## Challenge: Choosing a Chunking Strategy

### Problem

Whole-document embeddings are too coarse for precise retrieval.

A complete project document may contain an overview, several unrelated challenges, technical decisions, implementation details, results, and lessons learned.

Embedding the entire document as one vector could therefore cause a query about one technical problem to retrieve a large amount of unrelated project content.

At the same time, arbitrarily splitting the document by character count could break meaningful concepts across chunk boundaries.

### Solution

I chose Markdown headings as the initial chunk boundaries.

The project documentation already uses structured headings such as:

- Overview
- Context
- Task
- Challenge
- Result
- Technical Decisions
- Lessons Learned
- Future Improvements

These sections provide meaningful semantic boundaries and make the resulting chunks easy to inspect.

Each chunk retains:

- Source document.
- Section.
- Content.
- Project metadata.
- Embedding.

Very large sections are intentionally left as a known limitation for a later iteration.

### Result

The retrieval system produces understandable project sections rather than arbitrary fragments.

This makes retrieval results easier to inspect and debug while providing a reasonable initial semantic unit for embedding.

---

## Challenge: Choosing a Vector Storage Architecture

### Problem

The system needs semantic similarity search while also retaining structured project metadata.

Introducing a separate vector database would add another persistence technology to a relatively small personal knowledge base.

### Solution

PostgreSQL was selected as the primary persistence layer, with pgvector providing vector storage and similarity search.

The database stores the generated retrieval representation:

- Chunk content.
- Source document.
- Section.
- Project metadata.
- Embedding vector.

The original Markdown files remain outside the database as the authoritative knowledge source.

### Result

Structured metadata and vector data can be queried from the same database.

The retrieval index can also be rebuilt from the Markdown knowledge base without treating PostgreSQL as the authoritative source of project information.

---

## Challenge: Evaluating Retrieved Evidence

### Problem

Semantic similarity alone does not guarantee that the retrieved evidence is sufficient to answer a candidate-oriented question correctly.

A retrieved section can be semantically related to a question while still failing to establish an important detail.

For example, evidence that a technology was used does not necessarily establish that it was used in production.

The answer-generation layer therefore needs to distinguish between what the retrieved evidence explicitly supports and what would require an unsupported inference.

### Solution

I started manually testing the retrieval and answer-generation pipeline with representative candidate questions.

The tests compare questions against the retrieved top-ranked sections and inspect whether the resulting answer stays within the evidence provided.

The answer prompt was also strengthened with explicit instructions to:

- Use retrieved context as evidence rather than repeating it mechanically.
- Avoid inventing technologies, responsibilities, projects, or experience.
- Avoid inferring production experience unless the retrieved evidence explicitly supports it.
- Distinguish between evidence that a technology was used and evidence that it was used in production.
- State clearly when the evidence is insufficient.

Retrieval results are currently inspected manually before considering further changes to the retrieval strategy.

### Result

The system can now be tested not only for whether it retrieves semantically related content, but also for whether the resulting answer makes claims that are actually supported by the retrieved evidence.

Initial manual tests have covered questions involving ASP.NET Core, PostgreSQL, pgvector, .NET/PostgreSQL, databases, CI/CD, Azure authentication, and ERP experience.

This evaluation is currently manual rather than an automated retrieval benchmark.

---

# Action

## Architecture

### Knowledge Source

Project documentation is stored under:

`knowledge/projects`

Each project is represented by a Markdown document containing:

- YAML frontmatter.
- Project overview.
- Context.
- Responsibilities.
- Challenges.
- Technical decisions.
- Results.
- Lessons learned.
- Interview-oriented information.

The frontmatter provides structured project metadata such as:

- Title.
- Organization.
- Role.
- Period.
- Status.
- Technologies.
- Concepts.
- Links.

The Markdown documents are version controlled and remain the source of truth.

---

### Frontend

The intended application includes a React and TypeScript frontend.

The current development focus is primarily on the backend ingestion and retrieval pipeline rather than a completed candidate-facing UI.

The frontend is intended to provide the future interface for:

- Entering job descriptions.
- Asking candidate-oriented questions.
- Reviewing retrieved project evidence.
- Generating candidate responses.
- Evaluating matching results.

The current retrieval implementation is therefore intentionally usable independently of the final frontend workflow.

---

### Backend

The backend is implemented with C# and ASP.NET Core.

The backend is responsible for:

- Document ingestion.
- Markdown parsing.
- Frontmatter extraction.
- Chunk generation.
- Metadata propagation.
- Embedding generation.
- PostgreSQL persistence.
- Query embedding generation.
- Vector similarity search.
- Retrieval result formatting.

The backend acts as the orchestration layer between the Markdown knowledge base, embedding model, PostgreSQL/pgvector, and the future LLM generation workflow.

---

### Database

PostgreSQL is used as the generated retrieval index.

The pgvector extension provides vector storage and similarity search.

The database stores each generated chunk together with:

- Source document.
- Section.
- Project metadata.
- Embedding vector.

The Markdown files remain authoritative.

If the chunking strategy, metadata schema, or embedding model changes, the PostgreSQL retrieval index can be regenerated from the source documents.

---

### Embedding Pipeline

The development environment uses BAAI `bge-small-en-v1.5` through Ollama.

The model produces 384-dimensional embeddings.

The same embedding model is used for both:

- Document chunks during indexing.
- User queries during semantic retrieval.

Using the same model ensures that document and query vectors occupy the same embedding space.

Changing the embedding model requires the existing document embeddings to be regenerated.

---

### Semantic Retrieval

A user question is converted into an embedding and compared against the stored document embeddings using pgvector.

The retrieval output currently exposes the top-ranked results for inspection.

Each result contains:

- Similarity score.
- Source project.
- Section.
- Retrieved content.
- Associated project metadata.

The retrieval layer is intentionally exposed for inspection so that retrieval quality can be evaluated before the LLM generation stage is introduced.

The current test output uses the top 10 retrieved results when evaluating whether sufficient evidence is available for an answer.

The retrieval implementation does not currently treat the similarity score as a definitive relevance decision.

---

### Data Flow

The current ingestion flow is:

`Markdown files` -> `MarkdownDocumentLoader` -> `FrontmatterParser` -> `Section chunking` -> `Metadata propagation` -> `Embedding generation` -> `PostgreSQL + pgvector`

The current query flow is:

`User question` -> `Query embedding` -> `pgvector similarity search` -> `Top-k chunks` -> `Retrieved project context`

The intended future flow is:

`Job description/question` -> `Query processing` -> `Semantic + metadata retrieval` -> `Relevant project context` -> `LLM` -> `Grounded candidate response`

---

## Technical Decisions

### Decision: Markdown as the Source of Truth

#### Context

Project knowledge needs to remain easy to maintain, version controlled, readable, and editable without database tooling.

#### Chosen Solution

Project documentation is maintained as Markdown files in the repository.

The KnowledgeIndexer processes these documents and generates the retrieval representation in PostgreSQL.

PostgreSQL is therefore treated as a generated index rather than the authoritative knowledge store.

#### Alternatives Considered

- Database-driven knowledge management.
- JSON documents.
- Manually maintained prompt context.

#### Trade-offs

Markdown provides:

- Human-readable documentation.
- Git-based version control.
- Easy review.
- Easy editing.
- Simple rebuilding of the retrieval index.

The trade-off is that changes to the source documents require the retrieval index to be regenerated.

---

### Decision: YAML Frontmatter for Structured Metadata

#### Context

Important project information should be available independently of the prose.

Technologies, roles, status, organization, and project periods should not have to be inferred from natural-language text.

#### Chosen Solution

YAML frontmatter is used for structured project metadata.

The metadata is parsed during ingestion and propagated to every chunk originating from the project.

#### Alternatives Considered

- Extracting metadata from prose.
- Maintaining metadata exclusively in PostgreSQL.
- Separate JSON metadata files.

#### Trade-offs

The approach provides explicit, version-controlled metadata that can be used by both the ingestion and retrieval layers.

The trade-off is that frontmatter must remain syntactically valid and changes require re-indexing.

The metadata schema also needs stronger validation in a future iteration.

---

### Decision: Section-Based Chunking

#### Context

Whole-document embeddings would be too broad for precise retrieval, while arbitrary fixed-size chunks could split concepts at inappropriate boundaries.

#### Chosen Solution

Documents are initially divided according to Markdown headings.

Each section becomes a retrieval unit together with its source project and metadata.

#### Alternatives Considered

- Whole-document embeddings.
- Fixed character-length chunks.
- Token-based chunks.
- Paragraph-based chunks.

#### Trade-offs

Section-based chunking preserves the structure already present in the documentation and makes retrieval results easy to understand.

The trade-off is that very large sections may still be too broad and may require secondary splitting later.

---

### Decision: PostgreSQL with pgvector

#### Context

The project needs semantic similarity search while also requiring structured project metadata.

The current knowledge base does not justify introducing a separate vector database.

#### Chosen Solution

PostgreSQL stores the chunks, metadata, and embedding vectors.

pgvector provides vector similarity search.

#### Alternatives Considered

- PostgreSQL full-text search.
- Keyword-only search.
- Dedicated vector databases.
- Hybrid search.

#### Trade-offs

Using PostgreSQL provides:

- One persistence system.
- Structured and vector data in the same database.
- Straightforward development and inspection.
- Easy access to metadata during retrieval.

The main limitation is that vector similarity alone is not sufficient for every candidate-matching query.

Exact technologies, identifiers, and highly specific requirements may be better handled using metadata or lexical search.

---

### Decision: Local Embeddings During Development

#### Context

The development environment needs repeatable embeddings without introducing external API costs or sending personal project information to a hosted embedding provider.

#### Chosen Solution

BAAI `bge-small-en-v1.5` is executed locally through Ollama.

The same model generates both document and query embeddings.

#### Alternatives Considered

- Hosted embedding APIs.
- OpenAI embeddings.
- Azure-hosted embedding services.

#### Trade-offs

Local embeddings provide:

- No embedding API cost during development.
- Local processing.
- Reproducible development.
- Control over the embedding pipeline.

The trade-offs are dependency on local model infrastructure and potentially lower inference performance depending on available hardware.

Changing the embedding model requires re-indexing the knowledge base.

---

### Decision: Keep Retrieval Separate from LLM Generation

#### Context

It is difficult to determine whether a poor AI response is caused by retrieval quality or by the generation model when both systems are introduced simultaneously.

#### Chosen Solution

The retrieval pipeline is being implemented and evaluated independently before connecting it to the LLM.

The system exposes the retrieved project sections and similarity scores so that retrieval quality can be inspected directly.

The answer-generation prompt is also designed to require the model to stay within the evidence retrieved for each question and to explicitly acknowledge insufficient evidence.

#### Alternatives Considered

- Implementing retrieval and LLM generation simultaneously.
- Sending large parts of the knowledge base directly into the LLM.
- Evaluating only the final generated response.

#### Trade-offs

Separating retrieval makes the system easier to debug and evaluate.

The trade-off is that the complete candidate-assistant workflow takes longer to implement because generation is deliberately postponed until the retrieval foundation is sufficiently reliable.

---

## Implementation

The implementation currently covers the complete ingestion and semantic retrieval pipeline.

The main implementation flow is:

1. Project Markdown files are stored under `knowledge/projects`.
2. The `MarkdownDocumentLoader` recursively discovers Markdown documents.
3. Template files are excluded from ingestion.
4. The `FrontmatterParser` extracts YAML metadata.
5. The document is separated into metadata and Markdown content.
6. The content is split into sections based on Markdown headings.
7. Project metadata is propagated to each generated chunk.
8. Generated chunks are validated.
9. Each chunk is passed through the local embedding model.
10. The resulting 384-dimensional vectors are stored in PostgreSQL.
11. pgvector provides vector similarity search over the stored embeddings.
12. A user question is converted into an embedding using the same model.
13. The query vector is compared against the stored document vectors.
14. The top-ranked chunks are returned for inspection.
15. Retrieval results expose the similarity score, source document, section, content, and project metadata.
16. Retrieved evidence can be evaluated against candidate-oriented questions before being passed to the future LLM generation layer.

The current pipeline is:

`Markdown` -> `Frontmatter parsing` -> `Document loading` -> `Section chunking` -> `Metadata propagation` -> `Embedding generation` -> `PostgreSQL + pgvector` -> `Query embedding` -> `Similarity search` -> `Top-N retrieved chunks`

The current implementation intentionally does not treat the similarity score as a definitive relevance decision.

---

# Result

The ingestion and semantic retrieval foundation is operational.

The system can currently:

- Discover Markdown project documentation recursively.
- Exclude template files.
- Parse YAML frontmatter.
- Extract structured project metadata.
- Split documents into semantic sections.
- Propagate project metadata to generated chunks.
- Validate generated chunks.
- Generate 384-dimensional embeddings locally.
- Store embeddings in PostgreSQL.
- Perform vector similarity search using pgvector.
- Generate embeddings for natural-language queries.
- Retrieve semantically related project sections.
- Inspect retrieval results independently of LLM generation.
- Inspect similarity scores, source sections, and associated project metadata.
- Manually evaluate whether retrieved evidence supports candidate-oriented answers.

Current retrieval tests show similarity scores approximately in the `0.58-0.82` range depending on the query and retrieved content.

These scores are useful for comparing retrieval results but should not be interpreted as probabilities or as a universal relevance threshold.

The retrieval output is currently inspected using the top-ranked results, with the test workflow exposing the top 10 results for evidence evaluation.

Initial manual tests have been performed against multiple candidate-oriented questions, including questions about:

- ASP.NET Core.
- Azure authentication.
- CI/CD.
- PostgreSQL.
- pgvector.
- .NET with PostgreSQL.
- Databases.
- ERP systems.
- PostgreSQL production usage.

The answer-generation prompt has also been refined to reduce unsupported claims. In particular, it explicitly distinguishes between evidence that a technology was used and evidence that it was used in production.

The current evaluation remains manual. There is not yet an automated retrieval evaluation dataset, automated top-N comparison, metadata-aware ranking implementation, hybrid search implementation, or reranking layer.

The current system therefore provides a working retrieval foundation and an initial manual evaluation workflow, while deliberately leaving those retrieval optimizations for later validation.

---

# Lessons Learned

## Retrieval Quality Is More Important Than the LLM Layer

A sophisticated LLM cannot compensate for consistently poor retrieval.

If the wrong project or section is supplied as context, the generation layer can produce an answer that sounds convincing while still being unsupported by the candidate's actual experience.

The retrieval layer therefore needs to be evaluated independently before relying on generated answers.

---

## Source Data Should Remain Human-Maintained

Keeping project knowledge in Markdown makes the source material easy to read, review, update, and version control.

The database can then be treated as disposable generated infrastructure.

This is preferable to making the vector database the authoritative source because the complete knowledge base can be rebuilt when the indexing strategy changes.

---

## Metadata Should Not Be Inferred When It Can Be Declared

A technology mentioned in a document does not necessarily mean that the project implemented that technology.

Explicit frontmatter makes this distinction clearer.

For example, the project's `technologies` field represents technologies declared as part of the project's implemented technology stack rather than technologies merely mentioned somewhere in the documentation.

This provides a much stronger foundation for future technology-aware retrieval.

---

## Prompt-Level Evidence Constraints Matter

Retrieval quality and answer quality are related but separate problems.

Even when the retrieved context is relevant, an LLM can make an unsupported inference if the answer prompt does not explicitly constrain it.

One example is production experience: evidence that PostgreSQL was used in a project does not by itself prove that PostgreSQL was used in a commercial production environment.

The answer prompt therefore needs to distinguish between:

- Evidence that a technology was used.
- Evidence that a technology was used in production.
- Evidence that a responsibility was actually performed.
- Evidence that a claim is only implied rather than explicitly supported.

This reduces the risk of technically plausible but unsupported candidate claims.

---

## Chunking Is a Retrieval Design Decision

The chunking strategy directly affects retrieval quality.

Whole-document embeddings provide too little precision, while overly small chunks can lose the context required to understand a technical decision or project result.

Using Markdown headings as initial boundaries provides a practical balance because the documentation structure already reflects meaningful semantic units.

The strategy should still evolve as retrieval evaluation produces more evidence.

---

## Vector Similarity Is Not a Complete Matching Strategy

Semantic search is useful for finding conceptually related project experience, but it is not sufficient for every candidate-oriented question.

Exact technology requirements, specific roles, organizations, project status, and other structured constraints are better represented through metadata.

This makes hybrid retrieval or metadata-aware ranking a natural next step.

---

## Similarity Scores Are Not Probabilities

A similarity score such as `0.8` does not mean that a result is 80% relevant.

The value depends on the embedding model, vector distribution, query, corpus, and similarity metric.

Retrieval quality should therefore be evaluated using actual questions and expected results rather than relying on an arbitrary score threshold.

---

## Manual Retrieval Testing Is Useful but Limited

Manual inspection is useful during early development because it makes it possible to see which project sections are being retrieved and whether the generated answer remains grounded in those sections.

The current tests use representative candidate-oriented questions and inspect the retrieved top-ranked results before judging the generated answer.

However, manual testing alone is not enough to determine whether a retrieval change consistently improves the system.

A larger evaluation dataset and automated regression tests are therefore future improvements rather than current functionality.

---

# Future Improvements

## Retrieval

- Introduce metadata-aware filtering.
- Add metadata-aware ranking.
- Compare vector-only retrieval against hybrid retrieval.
- Add lexical search for exact technologies and identifiers.
- Add project-level result aggregation.
- Add result deduplication.
- Evaluate reranking strategies.
- Consider query rewriting where appropriate.
- Evaluate different top-N values using a retrieval evaluation dataset rather than choosing top-k based only on manual inspection.

## Evaluation

- Create a representative retrieval evaluation dataset.
- Define expected projects and sections for each test question.
- Measure top-k retrieval precision.
- Track false-positive retrievals.
- Add automated retrieval regression tests.
- Compare retrieval strategies quantitatively rather than relying only on manual inspection.
- Evaluate whether fewer than the current top-ranked results provide sufficient evidence for specific question types.

## LLM Integration

- Connect retrieved context to an LLM.
- Generate grounded candidate responses.
- Include source project references in generated answers.
- Prevent unsupported claims when the knowledge base does not contain sufficient evidence.
- Add job-description extraction.
- Implement candidate-to-job matching.
- Separate retrieval confidence from generation quality.

## Knowledge Management

- Add stronger frontmatter schema validation.
- Detect invalid or incomplete project metadata during indexing.
- Automatically re-index changed documents.
- Detect deleted or renamed source documents.
- Track knowledge-base versioning.
- Add index rebuild commands.

## Infrastructure

- Containerize the complete development environment.
- Add automated indexing to the development workflow.
- Add observability around ingestion and retrieval.
- Add production deployment when the retrieval and generation workflow is sufficiently validated.

---