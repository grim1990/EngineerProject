namespace Domain.Entities;

public class MainCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string UserId { get; set; } = null!;
    public DateTime CreateDate { get; set; }
    public DateTime? EditDate { get; set; }
    public bool IsAnnual { get; set; }
}