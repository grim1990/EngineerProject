using Domain.Entities;

namespace Application.Commands;

public static class CategoryCommands
{
    public static List<Category> CalculateCategoryOperationsValue(IEnumerable<Category> categories, IEnumerable<Operation> operations)
    {
        var categoriesValue = new List<Category>();

        foreach (var category in categories)
        {
            category.Value = 0;

            foreach (var operation in operations)
            {
                if (category.Id == operation.CategoryId)
                {
                    category.Value += operation.Value;
                }
            }

            categoriesValue.Add(category);
        }

        return categoriesValue;
    }
}
