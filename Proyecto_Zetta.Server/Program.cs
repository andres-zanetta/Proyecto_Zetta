using Proyecto_Zetta.DB.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Proyecto_Zetta.Server.repositorio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



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
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();


app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
