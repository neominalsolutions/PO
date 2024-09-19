using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Api.Data;
using PurchaseOrder.Api.Data.Repositories;
using PurchaseOrder.Api.Domain.Aggregates.POAggregate;
using PurchaseOrder.Api.Domain.Aggregates.PRAggregate;
using PurchaseOrder.Api.SeedWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PODbContext>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));

});

builder.Services.AddCap(x =>
{
  x.UseEntityFramework<PODbContext>();
  x.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
  x.UseRabbitMQ(builder.Configuration.GetConnectionString("RabbitMqConn"));
  x.UseDashboard(opt => { opt.PathMatch = "/CapDashboard"; });
});

builder.Services.AddScoped<IUnitOfWork, POUnitOfWork>();
builder.Services.AddScoped<IPurchaseRequestRepository, PurchaseRequestRepo>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepo>();



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


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
