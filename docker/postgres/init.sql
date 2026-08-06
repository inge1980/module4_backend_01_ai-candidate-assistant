CREATE EXTENSION IF NOT EXISTS vector;

CREATE TABLE IF NOT EXISTS document_chunks
(
    id text PRIMARY KEY,

    source text NOT NULL,

    section text NOT NULL,

    content text NOT NULL,

    metadata jsonb,

    embedding vector(1024)
);