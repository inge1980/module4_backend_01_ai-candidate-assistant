# M.I.N.D - My Indexed Knowledge Directory

M.I.N.D is a personal AI knowledge assistant built to represent a developer's experience, projects, technical decisions, and professional background.

The system uses Retrieval-Augmented Generation (RAG) to combine semantic search with a Large Language Model (LLM). Knowledge is stored as Markdown files, indexed using embeddings, stored in PostgreSQL with pgvector, and retrieved dynamically when answering questions.

The goal is to create an AI assistant that can answer questions about a candidate's:

* Projects
* Technical experience
* Architecture decisions
* STAR stories
* CV and career history
* Engineering opinions and preferences

---

# Features

## Knowledge Management

* Markdown-based knowledge source (Single Source of Truth)
* Structured knowledge about projects, experience, and technical decisions
* Automatic indexing pipeline from Markdown to vector database

## Semantic Search (RAG)

* Generate embeddings using BAAI/bge-small-en-v1.5
* Store vectors using PostgreSQL with pgvector
* Retrieve relevant knowledge using semantic similarity search
* Build optimized context before sending information to the LLM

## AI Assistant

* Chat-based interface for asking questions about the candidate
* Uses retrieved knowledge instead of sending the entire knowledge base
* Generates answers through an external LLM provider via OpenRouter

## Architecture

* Backend API built with ASP.NET Core
* React frontend for chat interaction
* Local development environment using Docker Compose
* PostgreSQL + pgvector for vector storage
* Local embedding generation service

---

# Technologies

## Backend

* C#
* .NET / ASP.NET Core Web API
* Dependency Injection
* REST API

## AI / RAG

* BAAI/bge-small-en-v1.5 (embedding model)
* Hugging Face Text Embeddings Inference
* PostgreSQL pgvector
* OpenRouter LLM API

## Frontend

* React
* TypeScript
* Modern component-based UI

## Infrastructure

* Docker Compose
* PostgreSQL
* GitHub Actions

---

# How to Run

## 1. Start infrastructure

Start PostgreSQL with pgvector and the embedding service:

```bash
docker compose up -d
```

---

## 2. Index knowledge base

Import Markdown files and generate embeddings:

```bash
dotnet run --project src/tools/KnowledgeIndexer
```

This will:

1. Read Markdown files from `/knowledge`
2. Generate embeddings
3. Store indexed knowledge in PostgreSQL with pgvector

---

## 3. Start backend API

Run the ASP.NET Core API:

```bash
dotnet run --project src/backend/Api
```

---

## 4. Start frontend

Navigate to the frontend application:

```bash
cd frontend/Web
npm install
npm run dev
```

---

# Testing API

The API can be tested manually using Swagger UI:

```
http://localhost:5000/swagger
```

Example endpoint:

```
POST /api/chat
```

Request:

```json
{
  "question": "Tell me about the ERP project experience"
}
```

The API will:

1. Generate an embedding for the question
2. Search relevant knowledge in pgvector
3. Build context
4. Send context and question to the LLM
5. Return an AI-generated answer

---

# Development Goals

The project demonstrates:

* Retrieval-Augmented Generation (RAG)
* Semantic search
* Vector databases
* AI-assisted knowledge retrieval
* Clean backend architecture
* Full-stack AI application development
* Practical use of LLMs in developer tooling
