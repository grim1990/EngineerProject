using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Account
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	[RegularExpression(@"^\d+(?:[,.]\d{0,2})?$", ErrorMessage = "Należy wprowadzić poprawną wartość, który może zawierać znak '.' lub ','")]
	public decimal Value { get; set; }
	[RegularExpression(@"^\d+(?:[,.]\d{0,2})?$", ErrorMessage = "Należy wprowadzić poprawną wartość, który może zawierać znak '.' lub ','")]
	public decimal Blockade { get; set; }
	public string? Description { get; set; }
	public string UserId { get; set; } = null!;
}