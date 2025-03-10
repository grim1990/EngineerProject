using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface ICategoryService
{
    Task<Category> GetCategoryByIdAsync(int id);
    Task<List<Category>> GetAllCategoriesAsync();
    Task CreateCategoryAsync(Category category);
    Task EditCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
    Task<List<Category>> GetAllCategoriesByTypeAsync(int categoryType);
    Task<List<Category>> GetAllFilteredCategoriesByOperations(int categoryId, int month, int year, int type);
}