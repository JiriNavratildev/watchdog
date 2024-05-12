using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Purple.Model;
using Purple.Model.Repositories;
using Purple.Watchdog;
using Purple.Watchdog.Deals;
using Purple.Watchdog.Monitors;
using Purple.Watchdog.Notifiers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MockNotifier>();
builder.Services.AddScoped<ISimilarDealsMonitor, SimilarDealsMonitor>();
builder.Services.AddScoped<IWatchdogService, WatchdogService>();
builder.Services.AddHostedService<DealProcessor>();

builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")); });
builder.Services.AddSingleton(_ => new ServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus"), new ServiceBusClientOptions { TransportType = ServiceBusTransportType.AmqpWebSockets }));
builder.Services.AddScoped<IOrderRepository, OrderRepository>(); 
builder.Services.AddScoped<ISimilarDealRepository, SimilarDealRepository>();
builder.Services.AddSingleton<SimilarDealConfiguration>(_ =>
    builder.Configuration.GetSection("SimilarDealConfiguration").Get<SimilarDealConfiguration>()!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();