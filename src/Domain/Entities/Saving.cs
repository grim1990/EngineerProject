using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Saving
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	[RegularExpression(@"^\d+(?:[,.]\d{0,2})?$", ErrorMessage = "Należy wprowadzić poprawną wartość, który może zawierać znak '.' lub ','")]
	public decimal AimValue { get; set; }
	[RegularExpression(@"^\d+(?:[,.]\d{0,2})?$", ErrorMessage = "Należy wprowadzić poprawną wartość, który może zawierać znak '.' lub ','")]
	public decimal Value { get; set; }
	public int BudgetId { get; set; }
	public string? Description { get; set; }
	public string UserId { get; set; } = null!;
}