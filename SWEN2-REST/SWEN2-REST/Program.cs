using SWEN2_REST.BL.Models;
using SWEN2_REST.DAL;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Tours tours = new Tours();
TourContext tourContext = new();
tourContext.LoadTours(tours);

builder.Services.AddControllers();
builder.Services.AddSingleton<Tours>(tours);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
