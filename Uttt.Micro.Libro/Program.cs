using Microsoft.EntityFrameworkCore;
using Uttt.Micro.Libro.Extenciones;
using Uttt.Micro.Libro.Persistencia;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();    
builder.Services.AddSwaggerGen();  

builder.Services.AddDbContext<ContextoLibreria>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});

app.MapOpenApi(); 

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using Uttt.Micro.Libro.Extenciones;
//using Uttt.Micro.Libro.Persistencia;

//var builder = WebApplication.CreateBuilder(args);
////var secretKey = builder.Configuration["ApiSettings:JwtOptions:Secret"];
////var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

////builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
////    .AddJwtBearer(options =>
////    {
////        options.TokenValidationParameters = new TokenValidationParameters
////        {
////            ValidateIssuer = true,
////            ValidateAudience = true,
////            ValidateLifetime = true,
////            ValidateIssuerSigningKey = true,
////            ValidIssuer = builder.Configuration["ApiSentings:JwtOptions:Issuer"],
////            ValidAudience = builder.Configuration["ApiSentings:JwtOptions:Audience"],
////            IssuerSigningKey = key
////        };
////    });

//// Agregar controladores
//builder.Services.AddControllers();

//// Configurar Swagger y OpenAPI
//builder.Services.AddOpenApi();     // Extensión personalizada
//builder.Services.AddSwaggerGen();  // Swagger de Swashbuckle

//// 🔒 Cadena de conexión directamente en el código
//var connectionString = "Server=sqlserver_dev,1433;Database=libreriaUT;User Id=sa;Password=TuPasswordSegura123@;Encrypt=False;TrustServerCertificate=True;";
//Console.WriteLine($"🔧 Usando cadena de conexión hardcoded: {connectionString}");

//// Configurar EF Core con SQL Server y la cadena hardcoded
//builder.Services.AddDbContext<ContextoLibreria>(options =>
//    options.UseSqlServer(connectionString));

////builder.Services.AddScoped<ILibroRespository, LibroRepository>();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("PermitirFrontend",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:3001") // Cambia al puerto real del frontend
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});

//// Servicios personalizados (como validadores, etc.)
//builder.Services.AddCustomServices(builder.Configuration);

//var app = builder.Build();

//app.UseCors("PermitirFrontend");

//// Swagger solo en entorno de desarrollo
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.MapOpenApi(); // OpenAPI personalizado
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.UseSwagger();
//app.UseSwaggerUI();

//app.MapControllers();

//app.Run();
