using Application.Interfaces;
using Application.Users.Commands;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// Configuración de políticas CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomCORS", policy =>
    {
        policy.WithOrigins(allowedOrigins) // Configura los dominios desde appsettings.json
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ISecurityService, AESCryptoService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddCookie("Cookies", options =>
            {
                options.Cookie.HttpOnly = true; // La cookie no es accesible desde JavaScript
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Requiere HTTPS
                options.Cookie.SameSite = SameSiteMode.None; // Permite envío de cookies entre dominios
                options.Cookie.Name = "AuthCookie"; // Nombre de la cookie
                options.LoginPath = "/auth/login"; // Endpoint para redirigir al inicio de sesión
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Cookies["JwtToken"]; // Leer el token de la cookie
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };

                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                // Configuración de validación del token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"], // Debe coincidir con el 'Issuer' de tu token
                    ValidAudience = builder.Configuration["Jwt:Audience"], // Debe coincidir con el 'Audience' de tu token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])) // La clave secreta para la firma
                };
            });
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingrese el token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API REST - BRO FARMA",
        Description = "API REST para la empresa BRO FARMA",
        Version = "v1"
    });
});

var app = builder.Build();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
    Secure = CookieSecurePolicy.None  // Esto asegura que las cookies no sean seguras en desarrollo
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None, // Necesario para CORS
    Secure = CookieSecurePolicy.Always // Requiere HTTPS
});
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CustomCORS");
app.MapControllers();

app.Run();
