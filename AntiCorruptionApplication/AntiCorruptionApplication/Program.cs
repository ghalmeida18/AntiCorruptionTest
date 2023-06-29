using AntiCorruption.Business.Executor;
using AntiCorruption.Business.Interface;
using AntiCorruption.Data;
using AntiCorruption.Data.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Environment Variables
builder.Configuration.AddEnvironmentVariables();

IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appSettings.json", false)
        .Build();

//Dependency Injection
builder.Services.AddTransient<ICreateRepositoryExecutor, CreateRepositoryExecutor>();

builder.Services.AddTransient<IGitHubRepository, GitHubRepository>();
builder.Services.AddTransient<IListBrachExecutor, ListBrachExecutor>();
builder.Services.AddTransient<ICreateWebhooksExecutor, CreateWebhooksExecutor>();
builder.Services.AddTransient<IListWebHookExecutor, ListWebHookExecutor>();
builder.Services.AddTransient<IUpdateWebHookExecutor, UpdateWebHookExecutor>();

builder.Services.AddSingleton<IConfiguration>(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
