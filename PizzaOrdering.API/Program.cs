using Microsoft.EntityFrameworkCore;
using PizzaOrdering.Application;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Infrastructure.Data;
using PizzaOrdering.Infrastructure.Repositories;
using PizzaOrdering.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Configure Entity Framework Core with SQLite
builder.Services.AddDbContext<PizzaOrderingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=PizzaOrdering.db"));

// Register Repositories
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IToppingRepository, ToppingRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Register Services
builder.Services.AddScoped<IOrderService, OrderService>();

// Add MediatR for CQRS pattern - Register from Application assembly
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly);
});

// Configure OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PizzaOrderingDbContext>();
    await DbSeeder.SeedAsync(context);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
