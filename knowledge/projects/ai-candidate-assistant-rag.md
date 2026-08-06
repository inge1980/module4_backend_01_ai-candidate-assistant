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

---

# Technical Decisions

## Decision: Markdown as Knowledge Source

### Context

The knowledge base needs to be easy to maintain and version control.

---

### Chosen Solution

Markdown documents with structured YAML frontmatter.

---

### Alternatives Considered

- Database-driven content management.
- JSON documents.
- Manual prompt-based context.

---

### Trade-offs

Advantages:

- Human-readable.
- Git-friendly.
- Easy to update.
- Works well with developer documentation.

Disadvantages:

- Requires validation of document structure.
- Requires ingestion processing.

---

## Decision: Section-Based Chunking

### Context

Large documents need to be split into smaller retrieval units.

---

### Chosen Solution

Documents are split based on Markdown headings.

Each chunk contains:

- Source document.
- Section title.
- Content.
- Metadata.

---

### Alternatives Considered

- Fixed character length chunks.
- Token-based chunking.
- Paragraph-based splitting.

---

### Trade-offs

Advantages:

- Preserves document structure.
- Better semantic grouping.
- Easier debugging.

Disadvantages:

- Depends on consistent documentation structure.

---

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