using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
	public int Id { get; set; }
	public int Type { get; set; }
	public int MainCategoryId { get; set; }
	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public string UserId { get; set; } = null!;
	[RegularExpression(@"^\d+(?:[,.]\d{0,2})?$", ErrorMessage = "Należy wprowadzić poprawną wartość, który może zawierać znak '.' lub ','")]
	public decimal Value { get; set; }
	public bool OneOrMonth { get; set; }
	public DateTime CreateDate { get; set; }
	public DateTime? EditDate { get; set; }
	public bool IsAnnual { get; set; }
}
