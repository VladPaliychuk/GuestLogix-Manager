using BasketProducts.API;
using BasketProducts.API.Data;
using BasketProducts.API.Data.Interfaces;
using BasketProducts.API.Repository;
using BasketProducts.API.Repository.Interfaces;
using BasketProducts.API.Services;
using BasketProducts.API.Services.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IBasketContext, BasketContext>();
builder.Services.AddSingleton<ICachingService, CachingService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketProducts.API", Version = "v1" });
});

//builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("DatabaseSettings"));
var client = new MongoClient("mongodb://localhost:27019");
var database = client.GetDatabase("BasketProductsDb");
builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration["DatabaseSettings:ConnectionString"],
        "MongoDb Health", HealthStatus.Degraded);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketProducts.API v1"));
}

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.Run();