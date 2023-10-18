using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataBasesContext;
using RepositoryLayer.Infrastructure.Vanancies;
using RepositoryLayer.Infrastructure.Worker;
using ServiceLayer.Property.VacanciesService;
using ServiceLayer.Property.WorkerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<VacancyContext>(options => options.UseNpgsql(connection));

builder.Services.AddScoped(typeof(IVananciesLogic<>), typeof(VanancyesLogic<>));
builder.Services.AddScoped(typeof(IResponseLogic<>), typeof(ResponseLogic<>));
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
