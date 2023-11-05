using Company.RabitMQ;
using Microsoft.EntityFrameworkCore;
using CompanyRepository.DataBasesContext;
using CompanyRepository.Infrastructure.Company;
using CompanyService.Property.CompanyService;

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
builder.Services.AddTransient<ICompanyService, CompanyServices>();
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
