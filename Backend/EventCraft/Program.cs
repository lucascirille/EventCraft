using DataBase.Data;
using Servicios.Interfaces;
using Servicios.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:5173") // Especifica el origen del frontend
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()); // Permitir credenciales (cookies)

});

// Add services to the container.

//var servidor = builder.Configuration.GetValue<string>("BDPostgre:Servidor");
//var puerto = builder.Configuration.GetValue<string>("BDPostgre:Puerto");
//var baseDeDatos = builder.Configuration.GetValue<string>("BDPostgre:BaseDeDatos");
//var usuario = builder.Configuration.GetValue<string>("BDPostgre:Usuario");
//var clave = builder.Configuration.GetValue<string>("BDPostgre:Clave");

//var connectionString = "Server=" + servidor + ";Database=" + baseDeDatos + ";Port=" + puerto + ";User ID=" + usuario + ";Password=" + clave + ";";

builder.Services.AddDbContext<EventCraftContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<ISalonService, SalonService>();
builder.Services.AddScoped<ICaracteristicaService, CaracteristicaService>();
builder.Services.AddScoped<ISalonCaracteristicaService, SalonCaracteristicaService>();
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<ISalonServicioService, SalonServicioService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>(); 
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IReservaServicioService, ReservaServicioService>();
builder.Services.AddScoped<IJWT, JWT>();

builder.Services.AddSwaggerGen(c => { //Autenticar en Swagger
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EVENTCRAFT", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Encabezado de autorización JWT utilizando el esquema Bearer. \r\n\r\n
                          Ingresamos la palabra 'Bearer', luego un espacio y el token.
                          \r\n\r\nPor ejemplo: 'Bearer fedERGeWefrt5t45e5g5g4f643333uyhr'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        }
    );
});

builder.Services.AddAuthentication(opcionesDeAutenticacion =>
{
opcionesDeAutenticacion.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
opcionesDeAutenticacion.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => //para login de nuestra pagina
{
options.TokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuer = false, //nosotros
    ValidateAudience = false, //no tenemos
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? string.Empty)
    )
};
});

// Agregar servicios de caché distribuida y sesiones
//builder.Services.AddDistributedMemoryCache(); // Esto habilita la caché en memoria para sesiones
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30); // Duración de la sesión
//    options.Cookie.HttpOnly = true; // Solo accesible desde el servidor
//    options.Cookie.IsEssential = true; // Necesario para asegurar que las sesiones funcionen sin importar la política de cookies
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowSpecificOrigin");

// Middleware de sesiones
//app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();