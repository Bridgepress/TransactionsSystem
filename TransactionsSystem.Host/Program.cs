using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using TransactionsSystem.Api;
using TransactionsSystem.Api.Filters;
using TransactionsSystem.Core.Extensions;
using TransactionsSystem.DataAccess;
using TransactionsSystem.FileHandlers;
using TransactionsSystem.FileHandlers.Interfaces;
using TransactionsSystem.Handlers.Installers;
using TransactionsSystem.Repositories.Implementantion;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        builder.Configuration.GetConnectionString(ApplicationDbContext.ConnectionStringKey),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            SchemaName = "dbo",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(
            builder.Configuration["Logging:LogLevel:Default"] ??
            throw new Exception("Cannot find 'Logging:LogLevel:Default'")))
    .CreateLogger();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddSingleton(Log.Logger);
builder.Logging.AddSerilog();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddScoped<IExcelExtensions, ExcelExtensions>();
builder.Services.AddScoped<ICsvExtensions, CsvExtensions>();
builder.Services.AddInstallersFromAssemblies(builder.Configuration,
    typeof(ApplicationDbContext), typeof(RepositoryManager),
    typeof(HandlersInstaller), typeof(ApiAssemblyMarker));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "docs";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
