using ASPNetExapp.Models;

namespace ASPNetExapp.Middlewares;

public class WorkerValidationMiddleware
{
    private readonly RequestDelegate _next;

    public WorkerValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == "POST" || context.Request.Method == "PUT")
        {
            if (context.Request.Path.StartsWithSegments("/api/worker"))
            {
                context.Request.EnableBuffering();
                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                var worker = System.Text.Json.JsonSerializer.Deserialize<Worker>(body);
                if (worker != null)
                {
                    if (worker.RoomNumber < 0)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync($$"""{"status": 400, "error": "Room number must be a positive number"}""");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(worker.LastName) || worker.LastName.Length < 2)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync($$"""{"status": 400, "error": "LastName must be at least 2 characters long"}""");
                        return;
                    }
                    if (worker.Department != null && (string.IsNullOrWhiteSpace(worker.Department) || worker.Department.Length < 2))
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync($$"""{"status": 400, "error": "Department must be at least 2 characters long"}""");
                        return;
                    }
                    if (worker.ComputerInfo != null && (string.IsNullOrWhiteSpace(worker.ComputerInfo) || worker.ComputerInfo.Length < 2))
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync($$"""{"status": 400, "error": "ComputerInfo must be at least 2 characters long"}""");
                        return;
                    }
                }
            }
        }
        await _next(context);
    }
}