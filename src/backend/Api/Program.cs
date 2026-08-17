// To use:
// http://localhost:5179/swagger/index.html
// http://localhost:5179/api/tasks/
using System.Reflection;
using Api.Services;
using Infrastructure.Configuration;
using Infrastructure.LLM;
//using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// use global config file for all projects in solution
builder.Configuration.AddConfiguration(AppConfiguration.Build());

builder.Services.Configure<LlmOptions>(
    builder.Configuration.GetSection("Llm"));

// Test global configuration values
//Console.WriteLine($"Llm Provider: {builder.Configuration["Llm:Provider"]}");
//Console.WriteLine($"Llm Model: {builder.Configuration["Llm:Model"]}");
//using var testProvider =
//    builder.Services.BuildServiceProvider();
//var options =
//    testProvider
//        .GetRequiredService<IOptions<LlmOptions>>()
//        .Value;
//Console.WriteLine($"DI Llm Provider: {options.Provider}");
//Console.WriteLine($"DI Llm Model: {options.Model}");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
//builder.Services.AddHttpClient<GeminiClient>();
//builder.Services.AddHttpClient<CerebrasClient>();

// Controllers
builder.Services.AddControllers();
builder.Services.AddSingleton<IQuestionService, QuestionService>();
builder.Services.AddSingleton<LlmClientFactory>();

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi(); // http://localhost:5179/openapi/v1.json
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.MapFallbackToFile("404.html"); // evt. index.html

app.Run();