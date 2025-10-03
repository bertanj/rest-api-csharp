using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Services;
using RestWithASPNET.Services.V1;

var builder = WebApplication.CreateBuilder(args);

// 1. --- CONFIGURAÇÃO DA CONEXÃO COM O BANCO DE DADOS ---

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection, MySqlServerVersion.AutoDetect(connection))
);

// 2. --- CONFIGURAÇÃO DO VERSIONAMENTO DE API ---
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// 3. --- CONFIGURAÇÃO DA INJEÇÃO DE DEPENDÊNCIA ---
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<IBookService, BookService>();
 builder.Services.AddScoped<IBookRepository, BookRepository>();


// Add services to the container.
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();