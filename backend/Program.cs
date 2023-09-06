using System.Net;
using System.Text.Json;
using backend.Configurations;
using backend.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllerExtension();


builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
builder.Services.AddCors();

builder.Services.AddAuthenticationPolicies();
builder.Services.AddJWTAuthentication(Configuration);


var app = builder.Build();
app.EnsureDataInit();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
