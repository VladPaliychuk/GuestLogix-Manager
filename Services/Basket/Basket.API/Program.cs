using Basket.API.Repository.Interfaces;
using Basket.API.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
/*builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = Configuration.GetValue<string>(“DatabaseSettings: ConnectionString”);
});*/

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cards API", Version = "v1" });
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI
        (
            c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cards API V1"); }
        );
}

app.UseAuthorization();

app.MapControllers();

app.Run();
