---
title: AI Candidate Assistant with RAG Knowledge Base

organization: Personal Project

role: Fullstack Developer

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

links:
  github:
  live:

---

# Overview

An AI-powered candidate assistant using Retrieval-Augmented Generation (RAG) to match job descriptions against a structured personal knowledge base.

The system ingests project documentation written in Markdown, extracts structured frontmatter metadata, splits documents into semantic sections, generates embeddings, and stores the resulting searchable representation in PostgreSQL with pgvector.

The intended application workflow is:

1. A user provides a job description or question.
2. The backend generates an embedding for the query.
3. PostgreSQL performs semantic similarity search against indexed project knowledge.
4. Relevant project chunks are retrieved together with their metadata.
5. Retrieved knowledge is used as context for an LLM response.

The current implementation focuses on validating the ingestion and retrieval foundation before completing the LLM-powered candidate matching workflow.

---

# Context

A general-purpose LLM does not have reliable access to a candidate's personal project history.

The system therefore uses a version-controlled knowledge base containing structured documentation about completed and ongoing projects.

The knowledge base is intentionally stored as Markdown rather than directly in the database. PostgreSQL is treated as a generated search index containing chunks, metadata, and embeddings.

This creates a clear separation between:

* Human-maintained project knowledge.
* Generated retrieval data.
* Runtime semantic search.
* Future LLM generation.

---

# Task

The current implementation focuses on building and validating the document ingestion and semantic retrieval pipeline.

Implemented functionality includes:

* Recursive Markdown document discovery.
* Template file filtering.
* YAML frontmatter parsing.
* Structured metadata extraction.
* Markdown section-based chunking.
* Metadata propagation from documents to chunks.
* 384-dimensional embedding generation.
* PostgreSQL persistence.
* pgvector similarity search.
* Query embedding generation.
* Retrieval of the most semantically similar chunks.

The system can currently ingest the project knowledge base and answer semantic queries against the resulting vector index.

---

# Challenge

## Challenge: Building a Reliable Personal Knowledge Retrieval Layer

### Problem

A RAG system is only useful if the retrieved knowledge is relevant and grounded in the source material.

Several problems therefore need to be controlled:

* Project documentation must have predictable structure.
* Metadata must survive the ingestion pipeline.
* Chunks must preserve their source and section information.
* Document and query embeddings must use the same embedding model.
* Similarity search must retrieve meaningful project information rather than merely matching individual keywords.

The system also needs to remain easy to inspect and rebuild during development.

---

### Solution

The knowledge base uses structured Markdown documents with YAML frontmatter.

The ingestion pipeline processes each document through the following stages:

1. Discover Markdown files recursively.
2. Ignore template files.
3. Parse YAML frontmatter.
4. Separate metadata from document content.
5. Split content into Markdown sections.
6. Attach document metadata to generated chunks.
7. Generate embeddings for each chunk.
8. Store chunks, metadata, and vectors in PostgreSQL.
9. Generate embeddings for user queries.
10. Perform vector similarity search using pgvector.

The Markdown files remain the source of truth while PostgreSQL acts as the generated retrieval index.

---

# Result

The ingestion and semantic retrieval pipeline is operational.

The system currently demonstrates:

* Structured Markdown knowledge ingestion.
* YAML metadata extraction.
* Metadata propagation into generated chunks.
* Local embedding generation.
* PostgreSQL vector storage.
* Semantic similarity search.
* Retrieval of relevant project sections from natural-language questions.

Example retrieval results currently return similarity scores in the approximate `0.58?0.82` range depending on the query and retrieved content.

The retrieval output is inspectable through the KnowledgeIndexer, making it possible to evaluate which projects and sections are being selected for individual questions.

---

# Architecture

## Knowledge Source

Project documentation is stored under:

`knowledge/projects`

Each project is represented as a Markdown document containing:

* YAML frontmatter.
* Project overview.
* Context.
* Responsibilities.
* Technical decisions.
* Challenges.
* Results.
* Interview-oriented information.

Frontmatter contains structured information such as:

* Title.
* Organization.
* Role.
* Period.
* Status.
* Technologies.
* Concepts.
* Links.

---

## Document Ingestion

The `MarkdownDocumentLoader` recursively discovers Markdown documents and excludes template files.

The `FrontmatterParser` extracts YAML frontmatter from each document.

The resulting `MarkdownDocument` contains:

* File name.
* Markdown content.
* Structured metadata.

The metadata is then propagated to every generated chunk originating from the document.

---

## Chunking

Documents are divided into chunks based on Markdown headings.

Each chunk contains:

* Source document.
* Section.
* Content.
* Project metadata.
* Embedding vector.

Section-based chunking was selected because Markdown headings already represent meaningful semantic boundaries in the knowledge base.

