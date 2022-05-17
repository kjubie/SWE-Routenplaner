using SWEN2_REST.BL;
using SWEN2_REST.BL.Models;
using SWEN2_REST.DAL;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Logging.AddLog4Net();

builder.Services.AddControllers();
builder.Services.AddSingleton<Tours>();
builder.Services.AddSingleton<MapQuestContext>();
builder.Services.AddSingleton<TourContext>();
builder.Services.AddSingleton<FileHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (builder.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();