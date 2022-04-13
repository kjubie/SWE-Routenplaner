using SWEN2_REST.BL;
using SWEN2_REST.BL.Models;
using SWEN2_REST.DAL;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
MapQuestContext mapQuestContext = new();
Tours tours = new();
TourContext tourContext = new();
tourContext.LoadTours(tours);

builder.Services.AddControllers();
builder.Services.AddSingleton<Tours>(tours);
builder.Services.AddSingleton<MapQuestContext>(mapQuestContext);
builder.Services.AddSingleton<TourContext>(tourContext);
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