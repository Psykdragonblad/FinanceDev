using FinanceDev.API.Helpers;
using FinanceDev.API.Middlewares;
using FinanceDev.Application.Services;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Domain.Interface.Service;
using FinanceDev.Application.Interface;
using FinanceDev.Infra.Repositories;
using FinanceDev.Infraestructure;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var defaultCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //Define o formato de data padrão no JSON
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

        // Formato de data: dd/MM/yyyy
        options.JsonSerializerOptions.Converters.Add(
            new DateTimeConverterUsingDateTimeParse("dd/MM/yyyy"));
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IMesVencimentoService, MesVencimentoService>();
builder.Services.AddScoped<IDI1CurvaService, DI1Service>();
 
builder.Services.AddScoped<IMesVencimentoRepository, MesVencimentoRepository>();
builder.Services.AddScoped<IDI1CurvaRepository, DI1CurvaRepository>();
builder.Services.AddScoped<IReferenciaCurvaRepository, ReferenciaCurvaRepository>();
builder.Services.AddScoped<IFeriadoRepository, FeriadoRepository>();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

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
