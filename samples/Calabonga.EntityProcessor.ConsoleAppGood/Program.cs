using Calabonga.ConsoleApp;
using Calabonga.ConsoleApp.Services;
using Calabonga.Shared.OrderEntity;
using Calabonga.Shared.Services;
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

// services
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<IStatisticService, StatisticService>();
services.AddScoped<INotificationService, NotificationService>();

var container = services.BuildServiceProvider();

#endregion

#region getting dependencies like in the controller constructor

var logger = container.GetRequiredService<ILogger<Program>>();
var appSettings = container.GetRequiredService<IOptions<AppSettings>>();
var orderService = container.GetRequiredService<IOrderService>();
var statisticService = container.GetRequiredService<IStatisticService>();
var notificationService = container.GetRequiredService<INotificationService>();

#endregion

logger.LogInformation("App {Name} started", appSettings.Value.ApplicationName);

// create Order 
var order = new Order
{
    Id = 1,
    CreatedAt = DateTime.UtcNow,
    Description = "No description",
    State = OrderState.Draft,
    Title = "Title",
    Price = 10,
    IsEnabled = false
};

logger.LogInformation("{Name} successfully created", nameof(Order));

// update state
logger.LogInformation("Updating {Name} {Id} state to {State}", nameof(Order), order.Id, nameof(OrderState.WaitingPayment));
var operation = await orderService.UpdateStateAsync(OrderState.WaitingPayment, order);
if (operation.Ok)
{
    logger.LogInformation("{Name} {Id} state successfully changed to {State}", nameof(Order), order.Id, nameof(OrderState.WaitingPayment));
    logger.LogInformation("Update state OK. Starting statistic re-calculation");
    await notificationService.NotifySuccessAsync();
    await statisticService.StartRecalculationAsync();
}
else
{
    logger.LogInformation("{Name} {Id} state updating to {State} is FAILED", nameof(Order), order.Id, nameof(OrderState.WaitingPayment));
    // we should update title
    logger.LogInformation("Update status FAILED. Sending email notification");
    await notificationService.NotifyErrorAsync("Update state ERROR");
}

logger.LogInformation("App {Name} successfully exit", appSettings.Value.ApplicationName);