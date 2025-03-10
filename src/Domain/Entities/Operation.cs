using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Operation
{
	public int Id { get; set; }
	public int Type { get; set; }
	[RegularExpression(@"^\d+(?:[,.]\d{0,2})?$", ErrorMessage = "Należy wprowadzić poprawną wartość, który może zawierać znak '.' lub ','")]
	public decimal Value { get; set; }
	[RegularExpression(@"^[0-9,.;]+$", ErrorMessage = "Nieprawidłowa wartość, która może zawierać następujące znaki: '.' ',' lub znak ';' w celu wprowadzenia wielu operacji.")]
	public string? ValuesString { get; set; }
	public int Month { get; set; }
	public string? Description { get; set; }
	public int BudgetId { get; set; }
	public int CategoryId { get; set; }
	public string UserId { get; set; } = null!;
	public DateTime CreateDate { get; set; }
	public DateTime? EditDate { get; set; }
}