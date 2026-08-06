using Npgsql;


namespace Infrastructure.Embeddings;


public class VectorStore
{

private readonly string _connection;


public VectorStore()
{
    _connection =
        Environment.GetEnvironmentVariable(
            "POSTGRES_CONNECTION")!;
}



public async Task Insert(
    string source,
    string content,
    float[] embedding)
{

await using var db =
    new NpgsqlConnection(_connection);


await db.OpenAsync();


var cmd =
new NpgsqlCommand(
"""
INSERT INTO documents
(source,content,embedding)
VALUES
(@source,@content,@embedding)
""",
db);


cmd.Parameters.AddWithValue(
"source",
source);


cmd.Parameters.AddWithValue(
"content",
content);


cmd.Parameters.AddWithValue(
"embedding",
embedding);


await cmd.ExecuteNonQueryAsync();

}

}