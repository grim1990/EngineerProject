using Application.BudgetMapper;
using Application.Commands;
using Application.Interfaces.IServices;
using Domain.Models;

namespace Application.Services;

public class BudgetService : IBudgetService
{
    private readonly IOperationService _operationService;
    private readonly IMainCategoryService _mainCategoryService;
    private readonly ICategoryService _categoryService;

    public BudgetService(IOperationService operationService, IMainCategoryService mainCategoryService, ICategoryService categoryService)
    {
        _operationService = operationService;
        _mainCategoryService = mainCategoryService;
        _categoryService = categoryService;
    }

    public BudgetModel BudgetPlan(int year)
    {
        var currentYear = DateTime.Now.Year;
        var mainCategories = _mainCategoryService.GetAllMainCategoriesAsync().Result;
        mainCategories = mainCategories
            .Where(m => m.CreateDate.Year == currentYear || m.IsAnnual)
            .ToList();

        var categories = _categoryService.GetAllCategoriesAsync().Result;

        if (year != 0)
        {
            mainCategories = mainCategories
                .Where(m => m.CreateDate.Year == year)
                .ToList();

            categories = categories
                .Where(c => c.CreateDate.Year == year)
                .ToList();
        }

        var currentYearIncomes = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == true && c.Type == 1 && c.CreateDate.Year == currentYear);
        var lastYearsIncomes = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == true && c.Type == 1 && c.CreateDate.Year != currentYear);
        var otherIncomes = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == false && c.Type == 1);
        var plannedIncomes = BudgetCommands.CalculateBudgetPlanIncomesOrExpenses(currentYearIncomes, lastYearsIncomes, otherIncomes);

        var currentYearExpenses = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == true && c.Type == 0 && c.CreateDate.Year == currentYear);
        var lastYearsExpenses = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == true && c.Type == 0 && c.CreateDate.Year != currentYear);
        var otherExpenses = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == false && c.Type == 0);
        var plannedExpenses = BudgetCommands.CalculateBudgetPlanIncomesOrExpenses(currentYearExpenses, lastYearsExpenses, otherExpenses);

        var budgetPlan = MapToBudgetModel.MapBudgetPlan(mainCategories, categories, plannedIncomes, plannedExpenses);

        return budgetPlan;
    }

    public BudgetModel BudgetStatus(int year, int month)
    {
        var currentYear = DateTime.Now.Year;
        var mainCategories = _mainCategoryService.GetAllMainCategoriesAsync().Result
            .Where(m => m.CreateDate.Year == currentYear || m.IsAnnual)
            .ToList();
        var categories = _categoryService.GetAllCategoriesAsync().Result;
        var operations = _operationService.GetAllOperationsAsync().Result;

        if (year != 0)
        {
            mainCategories = mainCategories.Where(m => m.CreateDate.Year == year).ToList();
            categories = categories.Where(c => c.CreateDate.Year == year).ToList();
            operations = operations.Where(o => o.CreateDate.Year == year).ToList();
        }
        if (month != 0)
        {
            operations = operations.Where(o => o.Month == month).ToList();
        }

        var incomes = operations.Where(o => o.Type == 1).Sum(o => o.Value);
        var annualIncomes = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == true && c.Type == 1);
        var otherIncomes = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == false && c.Type == 1);
        var plannedIncomes = BudgetCommands.CalculateBudgetStatusIncomesOrExpenses(annualIncomes, otherIncomes);

        var expenses = operations.Where(o => o.Type == 0).Sum(o => o.Value);
        var annualExpenses = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == true && c.Type == 0);
        var otherExpenses = BudgetCommands.CalculateCategoriesSum(categories, c => c.OneOrMonth == false && c.Type == 0);
        var plannedExpenses = BudgetCommands.CalculateBudgetStatusIncomesOrExpenses(annualExpenses, otherExpenses);

        var budgetStatus = MapToBudgetModel.MapBudgetStatus(mainCategories, categories, operations, plannedIncomes, plannedExpenses, incomes, expenses);

        return budgetStatus;
    }
}
