using Microsoft.OpenApi.Models;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Discount.API.Repositories.Interfaces;
using Discount.API.Repositories;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>

{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Discount.API", Version = "v1" });
});

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration["DatabaseSettings:ConnectionString"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount.API v1"));
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
