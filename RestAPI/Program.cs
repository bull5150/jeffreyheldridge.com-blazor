
using RestAPI.Interfaces;
using RestAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
});
// Bind configuration section
var blueskyConfig = builder.Configuration.GetSection("Bluesky");
var blueskyUsername = builder.Configuration["Bluesky:Username"];
var blueskyPassword = builder.Configuration["Bluesky:AppPassword"];
var baseUrl = blueskyConfig["BaseUrl"] ?? throw new InvalidOperationException("Bluesky BaseUrl is not configured.");

// Register service + HttpClient
builder.Services.AddHttpClient<IBlueSkyService, BlueSkyService>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(); // Enables serving files from wwwroot
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
