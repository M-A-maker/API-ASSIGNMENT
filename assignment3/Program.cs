using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "assignment3 API",
		Version = "v1",
		Description = "API documentation for assignment3"
	});
});

// Add CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

var app = builder.Build();

// Enable Swagger (always)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint(
		"http://localhost:5000/swagger/v1/swagger.json",
		"assignment3 API v1"
	);
	c.RoutePrefix = string.Empty; // Swagger at root
});


app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Run the app
app.Run("http://localhost:5000");

