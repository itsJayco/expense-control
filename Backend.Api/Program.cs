using Backend.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ========================================================
// 1. CONFIGURE SERVICES
// ========================================================

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// CORS - Permitir Angular en desarrollo
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ========================================================
// 2. CONFIGURE MIDDLEWARE
// ========================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔹 Activar CORS antes de Authorization
app.UseCors("AllowAngularDev");

app.UseAuthorization();

app.MapControllers();

// ========================================================
// 3. SEED DATABASE (Solo una vez al inicio de la app)
// ========================================================

Console.WriteLine(">>> SEED: Inicio del proceso");

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine(">>> SEED: Scope creado correctamente");

    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Console.WriteLine(">>> SEED: DbContext obtenido");

    DbInitializer.Initialize(db);
    Console.WriteLine(">>> SEED: Ejecutado con éxito");
}

Console.WriteLine(">>> SEED: Finalizado");

// ========================================================
// 4. RUN APPLICATION
// ========================================================

app.Run();
