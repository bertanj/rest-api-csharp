using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Mapper;
using RestWithASPNET.MiddleWare.Handler;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Services;
using RestWithASPNET.Services.V1;

var builder = WebApplication.CreateBuilder(args);

// 1. --- CONFIGURA��O DA CONEX�O COM O BANCO DE DADOS ---
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection, MySqlServerVersion.AutoDetect(connection))
);

// 2. --- CONFIGURA��O DO VERSIONAMENTO DE API ---
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
})
.AddApiExplorer(options     =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// 3. --- CONFIGURA��O DA INJE��O DE DEPEND�NCIA ---
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// 4. --- CONFIGURA��O DO AUTOMAPPER ---
builder.Services.AddAutoMapper(typeof(MappingProfile));


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- CONFIGURA��O DO PIPELINE DE REQUISI��ES ---

// ADICIONE SEU MIDDLEWARE AQUI, NO IN�CIO DO PIPELINE
app.UseMiddleware<ExceptionHandlerMiddleWare>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();