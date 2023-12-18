using CardBoard.API;
using CardBoard.BLL.Interfaces.IRepositories;
using CardBoard.BLL.Repositories;
using CardBoard.DAL.Data;
using CardBoard.DAL.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => { 
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CardBoard.API", Version = "v1" }); 
    }
);
//builder.Services.AddHealthChecks().AddMongoDb(builder.Configuration["DatabaseSettings:ConnectionString"], "MongoDb Health", HealthStatus.Degraded);

#region Dependencies
builder.Services.AddScoped<ICardBoardContext, CardBoardContext>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
        c => { 
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CardBoard.API v1"); 
        }
    );
}

app.UseRouting();

app.UseAuthorization();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});*/

app.Run();