This also makes retrieval results easier to understand and debug.

Very large sections may require additional splitting in a later iteration.

---

# Database

PostgreSQL is used as the persistence layer.

The pgvector extension provides vector storage and similarity search.

The database stores the generated retrieval representation:

* Chunk content.
* Source document.
* Section.
* Metadata.
* Embedding vector.

The Markdown documents remain the authoritative source. PostgreSQL is a generated index that can be rebuilt from the knowledge base.

---

# Embeddings

The current development environment uses BAAI `bge-small-en-v1.5` through Ollama.

The model produces 384-dimensional embeddings.

The same embedding model is used for:

* Document chunks during indexing.
* User queries during semantic search.

This is required because document and query vectors must exist in the same embedding space for meaningful similarity comparison.

Changing the embedding model requires re-indexing the existing knowledge base.

---

# Semantic Search

User questions are converted into embeddings and compared against stored document embeddings.

The current search returns the top five results ranked by vector similarity.

The results include:

* Similarity score.
* Source document.
* Section.
* Retrieved content.

Current retrieval testing shows that semantically related project sections can be retrieved even when the query does not contain the exact terminology used in the source document.

However, semantic similarity alone does not guarantee that a result is factually appropriate for a specific candidate question.

---

# Metadata

Metadata is extracted from project frontmatter and propagated to every chunk belonging to the project.

This provides structured information that can later be used for:

* Metadata filtering.
* Technology-specific retrieval.
* Status filtering.
* Role filtering.
* Organization filtering.
* Project-level ranking.
* Retrieval explanations.

A particularly important distinction is that frontmatter represents the project's declared implemented state.

For example, technologies listed in the project's `technologies` metadata are treated as implemented project technologies rather than merely technologies mentioned somewhere in the document.

---

# Technical Decisions

## Decision: Markdown as the Source of Truth

### Context

Project knowledge needs to remain easy to maintain, version controlled, and readable without requiring database tooling.

### Chosen Solution

Markdown files are maintained in the repository and processed by the KnowledgeIndexer.

PostgreSQL contains only the generated retrieval representation.

### Alternatives Considered

* Database-driven knowledge management.
* JSON documents.
* Manually maintained prompt context.

### Trade-offs

Advantages:

* Human-readable.
* Git-friendly.
* Easy to review.
* Easy to modify.
* Re-indexable.

Disadvantages:

* Requires an indexing process.
* Markdown structure must remain predictable.
* Changes require re-indexing.

---

## Decision: YAML Frontmatter for Structured Metadata

### Context

Project descriptions contain information that should be searchable and filterable independently from prose.

### Chosen Solution

Project documents use YAML frontmatter containing structured metadata such as technologies, concepts, role, status, and project period.

The metadata is parsed during ingestion and attached to every generated chunk.

### Alternatives Considered

* Extracting metadata from prose.
* Maintaining metadata exclusively in PostgreSQL.
* Separate JSON metadata files.

### Trade-offs

Advantages:

* Human-readable.
* Version controlled with the project documentation.
* Explicit rather than inferred.
* Available to both indexing and retrieval.
* Enables future metadata-aware ranking.

Disadvantages:

* Frontmatter syntax must remain valid.
* Metadata changes require re-indexing.
* Schema validation is still limited.

---

## Decision: Section-Based Chunking

### Context

Whole-document embeddings are too coarse for precise retrieval.

### Chosen Solution

Documents are split according to Markdown headings.

Each section becomes a retrieval unit together with its source and metadata.

### Alternatives Considered

* Whole-document embeddings.
* Fixed character-length chunks.
* Token-based chunks.
* Paragraph-based chunks.

### Trade-offs

Advantages:

* Preserves semantic boundaries.
* Easy to inspect.
* Easy to debug.
* Produces understandable retrieval results.

Disadvantages:

* Large sections can still be too broad.
* Depends on consistent Markdown structure.
* Chunking changes require re-indexing.

---

## Decision: PostgreSQL with pgvector

### Context

The system needs semantic similarity search without introducing an additional database product.

### Chosen Solution

PostgreSQL stores the document chunks, metadata, and 384-dimensional embedding vectors.

pgvector performs vector similarity search.

### Alternatives Considered

* PostgreSQL full-text search.
* Keyword-only search.
* Dedicated vector databases.
* Hybrid search.

### Trade-offs

Advantages:

* One database for structured and vector data.
* Easy development and inspection.
* Metadata can be stored alongside vectors.
* Suitable for the current knowledge-base size.

Disadvantages:

* Retrieval quality depends on embeddings and chunking.
* Semantic search can be weaker for exact identifiers and terminology.
* Vector indexing adds storage and maintenance requirements.

---

## Decision: Local Embeddings During Development

### Context

