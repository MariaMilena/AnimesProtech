using Crud.Domain.Interfaces;
using Crud.Infrastructure.Data;
using Crud.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqliteConnection(connectionString);
});

// Adiciona o serviço do contexto do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Crud")));

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAnimeDapperRepository, IAnimeDapperRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var myhandlers = AppDomain.CurrentDomain.Load("Crud.Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();
