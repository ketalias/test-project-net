using ASPNetExapp.Services;

// Створюємо екземпляр класу WebApplication, який дозволяє створювати веб-додатки ASP.NET Core
var builder = WebApplication.CreateBuilder(args);

// Додаємо підтримку контролерів
builder.Services.AddControllers();
// Додаємо підтримку Swagger
builder.Services.AddSwaggerGen();
// Додаємо кастомний сервіс
builder.Services.AddSingleton<UserService>();
//Додаю сервіс працівника
builder.Services.AddSingleton<WorkerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); // Важливо для роботи контролерів! Це по суті налаштування маршрутизації

app.Run();
