using System.Reflection;
using tasks_ASPdotnetAPI;
using tasks_ASPdotnetAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем конфигурации
builder.Services.Configure<RandomApiSettings>(builder.Configuration);
builder.Services.Configure<JsonSettings>(builder.Configuration.GetSection("Settings"));

// Получаем экземпляр JsonSettings для использования значения ParallelLimit
var jsonSettings = builder.Configuration
    .GetSection("Settings")
    .Get<JsonSettings>();

// Регистрируем RequestLimiterService с лимитом
builder.Services.AddSingleton(new RequestLimiterService(jsonSettings.ParallelLimit));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// XML doc in swagger
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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



