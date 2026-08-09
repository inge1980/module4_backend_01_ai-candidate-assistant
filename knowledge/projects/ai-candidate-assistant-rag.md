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
  - bge-embeddings
  - openrouter
  - github-actions

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
  - cloud-deployment
  - developer-tools

links:
  github:
  live:

---

# Temp MVP

MVP TEMP INFO:

 - A user pastes a joblisting into chat
 - AI reponds with a match referencing relevant projects

# Overview

An AI-powered candidate assistant using Retrieval-Augmented Generation (RAG) to match job descriptions against a structured personal knowledge base.

The system is designed to allow a user to provide a job listing and receive an AI-generated response referencing relevant projects, technical experience, and previous work.

The knowledge base consists of structured Markdown documents containing project descriptions, technical decisions, challenges, and results.

---

# Context

Traditional AI assistants rely only on general model knowledge and do not have access to personal project history.

This project explores how a structured knowledge base combined with embeddings and semantic search can allow an AI assistant to provide answers grounded in verified personal experience.

The goal is to create a reusable developer profile assistant that can support job applications, interview preparation, and technical discussions.

---

# Task

The initial implementation focused on building the document ingestion pipeline required before introducing vector search.

Implemented:

- Loading Markdown knowledge documents.
- Recursive discovery of project documentation.
- Ignoring template files during ingestion.
- Extracting YAML frontmatter metadata.
- Splitting Markdown documents into searchable sections.
- Generating document chunks with metadata attached.
- Creating a verification output for inspecting generated chunks.

---

# Challenge

## Challenge: Creating a Reliable Knowledge Ingestion Pipeline

### Problem

A RAG system depends heavily on the quality of the retrieved context.

Poor document structure, missing metadata, or unsuitable chunk sizes can result in irrelevant retrieval and lower quality AI responses.

The knowledge base needed a predictable structure that could later support embeddings and semantic search.

---

### Solution

A structured Markdown-based knowledge format was created using a shared project template.

The ingestion pipeline processes documents through several steps:

1. Load Markdown files from the knowledge directory.
2. Ignore template documents that should not be indexed.
3. Extract frontmatter metadata such as title, organization, role, and technologies.
4. Split documents into logical sections.
5. Remove empty sections.
6. Generate chunks containing content and metadata.

Each generated chunk keeps a reference to its source document, section, and project metadata.

---

### Result

The system can currently:

- Read project documentation from Markdown files.
- Generate structured document chunks.
- Preserve project metadata.
- Produce verifiable ingestion output.

The foundation is ready for the next stage: generating embeddings and storing vectors for semantic search.

---

# Action

## Architecture

### Frontend

Planned:

A React-based interface where users can submit job descriptions and receive AI-generated match for that specific applicant for that specific job.

---

### Backend

Current implementation:

- .NET backend architecture.
- Document ingestion services.
- Markdown parsing.
- Chunk generation.
- Metadata handling.

Planned:

- Retrieval API.
- Semantic search.
- LLM orchestration.
- Candidate matching workflow.

---

### Database

Planned:

PostgreSQL with pgvector extension.

Responsibilities:

- Store document chunks.
- Store embeddings.
- Perform similarity searches.

---

### Infrastructure

Current:

- Docker-based development environment.

Planned:

- Containerized backend services.
- Vector database deployment.
- AI service integration.

# Technical Decisions

## Decision: Markdown as Knowledge Source

### Context

The knowledge base contains project descriptions and technical documentation that should be easy to maintain, review, and version control.

The content changes infrequently, but when changes are made, the system needs a predictable way to rebuild the search index.

---

### Chosen Solution

The Markdown files are stored in the repository and are the single source of truth for the knowledge base.

The KnowledgeIndexer processes these files and generates the searchable representation stored in PostgreSQL.

The database is therefore treated as a generated search index rather than the primary source of knowledge.

---

### Alternatives Considered

- Database-driven content management.
- JSON documents.
- Manually maintained LLM prompts with embedded context.

---

### Trade-offs

Advantages:

- Human-readable format.
- Git-friendly and version controlled.
- Easy to update.
- Natural format for technical documentation.
- Allows rebuilding the vector index from source files.

Disadvantages:

- Requires indexing processing.
- Document structure needs validation.
- Changes require re-indexing.

---

# Decision: Section-Based Chunking

### Context

Documents need to be split into smaller retrieval units before generating embeddings.

A complete document is usually too broad as a single retrieval unit for efficient semantic search.

---

### Chosen Solution

Documents are split into chunks based on Markdown headings.

Each chunk contains:

- Source document.
- Section title.
- Content.
- Metadata.
- Generated embedding vector.

The chunks are stored in PostgreSQL using the pgvector extension.

The section-based approach preserves the semantic boundaries already expressed by the document structure. This makes retrieved chunks easier to understand and debug than arbitrary character-based fragments.

Sections may require additional splitting later if individual sections become too large for effective retrieval.

---

### Alternatives Considered

- Fixed character-length chunks.
- Token-based chunking.
- Paragraph-based splitting.
- Whole-document embeddings.

---

### Trade-offs

Advantages:

- Preserves document structure.
- Keeps related content together.
- Better semantic grouping.
- Easier debugging and inspection.
- Allows precise retrieval of relevant sections.

Disadvantages:

- Depends on reasonably consistent Markdown structure.
- Very large sections may require additional splitting.
- Requires re-indexing when the chunking strategy changes.

---

# Decision: Vector Search with pgvector

### Context

The RAG system needs to find knowledge that is semantically relevant to a user's question rather than relying only on exact keyword matches.

Traditional SQL text matching can identify shared words, but it does not reliably identify related concepts expressed using different terminology.

