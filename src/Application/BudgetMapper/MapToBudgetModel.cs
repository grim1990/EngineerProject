using Domain.Entities;
using Domain.Models;

namespace Application.BudgetMapper;

public static class MapToBudgetModel
{
    public static BudgetModel MapBudgetPlan(List<MainCategory> mainCategories, List<Category> categories, decimal plannedIncomes, decimal plannedExpenses) =>
        new()
        {
            MainCategories = mainCategories,
            Categories = categories,
            PlannedIncomes = plannedIncomes,
            PlannedExpenses = plannedExpenses,
        };

    public static BudgetModel MapBudgetStatus(List<MainCategory> mainCategories, List<Category> categories, List<Operation> operations,
        decimal plannedIncomes, decimal plannedExpenses, decimal incomes, decimal expenses) =>
        new()
        {
            MainCategories = mainCategories,
            Categories = categories,
            Operations = operations,
            Incomes = incomes,
            PlannedIncomes = plannedIncomes,
            Expenses = expenses,
            PlannedExpenses = plannedExpenses,
        };
}
