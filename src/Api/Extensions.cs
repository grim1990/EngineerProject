using Application.ModelBinders;
using Infrastructure;
using System.Globalization;

namespace Api;

public static class Extensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton(typeof(Microsoft.Extensions.Logging.ILogger), typeof(Logger<Program>));
        services.AddLogging();

        services.AddControllersWithViews(options =>
        {
            options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddInfrastructureServices(config);

        return services;
    }

    public static CultureInfo PLCultureInfoForNumberFormat()
    {
        var defaultDateCulture = "pl-PL";
        var cultureInfo = new CultureInfo(defaultDateCulture);
        cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
        cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";

        return cultureInfo;
    }
}
