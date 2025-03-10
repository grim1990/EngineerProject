using Domain.Models;

namespace Application.Interfaces.IServices;

public interface IBudgetService
{
    BudgetModel BudgetPlan(int year);
    BudgetModel BudgetStatus(int year, int month);
}