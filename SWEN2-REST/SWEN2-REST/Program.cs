using SWEN2_REST.BL.Models;
using SWEN2_REST.DAL;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Tours tours = new Tours();
TourContext tourContext = new();
tourContext.LoadTours(tours);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
