using EmailService.Application.Services;
using EmailService.Domain.Entities;
using EmailService.Domain.Interfaces;
using EmailService.Infrastructure.Cache;
using EmailService.Infrastructure.MessageQueue;
using EmailService.Infrastructure.Persistence;
using EmailService.Infrastructure.Settings;
using EmailService.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddControllers();

builder.Services.AddDbContext<EmailContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<Email>, EmailRepository>();
builder.Services.AddScoped<IEmailService, EmailManager>();
builder.Services.AddScoped<IMessageQueue, RabbitMQService>();
builder.Services.AddScoped<ICache, RedisCache>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();