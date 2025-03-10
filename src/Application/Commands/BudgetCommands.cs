using Domain.Entities;

namespace Application.Commands;

public static class BudgetCommands
{
    public static decimal CalculateBudgetStatusIncomesOrExpenses(decimal annualIncomes, decimal otherIncomes) => (annualIncomes * (13 - DateTime.Now.Month)) + otherIncomes;

    public static decimal CalculateBudgetPlanIncomesOrExpenses(decimal currentYearSum, decimal lastYearsSum, decimal otherSum)
    {
        lastYearsSum = (lastYearsSum * 12) + otherSum;
        currentYearSum *= (13 - DateTime.Now.Month);

        return lastYearsSum + currentYearSum;
    }

    public static decimal CalculateCategoriesSum(IEnumerable<Category> categories, Func<Category, bool> condition)
    {
        return categories
            .Where(condition)
            .Sum(c => c.Value);
    }
}
