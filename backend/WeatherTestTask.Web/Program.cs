using Serilog;
using WeatherTestTask.Application;
using WeatherTestTask.Domain;
using WeatherTestTask.Infrastructure;
using WeatherTestTask.Infrastructure.Data.Extensions;
using WeatherTestTask.Web.Endpoints;
using WeatherTestTask.Web.Extensions;
using WeatherTestTask.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddDomain();

builder.Host.UseSerilog((context, loggerConf) =>
{
    loggerConf.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddMemoryCache();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .WithOrigins("http://localhost")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandlers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseCors("AllowReactApp");
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSession();
app.UseMiddleware<SaveSessionIdMiddleware>();

app.MapWeatherEndpoints();

app.Run();