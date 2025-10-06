using FinanceDev.API.Middlewares;
using FinanceDev.Application.Services;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Domain.Interface.Service;
using FinanceDev.Infra.Repositories;
using FinanceDev.Infraestructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IMesVencimentoService, MesVencimentoService>();
builder.Services.AddScoped<IDI1CurvaService, DI1Service>();

builder.Services.AddScoped<IMesVencimentoRepository, MesVencimentoRepository>();
builder.Services.AddScoped<IDI1CurvaRepository, DI1CurvaRepository>();
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
