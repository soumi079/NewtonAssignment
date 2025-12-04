using DLCGGameCore.Application.Interfaces;
using DLCGGameCore.Application.Mappings;
using DLCGGameCore.Application.Services;
using DLCGGameCore.Infrastructure.Data;
using DLCGGameCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options => options.AddPolicy("AllowAngular", builder =>
{
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod();
}));

builder.Services.AddControllers();

//builder.Services.AddAutoMapper(typeof(VideoGameProfile));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new VideoGameProfile());
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddScoped<IVideoGameRepository, VideoGameRepository>();

builder.Services.AddScoped<IVideoGameService, VideoGameService>();
builder.Services.AddMemoryCache();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();    
}

app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }