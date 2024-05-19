using GerenciadorDeTarefas.Data;
using GerenciadorDeTarefas.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseMySql(builder.Configuration.GetConnectionString("MYSQL_CONNECTION_STRING"),
              new MySqlServerVersion(
                   new Version(8,0,31)));
});
builder.Services.AddScoped<ITarefasRepository, TarefasRepository>();
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("*")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gerenciador de Tarefas", Version = "v1" });
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.UseCors("AllowSpecificOrigin");

app.Run();
