using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Models;

public class CustomSelectListItem : SelectListItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public new decimal Value { get; set; }
    public bool OneOrMonth { get; set; }
    public int Type { get; set; }
    public int MainCategoryId { get; set; }
}