﻿using Calabonga.ConsoleAppAdvanced;
using Calabonga.ConsoleAppAdvanced.OrderEntity.Actions;
using Calabonga.ConsoleAppAdvanced.OrderEntity.Rules;
using Calabonga.ConsoleAppAdvanced.Processors;
using Calabonga.ConsoleAppAdvanced.Services;
using Calabonga.EntityProcessor;
using Calabonga.EntityProcessor.Extensions;
using Calabonga.EntityProcessor.Rules;
using Calabonga.Shared.OrderEntity;
using Calabonga.Shared.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

#region configuration with logger

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
    .Build();

// logger from Serilog
var serilogLogger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Console().CreateLogger();

// services
var services = new ServiceCollection();
services.AddLogging(x => x.AddSerilog(serilogLogger));
services.Configure<AppSettings>(x => { configuration.GetSection(nameof(AppSettings)).Bind(x); });

#endregion

#region registering dependency and build container

// controllers
services.AddScoped<ControllerEmulator>();

// services
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<IStatisticService, StatisticService>();
services.AddScoped<INotificationService, NotificationService>();

services.AddMediatR(typeof(Program));

// processors
services.AddEntityProcessor<OrderEntityProcessor>(
    configuration
        .GetSection($"{nameof(EntityProcessorConfiguration)}:Orders")
        .Get<EntityProcessorConfiguration>());

#region anotner way to configure

// services.AddEntityProcessor<OrderEntityProcessor>(config =>
// {
// config.SkipRuleDuplicates = true;
// config.AutoFireDomainEvents = true;
// });

#endregion


// adding rules
services.AddScoped<IRule<Order>, CheckTitleOrderRule>();
services.AddScoped<IRule<Order>, CheckCreatedDateExistsOrderRule>();
services.AddScoped<IRule<Order>, CheckCreatedInThePastOrderRule>();
services.AddScoped<IRule<Order>, CheckPriceOrderRule>();


// adding commands and notifications
services.AddScoped<ChangeStateToPublishedAction>();

var container = services.BuildServiceProvider();

#endregion

#region getting dependencies like in the controller constructor

var controller = container.GetRequiredService<ControllerEmulator>();
var logger = container.GetRequiredService<ILogger<Program>>();
var appSettings = container.GetRequiredService<IOptions<AppSettings>>();

#endregion

logger.LogInformation("App {Name} started", appSettings.Value.ApplicationName);

await controller.CreateOrderAsync();


// Order 1
var order = new Order
{
    Id = 1,
    CreatedAt = DateTime.UtcNow,
    Description = "No description",
    State = OrderState.Draft,
    Title = "Title 1",
    Price = 10,
    IsEnabled = false
};

await controller.UpdateOrderAsync(order);