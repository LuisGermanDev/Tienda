using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//entityframework
builder.Services.AddDbContext<TiendaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TiendaConennection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
