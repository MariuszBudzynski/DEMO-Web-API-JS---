using DEMO_Web_API_JS.Data;
using DEMO_Web_API_JS.Models;
using DEMO_Web_API_JS.Repository;
using DEMO_Web_API_JS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NotesDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Notes")));
builder.Services.AddScoped<INotesRepository,NotesRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//this is required for our simple client to talk to our API
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
