using ConsultaMedicamentos.Application.Services;
using ConsultaMedicamentos.Domain.IRepositories;
using ConsultaMedicamentos.Domain.IServices;
using ConsultaMedicamentos.Infrastructure.Data;
using ConsultaMedicamentos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Desarrollo",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection dependencys
builder.Services.AddScoped<IMedicamentosService, MedicamentosService>();
builder.Services.AddScoped<IMedicamentosRepository, MedicamentosRepository>();
builder.Services.AddScoped<IRegistroEmailService, RegistroEmailService>();
builder.Services.AddScoped<IRegistroEmailRepository, RegistroEmailRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Desarrollo");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
