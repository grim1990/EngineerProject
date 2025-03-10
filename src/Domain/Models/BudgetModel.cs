using Domain.Entities;

namespace Domain.Models;

public class BudgetModel
{
	public ICollection<Category> Categories { get; set; } = new List<Category>();
	public ICollection<MainCategory> MainCategories { get; set; } = new List<MainCategory>();
	public ICollection<Operation> Operations { get; set; } = new List<Operation>();
	public decimal Incomes { get; set; }
	public decimal Expenses { get; set; }
	public decimal PlannedIncomes { get; set; }
	public decimal PlannedExpenses { get; set; }
}
