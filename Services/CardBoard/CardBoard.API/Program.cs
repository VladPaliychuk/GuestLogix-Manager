using CardBoard.BLL.Interfaces.IRepositories;
using CardBoard.BLL.Repositories;
using CardBoard.DAL.Data;
using CardBoard.DAL.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => { 
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CardBoard.API", Version = "v1" }); 
    }
);

#region Dependencies
builder.Services.AddScoped<ICardBoardContext, CardBoardContext>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c => { 
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CardBoard.API v1"); 
        }
    );
}

app.UseAuthorization();

app.MapControllers();

app.Run();