The project requires repeatable document and query embeddings during development.

### Chosen Solution

BGE Small is run locally through Ollama.

The same model generates both document and query embeddings.

### Alternatives Considered

* Hosted embedding APIs.
* OpenAI embeddings.
* Azure-hosted embedding services.

### Trade-offs

Advantages:

* No embedding API cost during development.
* Data remains local.
* Easy to reproduce.
* Full control over the embedding pipeline.

Disadvantages:

* Requires local model infrastructure.
* Inference performance depends on local hardware.
* Changing models requires re-indexing.

---

# Current Implementation

Completed:

* Markdown document loader.
* Recursive document discovery.
* Template file filtering.
* YAML frontmatter parsing.
* Structured metadata extraction.
* Metadata propagation.
* Markdown section chunking.
* Chunk validation.
* Local embedding generation.
* PostgreSQL persistence.
* pgvector integration.
* Query embedding generation.
* Semantic similarity search.
* Retrieval result inspection.

---

# Current Development State

The project has moved beyond the initial ingestion stage.

The current pipeline is:

```text
Markdown
   ?
Frontmatter parsing
   ?
Document loading
   ?
Section chunking
   ?
Metadata propagation
   ?
Embedding generation
   ?
PostgreSQL + pgvector
   ?
Query embedding
   ?
Similarity search
   ?
Top-k retrieved chunks
```

The remaining major part of the MVP is connecting retrieved knowledge to the LLM response-generation workflow.

---

# Evaluation

Retrieval quality is currently being evaluated manually using a set of candidate-oriented questions.

The evaluation focuses on:

* Whether the correct project is retrieved.
* Whether the correct section is retrieved.
* Similarity score distribution.
* False-positive retrievals.
* Whether metadata could improve ranking.
* Whether exact technical requirements are better handled through metadata than pure vector similarity.

Current results show that vector search is capable of retrieving semantically relevant projects, but similarity scores alone should not be treated as a definitive relevance threshold.

---

# Known Limitations

The current retrieval system is vector-only.

This creates several limitations:

* Exact technologies may not always rank highly enough.
* Similar concepts can produce false positives.
* Similarity scores are not probabilities.
* A high score does not necessarily mean the retrieved project is the best candidate.
* Metadata is currently available but is not yet used as a ranking signal.
* There is no formal retrieval evaluation dataset yet.
* There is no automated regression test for retrieval quality.

These limitations are expected to guide the next retrieval iteration.

---

# Next Steps

The next development priorities are:

1. Use frontmatter metadata as structured retrieval information.
2. Introduce metadata-aware ranking or filtering.
3. Evaluate vector-only retrieval against metadata-aware retrieval.
4. Create a small retrieval evaluation dataset.
5. Measure precision of the top-k results.
6. Connect retrieved context to LLM generation.
7. Implement the candidate/job matching workflow.
8. Add automated indexing when knowledge documents change.

---

# Future Improvements

Potential future improvements include:

* Hybrid lexical and vector search.
* Metadata-aware ranking.
* Technology-aware retrieval.
* Project-level result aggregation.
* Retrieval evaluation and regression tests.
* Query rewriting.
* Result deduplication.
* Reranking.
* LLM-based answer generation.
* Job-description extraction.
* Candidate-to-job matching.
* Automated knowledge indexing.
* Production deployment.

---

# Interview Notes

## Why use Markdown?

Markdown keeps project knowledge human-readable, version controlled, and easy to maintain. PostgreSQL is treated as a generated search index rather than the source of truth.

## Why use frontmatter?

Frontmatter provides explicit structured metadata that should not have to be inferred from prose. It can later be used for filtering and ranking.

## Why chunk by sections?

Markdown headings already provide semantic boundaries. Section-based chunks are easier to inspect and debug than arbitrary text fragments.

## Why use pgvector?

The current system already uses PostgreSQL for application data, so pgvector provides vector search without introducing another database.

## Why use local embeddings?

Local embeddings reduce development cost, keep data local, and make the indexing pipeline reproducible.

## Why isn't vector similarity enough?

Semantic similarity is useful for finding related knowledge, but it does not understand the business meaning of metadata such as whether a technology was actually implemented. Metadata-aware ranking can therefore complement vector similarity.

---

# Key Talking Points

* Built a custom RAG knowledge ingestion pipeline.
* Designed a Markdown-based personal knowledge base.
* Implemented YAML frontmatter metadata extraction.
* Propagated structured metadata into searchable chunks.
* Implemented section-based document chunking.
* Generated 384-dimensional local embeddings.
* Stored embeddings using PostgreSQL and pgvector.
* Implemented semantic similarity search.
* Built tooling for inspecting retrieval quality.
* Identified metadata-aware retrieval as the next optimization step.
