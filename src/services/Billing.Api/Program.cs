using Billing.Api.Consumers;
using Billing.Api.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BillingDbContext>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});

builder.Services.AddCap(x =>
{
  x.UseEntityFramework<BillingDbContext>();
  x.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
  x.UseRabbitMQ(builder.Configuration.GetConnectionString("RabbitMqConn"));
  x.UseDashboard(x => x.PathMatch = "/CapDashboard");
});

// dinleyici olarak tanýmla
// Net Core ait service Provider yapýsýný kullandýðýndan dinleme için gerekli consumer servisleri tanýmlýyoruz.
builder.Services.AddScoped<OrderCompletedConsumer>();


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
