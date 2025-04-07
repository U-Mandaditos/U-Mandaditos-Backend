using System.Text;
using API.Configuration;
using API.Hubs;
using API.Services;
using Aplication.Interfaces.Helpers;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;
using Infrastructure.Configuration;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración desde variables de entorno y appsettings.json
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var dbConfig = new DatabaseConfig();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:3000", "http://127.0.0.1:3000") // Origen de tu frontend
            .AllowAnyMethod() // Permite todos los métodos (GET, POST, etc.)
            .AllowAnyHeader() // Permite cualquier cabecera
            .AllowCredentials()); // Opcional: Si usas cookies o autenticación
});

// Introduction message to show in console
string? version = builder.Configuration[key: "version"];
if (version != null)
{
    Console.WriteLine($"U-Mandaditos API v{version}");
}
else
{
    Console.WriteLine("U-Mandaditos API version not found");
}

Console.WriteLine($"Up at {DateTime.Now}");

// Testing database connection
if (dbConfig.TestConnection())
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Connection successfully to U-Mandaditos database\n");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Connection failed to U-Mandaditos database\n");
}
Console.ResetColor();

// Cargar configuracion de Jwt para Autenticación

// Configurar el binding de las variables de entorno para JwtSettings
builder.Services.Configure<JwtSettings>(options =>
{
    options.SecretKey = builder.Configuration["JWT_SECRET_KEY"];
    options.ExpirationMinutes = double.Parse(builder.Configuration["JWT_EXPIRATION_MINUTES"] ?? "15.0"); 
});

builder.Services.AddAuthorization();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        // Aquí, usamos builder.Configuration para acceder a las configuraciones directamente.
        var secretKey = builder.Configuration["JWT_SECRET_KEY"];

        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("La clave secreta JWT no está configurada.");
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = signingKey
        };

        // Personalizar error
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // Personalizar el error 401 (Unauthorized)
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(
                    "{ \"success\": false, \"message\": \"Token inválido, expirado o no existente\"}"
                );
            }
        };
});

// Add services to the container.
builder.Services.AddDbContext<BackendDbContext>(options => options.UseSqlServer(dbConfig.ConnectionString));

builder.Services.AddInfrastructure();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<HubRequest>("/hubRequest");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
