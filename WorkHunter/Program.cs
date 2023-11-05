using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using CompanyRepository.DataBasesContext;
using CompanyRepository.Infrastructure.Vanancies;
using ServiceLayer.Property.VacanciesService;
using Vacancies.RabitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<VacancyContext>(options => options.UseNpgsql(connection));

builder.Services.AddScoped(typeof(IVananciesLogic<>), typeof(VanancyesLogic<>));
builder.Services.AddScoped(typeof(IResponseLogic<>), typeof(ResponseLogic<>));
builder.Services.AddSingleton<IConnection>(factory =>
{
    var rabbitMqFactory = new ConnectionFactory() { HostName = "localhost" };
    return rabbitMqFactory.CreateConnection();
});

builder.Services.AddSingleton<IModel>(provider =>
{
    var connection = provider.GetRequiredService<IConnection>();
    return connection.CreateModel();
});
builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();
//builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();
builder.Services.AddTransient<IResponceService, ResponceService>();
builder.Services.AddTransient<IVacancyService, VacancyService>();
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
