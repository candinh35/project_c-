using System.Text.Json.Serialization;
using Business.Core.Extensions;
using MySolution.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

var service = builder.Services;
var configuration = builder.Configuration;

service.AddDatabase(configuration);
service.AddCoreService(configuration);
service.AddJwtAuthentication(configuration);

// Add services to the container.
service.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.ConfigurationDb();
app.MapControllers();

app.Run();
