using Company.RabitMQ;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataBasesContext;
using RepositoryLayer.Infrastructure.Company;
using RepositoryLayer.Infrastructure.Role;
using ServiceLayer.Property.CompanyService;
using ServiceLayer.Property.RoleService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<CompanyContext>(options => options.UseNpgsql(connection));

//builder.Services.AddHealthChecks().ForwardToPrometheus();

builder.Services.AddScoped(typeof(ICompanyLogic<>), typeof(CompanyLogic<>));
builder.Services.AddScoped<IRabitMQListener, RabitMQListener>();
builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
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
