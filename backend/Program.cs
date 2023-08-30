using System.Text.Json;
using backend.Configurations;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(o=> {
        o.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


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
