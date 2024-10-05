using Microsoft.EntityFrameworkCore;
using Proyecto_Zetta.DB.Data;
using Proyecto_Zetta.Server.Repositorio;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();//agrega primero
builder.Services.AddControllers().AddJsonOptions(//agrega primero
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IPresupuestoRepositorio, PresupuestoRepositorio>();

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
