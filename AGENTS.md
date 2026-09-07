# Project map (M.I.N.D)

Backend RAG assistant: Markdown knowledge ? embeddings ? PostgreSQL/pgvector ? evidence-based LLM answers.

- **SSoT:** `knowledge/` Markdown. Postgres is a rebuildable index.
- **Solution:** `module4_backend_01_ai-candidate-assistant.slnx` (.NET 10).
- **Shared config:** repo-root `appsettings.json` + `.env` via `Infrastructure.Configuration.AppConfiguration` (walks up until it finds the `.slnx`).

Do not commit `.env`. Copy from `.env.example`.

---

## Run locally

```text
docker compose up -d
ollama pull qllama/bge-small-en-v1.5
dotnet run --project src/tools/KnowledgeIndexer          # from repo root
dotnet run --project src/tools/CandidateConsoleAssistant
dotnet run --project src/backend/Api
```

- API: `http://localhost:5179` ? Swagger `/swagger`, `POST /api/v1/Questions`, LLM smoke `GET /api/v1/llm/test`
- Embeddings: Ollama `OLLAMA_EMBED_URL` + `OLLAMA_EMBEDDING_MODEL` (env; not the JSON `Embeddings` section)
- Postgres: `ConnectionStrings__Postgres` ? db `candidate_ai`, table `document_chunks`, vector(384), HNSW cosine

Indexer `knowledgePath` is `cwd/knowledge/projects`. Run it from the repo root.

---

## Solution layout

```text
appsettings.json          Shared JSON config (GitHub, Knowledge, Embeddings, Llm, provider URLs)
.env.example              Env template (Ollama, Postgres, LLM API keys)
docker-compose.yml        pgvector/pg17 on :5432
docker/postgres/init.sql  extension + document_chunks + HNSW index
knowledge/
  _template_project.md    Excluded from indexing
  skills/skills.md        Skills notes (not indexed by KnowledgeIndexer)
  projects/*.md           Indexed project docs (YAML frontmatter)
src/backend/
  Api/                    ASP.NET Core host, controllers, DI
  Application/            Retrieval + question orchestration + prompt templates
  Infrastructure/         Documents, embeddings, pgvector, LLM clients, config
src/tools/
  KnowledgeIndexer/       Ingest Markdown ? embed ? upsert chunks
  CandidateConsoleAssistant/  Manual retrieval + prompt inspection
PROJECT.md                Formal Module 4 brief
README.md                 Product/docs write-up
```

### Api (`src/backend/Api`)

| File | Role |
|---|---|
| `Program.cs` | DI, Swagger, controllers, static files, 404 fallback |
| `Controllers/QuestionsController.cs` | `POST /api/v1/Questions` (`includeDebug` query) |
| `Controllers/LlmController.cs` | `GET /api/v1/llm/test` (dev smoke) |
| `Properties/launchSettings.json` | http `5179`, https `7277` |
| `wwwroot/404.html` | Fallback page |
| `api.http` | Manual HTTP samples |

`QuestionService` is registered here but **lives in Application** with namespace `Api.Services`.

### Application (`src/backend/Application`)

| File | Role |
|---|---|
| `Knowledge/KnowledgeRetrievalService.cs` | Embed query ? `VectorStore.SearchAsync` ? score/rerank |
| `Knowledge/IKnowledgeRetrievalService.cs` | Retrieval contract |
| `Knowledge/KnowledgeRetrievalResult.cs` | Ranked `KnowledgeRetrievalItem` list |
| `Questions/QuestionService.cs` | Retrieve 25, take top 5 for prompt, call LLM, map sources |
| `Questions/IQuestionService.cs` | Ask contract |
| `Questions/AskQuestionRequest.cs` / `AskQuestionResponse.cs` | API DTOs |
| `Questions/QuestionSource.cs` / `QuestionRelevance.cs` | Source + score payload |
| `Questions/QuestionItem.cs` / `QuestionItemStatus.cs` / `QuestionDebugInfo.cs` | Extra question types |
| `Prompts/answer/answer-prompt-v1.md` ? `v6.md` | Prompt history; **runtime uses v6** (copied to output) |

Hardcoded prompt path: `Prompts/answer/answer-prompt-v6.md` (Api + console).

### Infrastructure (`src/backend/Infrastructure`)

**Documents**

