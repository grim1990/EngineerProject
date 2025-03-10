using Microsoft.Extensions.Configuration;
using Quartz;

namespace Infrastructure.Email.Jobs;

public static class Extensions
{
    private const string DefaultCronExpression = "0 0 0 * * ?";

    public static void AddSendReminderEmailsJob(this IServiceCollectionQuartzConfigurator configurator, IConfiguration configuration)
    {
        var cronExpression = configuration.GetSection("Smtp:CronExpression").Value ?? DefaultCronExpression;

        var jobKey = JobKey.Create(nameof(RemainderEmailJob));

        configurator
            .AddJob<RemainderEmailJob>(jobKey)
            .AddTrigger(trigger =>
                trigger.ForJob(jobKey)
                .WithCronSchedule(cronExpression));
    }
}