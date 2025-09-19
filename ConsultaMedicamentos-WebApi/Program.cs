using ConsultaMedicamentos.Application.Services;
using ConsultaMedicamentos.Domain.IRepositories;
using ConsultaMedicamentos.Domain.IServices;
using ConsultaMedicamentos.Infrastructure.config;
using ConsultaMedicamentos.Infrastructure.Data;
using ConsultaMedicamentos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings"));

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

// cors de produccion
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("Produccion",
//        policy =>
//        {
//            policy.WithOrigins("https://tusitio.com") // Reemplaza con el dominio de tu sitio
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});


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

builder.Services.AddSingleton<IEmailService>(sp =>
{
    var smtpSettings = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<SmtpSettings>>().Value;

    return new EmailService(
        smtpServer: smtpSettings.Server,
        smtpPort: smtpSettings.Port,
        smtpUser: smtpSettings.User,
        smtpPass: smtpSettings.Pass,
        fromAddress: smtpSettings.FromAddress
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Desarrollo");
//app.UseCors("Produccion");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
