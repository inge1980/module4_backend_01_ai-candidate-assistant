CREATE EXTENSION IF NOT EXISTS vector;


CREATE TABLE documents
(
 id SERIAL PRIMARY KEY,

 source TEXT NOT NULL,

 content TEXT NOT NULL,

 embedding vector(1536)
);