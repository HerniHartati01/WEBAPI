using Microsoft.EntityFrameworkCore;
using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration
                               .GetConnectionString("DefaultConnection");
// Configurasi project
builder.Services.AddDbContext<BookingMangementDbContext>(options => options.UseSqlServer(connectionString));

// Add Repository to the container.
builder.Services.AddScoped<IRepositoryGeneric<University>, RepositoryGeneric<University>>();
builder.Services.AddScoped<IRepositoryGeneric<Room>, RepositoryGeneric<Room>>();
builder.Services.AddScoped<IRepositoryGeneric<Education>, RepositoryGeneric<Education>>();
builder.Services.AddScoped<IRepositoryGeneric<Employee>, RepositoryGeneric<Employee>>();
builder.Services.AddScoped<IRepositoryGeneric<Account>, RepositoryGeneric<Account>>();
builder.Services.AddScoped<IRepositoryGeneric<AccountRole>,RepositoryGeneric<AccountRole>>();
builder.Services.AddScoped<IRepositoryGeneric<Booking>, RepositoryGeneric<Booking>>();
builder.Services.AddScoped<IRepositoryGeneric<Role>, RepositoryGeneric<Role>>();




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

app.UseAuthorization();

app.MapControllers();

app.Run();
