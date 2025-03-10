using Application;
using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Email;
using Infrastructure.Email.Jobs;
using Infrastructure.Email.Options;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EmailOptions>(config.GetSection(EmailOptions.Smtp));

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IMainCategoryRepository, MainCategoryRepository>();
        services.AddScoped<IOperationRepository, OperationRepository>();
        services.AddScoped<ISavingRepository, SavingRepository>();

        services.AddScoped<DataSeeder>();
        services.AddScoped<IEmailSender, EmailSender>();

        services.AddQuartz(options =>
            options.AddSendReminderEmailsJob(config)
        );
        services.AddQuartzHostedService();

        services.AddDbContext<DataContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Api")));
        services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(config.GetConnectionString("IdentityDbContextConnection"), b => b.MigrationsAssembly("Api")));

        services.AddDefaultIdentity<IdentityUserModel>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddGoogle(options =>
        {
            IConfigurationSection googleAuthSection =
            config.GetSection("Authentication:Google");
            options.ClientId = googleAuthSection["ClientId"]!;
            options.ClientSecret = googleAuthSection["ClientSecret"]!;

            options.BackchannelHttpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
        });

        services.AddApplicationServices();

        return services;
    }
}