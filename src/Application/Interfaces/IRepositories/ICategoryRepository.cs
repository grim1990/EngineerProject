using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface ICategoryRepository
{
    Task<Category> GetCategoryByIdAsync(int id);
    Task<List<Category>> GetAllCategoriesAsync(string userId);
    Task CreateCategoryAsync(Category category);
    Task EditCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}