---

### Chosen Solution

Use PostgreSQL with the pgvector extension for storing and searching document embeddings.

Each document chunk is converted into an embedding vector. When a user submits a question, the question is converted into an embedding using the same embedding model.

The vector representation of the question is compared against the stored document vectors using vector similarity. The most semantically similar chunks are retrieved and supplied as context to the LLM.

Cosine similarity is used for the initial vector search implementation.

The database stores 384-dimensional vectors using the PostgreSQL column type vector(384).

An HNSW index is used to make similarity searches efficient as the number of stored chunks grows.

---

### Alternatives Considered

- PostgreSQL full-text search only.
- Keyword-based search.
- External vector databases.
- Hybrid keyword and vector search.

---

### Trade-offs

Advantages:

- Semantic rather than purely lexical retrieval.
- Keeps vector data in the existing PostgreSQL database.
- Avoids introducing a separate vector database.
- Supports efficient nearest-neighbor search through HNSW.
- Easy to inspect and query during development.
- Allows metadata and document content to be stored alongside embeddings.

Disadvantages:

- Retrieval quality depends heavily on the embedding model and chunking strategy.
- Semantic search can miss exact identifiers, names, or technical terms where keyword search would perform better.
- Vector indexes add database storage and maintenance overhead.
- The embedding model must remain consistent between indexed documents and user queries.

---

# Decision: Local Embedding Model for Development

### Context

The system requires embeddings both when indexing documents and when processing user questions during chat.

The same embedding model must be used for both operations because vector similarity only works when vectors are generated in the same embedding space.

---

### Chosen Solution

Use BAAI bge-small-en-v1.5 locally through Ollama during development.

The model produces 384-dimensional embeddings, matching the PostgreSQL vector(384) column.

The embedding model is used by both the KnowledgeIndexer and the backend's runtime retrieval process.

Document embeddings are generated when knowledge is indexed or updated. Query embeddings are generated when a user submits a question.

This keeps the entire embedding pipeline local during development and avoids external embedding API costs.

---

### Alternatives Considered

- Hosted embedding APIs.
- OpenAI embedding models.
- Azure/OpenAI managed embedding services.
- Running a different embedding model remotely.

---

### Trade-offs

Advantages:

- No API cost during development.
- Full control over the embedding pipeline.
- Data remains local.
- Easy to reproduce the development environment.
- Good fit for a small knowledge base.

Disadvantages:

- Requires Ollama and the embedding model to be installed locally.
- Local inference is slower than some managed services.
- Changing the embedding model requires regenerating the stored document embeddings.

---

# Decision: Local-First RAG Development

### Context

The initial goal is to build and validate the complete RAG pipeline before introducing cloud deployment complexity.

---

### Chosen Solution

All application components are developed and tested locally first.

PostgreSQL and pgvector run locally through Docker Compose. The ASP.NET Core backend communicates with the local database, while the frontend communicates with the backend API.

The KnowledgeIndexer is run separately when the Markdown knowledge base needs to be imported or updated. It generates the document embeddings and stores the resulting chunks in PostgreSQL.

During chat, the backend generates an embedding for each user question, performs semantic vector search against the stored document chunks, and provides the retrieved context to the LLM.

Cloud deployment is intentionally postponed until the complete local RAG pipeline is working.

---

### Alternatives Considered

- Deploying directly to cloud infrastructure.
- Using managed vector databases from the start.
- Hosting the embedding model remotely during development.

---

### Trade-offs

Advantages:

- Faster development cycle.
- Easier debugging.
- Lower cost.
- Full understanding of the complete RAG pipeline.
- Clear separation between source documents and generated vector data.
- Allows deployment decisions to be made after the local architecture has been validated.

Disadvantages:

- Requires local setup.
- Cloud deployment decisions are postponed.
- Production architecture may require different infrastructure or embedding providers.

# Implementation

Completed:

- Markdown document loader.
- Template file filtering.
- YAML frontmatter parsing.
- Metadata extraction.
- Markdown section splitting.
- Document chunk generation.
- Chunk verification tooling.

Next:

- Generate embeddings.
- Store vectors in PostgreSQL with pgvector.
- Implement similarity search.
- Connect retrieval results to LLM generation.

---

# Result

The first stage of the RAG pipeline is implemented.

The system can transform structured Markdown project documentation into searchable document chunks while preserving metadata required for future retrieval.

---

# Lessons Learned

Key lessons:

- Document quality directly affects RAG quality.
- Metadata is important for filtering and explaining retrieval results.
- Chunking strategy impacts future semantic search performance.
- A structured knowledge format makes AI-generated answers more reliable.

---

# Interview Notes

## Possible Questions

### Why use Markdown instead of a database?

Markdown keeps project knowledge easy to maintain, version controlled, and readable while still allowing automated ingestion.

---

### Why extract metadata?

Metadata allows future filtering and improves explainability when retrieving relevant project information.

---

### Why chunk documents?

Embedding entire documents reduces retrieval precision. Smaller semantic chunks allow more relevant context to be retrieved. Optimize cost of tokens used for remote LLMs.

---

# Key Talking Points

- Built a custom RAG ingestion pipeline.
- Structured knowledge base using Markdown.
- Implemented metadata extraction.
- Designed document chunking strategy.
- Prepared architecture for embeddings and vector search.

---

# Future Improvements

- Generate embeddings using BGE embedding models.
- Store vectors using PostgreSQL and pgvector.
- Implement semantic similarity search.
- Integrate LLM response generation.
- Add hybrid search combining metadata filtering and vector similarity.
- Add evaluation dataset for retrieval quality.
- Add automated ingestion pipeline.