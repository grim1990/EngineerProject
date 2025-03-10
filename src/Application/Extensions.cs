using Application.Decorators;
using Application.Helpers;
using Application.Interfaces.IServices;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IMainCategoryService, MainCategoryService>();
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<IOperationService, OperationService>();
        services.AddScoped<ISavingService, SavingService>();

        services.Decorate<IAccountService, AccountServiceDecorator>();
        services.Decorate<ICategoryService, CategoryServiceDecorator>();
        services.Decorate<IMainCategoryService, MainCategoryServiceDecorator>();
        services.Decorate<IOperationService, OperationServiceDecorator>();
        services.Decorate<ISavingService, SavingsServiceDecorator>();

        services.AddSingleton<UserHelper>();

        return services;
    }
}