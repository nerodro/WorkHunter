using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataBasesContext;
using RepositoryLayer.Infrastructure.Company;
using RepositoryLayer.Infrastructure.Worker;
using ServiceLayer.Property.CompanyService;
using ServiceLayer.Property.WorkerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<WorketContext>(options => options.UseNpgsql(connection));

builder.Services.AddScoped(typeof(IUserLogic<>), typeof(UserLogic<>));
builder.Services.AddScoped(typeof(ICVLogic<>), typeof(CVLogic<>));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICVService, CVService>();
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
