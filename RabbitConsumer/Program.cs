using MassTransit;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
.ConfigureServices((_, services) =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<TaskUpdatedConsumer>();
        x.AddConsumer<TaskDeletedConsumer>();
        x.AddConsumer<TaskCreatedConsumer>();

        x.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host("rabbitmq");
            cfg.ReceiveEndpoint("log-queue", e =>
            {
                e.ConfigureConsumer<TaskUpdatedConsumer>(ctx);
                e.ConfigureConsumer<TaskDeletedConsumer>(ctx);
                e.ConfigureConsumer<TaskCreatedConsumer>(ctx);
            });
        });
    });
})
.Build();

await host.RunAsync();
