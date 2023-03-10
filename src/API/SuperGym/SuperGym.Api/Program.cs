using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SuperGym.Application;
using SuperGym.Application.Utils.Automapper;
using SuperGym.Infra;
using SuperGym.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SuperGymDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SuperGymConnectionString")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inje??o de Dependencias de Repositories e UnityOfWork
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperConfiguration());
}).CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
