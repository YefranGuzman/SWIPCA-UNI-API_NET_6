using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;
using static SWIPCA_UNI_API.DataAccess.DA_Usuario;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar cadena de conexión
string connectionString = "Server=LAPTOP-0LOPLPE0;Database=XYZ;User Id=Administrador;Password=123;TrustServerCertificate=True;Trusted_Connection=True;";

// Agregar DbContext
builder.Services.AddDbContext<DbCargaAcademicaContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar servicios de autenticación e Identity
builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.AllowedUserNameCharacters = null; // Permite cualquier carácter en el nombre de usuario
    options.User.RequireUniqueEmail = false; // Permite correos electrónicos duplicados
})
    .AddEntityFrameworkStores<DbCargaAcademicaContext>()
    .AddDefaultTokenProviders();

// Agregar servicios DI
builder.Services.AddScoped<DA_Usuario>();
builder.Services.AddScoped<DA_Turno>();
builder.Services.AddScoped<DA_Grupo>();
builder.Services.AddScoped<DA_Carrera>();
builder.Services.AddScoped<DA_Docentes>();
builder.Services.AddScoped<DA_Disponibilidad>();
builder.Services.AddScoped<DA_Clase>();
builder.Services.AddScoped<DA_CargaAcademica>();
builder.Services.AddScoped<DA_Aula>();
builder.Services.AddScoped<DA_Asignatura>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<UserManager<Usuario>>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:58603", "http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
