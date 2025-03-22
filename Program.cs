using Microsoft.EntityFrameworkCore;
using ASPNetExapp;
using ASPNetExapp.Services;
using ASPNetExapp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<WorkerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<WorkerValidationMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();