| File | Role |
|---|---|
| `MarkdownDocumentLoader.cs` | Recursive `*.md`, skip `_template_project.md` |
| `FrontmatterParser.cs` | YAML frontmatter ? metadata + body |
| `ParsedMarkdown.cs` / `MarkdownDocument.cs` | Parse/load models |
| `DocumentChunker.cs` | Split on ATX headings; nested `HeadingPath` |
| `SemanticTypeResolver.cs` | Map heading path ? semantic type |
| `DocumentChunk.cs` | Chunk stored in pgvector (`Id`, `Source`, `HeadingPath`, ?) |

**Embeddings**

| File | Role |
|---|---|
| `EmbeddingService.cs` | POST to Ollama `/api/embed` |
| `VectorStore.cs` | Upsert on `id` conflict; cosine search `1 - (embedding <=> q)` |
| `SearchResult.cs` | Chunk + vector/metadata/evidence/combined scores |

**Reranking**

| File | Role |
|---|---|
| `MetadataEvidenceScorer.cs` | Term overlap on metadata + content; combined score |
| `IReranker.cs` / `RerankResult.cs` | Rerank types (scorer is what retrieval uses) |

**LLM**

| File | Role |
|---|---|
| `ILLMClient.cs` | `GenerateAsync` |
| `LlmClientFactory.cs` | Flatten `Llm.Providers[]` × `Models[]` ? `FallbackLlmClient` |
| `FallbackLlmClient.cs` | Sequential try/fail/log |
| `GeminiClient.cs` | Google provider |
| `GroqClient.cs` / `OpenRouterClient.cs` | OpenAI-compatible |
| `CerebrasClient.cs` | Implemented; not in active `appsettings` provider list |
| `LlmOptions.cs` / `LlmProviderOptions.cs` / `LlmProviderException.cs` | Options + errors |

**Config:** `Configuration/AppConfiguration.cs`

### Tools

- **KnowledgeIndexer:** load projects ? chunk ? embed ? `InsertAsync`. Prints index summary. Does **not** delete chunks whose source files were removed.
- **CandidateConsoleAssistant:** hardcoded eval questions; prints top-10 retrieval + built answer prompt (`retrievalLimit=10`, `promptContextLimit=5`).

---

## Knowledge base

Indexed from `knowledge/projects/` (not `knowledge/skills/`):

- `ai-candidate-assistant-rag.md`
- `azure-dotnet-devops-demo.md`
- `bootstrap-migration.md`
- `canteen-ordering-system.md`
- `developer-portfolio.md`
- `erp-platform-development.md`
- `gdpr-compliant-form-builder.md`
- `hierarchical-shopping-list-app.md`
- `lost-and-found-api.md`
- `n8n-social-content-generator.md`
- `pim-integration.md`
- `react-hotel-booking-case.md`

Frontmatter (see `_template_project.md`): `title`, `organization`, `role`, `environment`, `period`, `status`, `technologies`, `concepts`, `dependencies`, `links`.

Chunk id: `{filename}-{index:D3}-{headingSlug}`. Upsert updates same id; new heading structure can orphan old rows.

---

## Request path

1. `POST /api/v1/Questions` ? `QuestionService.AskAsync`
2. `KnowledgeRetrievalService`: embed ? pgvector (limit 25) ? `MetadataEvidenceScorer` ? sort by `CombinedScore`
3. Top **5** chunks into `answer-prompt-v6.md` (`{{question}}`, `{{context}}`)
4. `LlmClientFactory.Create()` ? fallback chain from `appsettings.json` `Llm.Providers`
5. Response: `answer` + sources (GitHub blob URLs from `GitHub:*` config). `includeDebug=true` adds scores and raw `Source`.

Console eval uses the same retrieval service with limit 10 / prompt context 5.

---

## Conventions and pitfalls

- Layers: Api host, Application orchestration, Infrastructure I/O. Tools reference Application + Infrastructure; they are not the REST host.
- Config: root `appsettings.json` + `.env`. Embedding **model/URL must be env vars**.
- Secrets: `Google__ApiKey`, `Groq__ApiKey`, `OpenRouter__ApiKey` (optional `Cerebras__ApiKey`).
- `VectorStore.InsertAsync` upserts; full rebuild after deleted/renamed docs needs a table wipe (or drop volume) then re-index.
- `QuestionService` namespace `Api.Services` vs folder `Application/Questions`.
- `GetProjectUrl` concatenates `ProjectsFolder` with an extra `/` before `{id}.md`.
- No automated tests in the solution.
- Out of scope: frontend, auth, public deploy (see `PROJECT.md`).
